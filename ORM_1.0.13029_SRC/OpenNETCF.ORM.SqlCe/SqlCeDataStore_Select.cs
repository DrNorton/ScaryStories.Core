﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using System.Threading;
using System.Data.Common;
using System.Reflection;

namespace OpenNETCF.ORM
{
    partial class SqlCeDataStore
    {
        private string m_lastEntity;
        private FieldAttributeCollection m_fields;
        private ReferenceAttribute[] m_references;

        protected override TCommand GetSelectCommand<TCommand, TParameter>(string entityName, IEnumerable<FilterCondition> filters, out bool tableDirect)
        {
            tableDirect = true;
            var buildFilter = false;
            string indexName = null;

            if (filters != null)
            {
                if (filters.Count() == 1)
                {
                    var filter = filters.First();

                    if (!(filter is SqlFilterCondition))
                    {
                        var field = Entities[entityName].Fields[filter.FieldName];

                        if (!field.IsPrimaryKey)
                        {
                            if (field.SearchOrder == FieldSearchOrder.NotSearchable)
                            {
                                buildFilter = true;
                            }
                            else if (filter.Operator != FilterCondition.FilterOperator.Equals)
                            {
                                buildFilter = true;
                            }
                            else
                            {
                                indexName = string.Format("ORM_IDX_{0}_{1}_{2}", entityName, filter.FieldName,
                                    field.SearchOrder == FieldSearchOrder.Descending ? "DESC" : "ASC");

                                // build the index if it's not there
                                VerifyIndex(entityName, filter.FieldName, field.SearchOrder, null);
                            }
                        }
                    }
                }
                else if (filters.Count() >= 1)
                {
                    var filter = filters.First() as SqlFilterCondition;

                    if (filter == null || !filter.PrimaryKey)
                    {
                        buildFilter = true;
                    }
                }
            }

            if (buildFilter)
            {
                tableDirect = false;
                return BuildFilterCommand<TCommand, TParameter>(entityName, filters);
            }

            return new SqlCeCommand()
            {
                CommandText = entityName,
                CommandType = CommandType.TableDirect,
                IndexName = indexName ?? ((SqlEntityInfo)Entities[entityName]).PrimaryKeyIndexName
            } as TCommand;
        }

        private void UpdateIndexCacheForType(string entityName)
        {
            // have we already cached this?
            if (((SqlEntityInfo)Entities[entityName]).IndexNames != null) return;

            // get all iindex names for the type
            var connection = GetConnection(true);
            try
            {
                string sql = string.Format("SELECT INDEX_NAME FROM information_schema.indexes WHERE (TABLE_NAME = '{0}')", entityName);

                using (SqlCeCommand command = new SqlCeCommand(sql, connection as SqlCeConnection))
                using (var reader = command.ExecuteReader())
                {
                    List<string> nameList = new List<string>();

                    while (reader.Read())
                    {
                        nameList.Add(reader.GetString(0));
                    }

                    ((SqlEntityInfo)Entities[entityName]).IndexNames = nameList;
                }
            }
            finally
            {
                DoneWithConnection(connection, true);
            }
        }

        protected override IEnumerable<object> Select(Type objectType, IEnumerable<FilterCondition> filters, int fetchCount, int firstRowOffset, bool fillReferences)
        {
            string entityName = m_entities.GetNameForType(objectType);

            if (entityName == null)
            {
                throw new EntityNotFoundException(objectType);
            }

            return Select(entityName, objectType, filters, fetchCount, firstRowOffset, fillReferences);
        }

        public override DynamicEntity Select(string entityName, object primaryKey)
        {
            var filter = new SqlFilterCondition
            {
                FieldName = (Entities[entityName] as SqlEntityInfo).PrimaryKeyIndexName,
                Operator = FilterCondition.FilterOperator.Equals,
                Value = primaryKey,
                PrimaryKey = true
            };

            return (DynamicEntity)Select(entityName, typeof(DynamicEntity), new FilterCondition[] { filter }, -1, -1, false).FirstOrDefault();
        }

        public override IEnumerable<DynamicEntity> Select(string entityName)
        {
            return Select(entityName, typeof(DynamicEntity), null, -1, -1, false).Cast<DynamicEntity>();
        }

        public T First<T>(DbSeekOptions option, string seekField, object seekValue)
            where T : class
        {
            var entityName = m_entities.GetNameForType(typeof(T));

            return (T)Seek(entityName, typeof(T), option, seekField, seekValue);
        }

        public DynamicEntity First(string entityName, DbSeekOptions option, string seekField, object seekValue)
        {
            return (DynamicEntity)Seek(entityName, typeof(DynamicEntity), option, seekField, seekValue);
        }

        private object Seek(string entityName, Type objectType, DbSeekOptions option, string seekField, object seekValue)
        {
            UpdateIndexCacheForType(entityName);

            var connection = GetConnection(false);
            var fillReferences = false;
            SqlCeCommand command = null;

            if (UseCommandCache)
            {
                Monitor.Enter(CommandCache);
            }

            try
            {
                ReferenceAttribute[] referenceFields = null;
                bool tableDirect;

                CheckOrdinals(entityName);
                CheckPrimaryKeyIndex(entityName);

                command = GetSelectCommand<SqlCeCommand, SqlCeParameter>(entityName, 
                    new FilterCondition[] { new FilterCondition(seekField, seekValue, FilterCondition.FilterOperator.Equals) }, 
                    out tableDirect);
                command.Connection = connection as SqlCeConnection;
                command.Transaction = CurrentTransaction as SqlCeTransaction;

                using (var results = command.ExecuteReader())
                {
                    bool found = results.Seek(option, seekValue);

                    if (!found) return null;

                    results.Read();

                    bool fieldsSet;
                    object item = CreateEntityInstance(entityName, objectType, Entities[entityName].Fields, results, out fieldsSet);

                    if (!fieldsSet)
                    {
                        // fill in the entity field values
                        PopulateFields(entityName, Entities[entityName].Fields, results, item, fillReferences);
                    }

                    // autofill references if desired
                    if ((fillReferences) && (referenceFields.Length > 0))
                    {
                        FillReferences(item, null, referenceFields, false);
                    }

                    return item;
                }
            }
            finally
            {
                if ((!UseCommandCache) && (command != null))
                {
                    command.Dispose();
                }

                if (UseCommandCache)
                {
                    Monitor.Exit(CommandCache);
                }

                FlushReferenceTableCache();
                DoneWithConnection(connection, false);
            }
        }

        private IEnumerable<object> Select(string entityName, Type objectType, IEnumerable<FilterCondition> filters, int fetchCount, int firstRowOffset, bool fillReferences)
        {

            UpdateIndexCacheForType(entityName);

            var items = new List<object>();
            bool tableDirect;

            var connection = GetConnection(false);
            SqlCeCommand command = null;

            if (UseCommandCache)
            {
                Monitor.Enter(CommandCache);
            }

            try
            {
                CheckOrdinals(entityName);
                CheckPrimaryKeyIndex(entityName);

                command = GetSelectCommand<SqlCeCommand, SqlCeParameter>(entityName, filters, out tableDirect);
                command.Connection = connection as SqlCeConnection;
                command.Transaction = CurrentTransaction as SqlCeTransaction;

                int searchOrdinal = -1;

                object matchValue = null;
                string matchField = null;

                if (tableDirect) // use index
                {
                    if ((filters != null) && (filters.Count() > 0))
                    {
                        var filter = filters.First();

                        matchValue = filter.Value ?? DBNull.Value;
                        matchField = filter.FieldName;

                        var sqlfilter = filter as SqlFilterCondition;
                        if ((sqlfilter != null) && (sqlfilter.PrimaryKey))
                        {
                            searchOrdinal = ((SqlEntityInfo)Entities[entityName]).PrimaryKeyOrdinal;
                        }
                    }

                    // we need to ensure that the search value does not exceed the length of the indexed
                    // field, else we'll get an exception on the Seek call below
                    var indexInfo = GetIndexInfo(command.IndexName);
                    if (indexInfo != null)
                    {
                        if (indexInfo.MaxCharLength > 0)
                        {
                            var value = (string)matchValue;
                            if (value.Length > indexInfo.MaxCharLength)
                            {
                                matchValue = value.Substring(0, indexInfo.MaxCharLength);
                            }
                        }
                    }
                }

                using (var results = command.ExecuteReader())
                {
                    ReferenceAttribute[] referenceFields = null;

                    int currentOffset = 0;

                    if (matchValue != null)
                    {
                        // convert enums to an int, else the .Equals later check will fail
                        // this feels a bit kludgey, but for now it's all I can think of
                        if (matchValue.GetType().IsEnum)
                        {
                            matchValue = (int)matchValue;
                        }

                        if (searchOrdinal < 0)
                        {
                            searchOrdinal = results.GetOrdinal(matchField);
                        }

                        if (tableDirect)
                        {
                            results.Seek(DbSeekOptions.FirstEqual, new object[] { matchValue });
                        }
                    }

                    while (results.Read())
                    {
                        if (currentOffset < firstRowOffset)
                        {
                            currentOffset++;
                            continue;
                        }

                        if (tableDirect && (matchValue != null))
                        {
                            // if we have a match value, we'll have seeked to the first match above
                            // then at this point the first non-match means we have no more matches, so
                            // we can exit out once we hit the first non-match.

                            // For string we want a case-insensitive search, so it's special-cased here
                            if (matchValue is string)
                            {
                                if (string.Compare((string)results[searchOrdinal], (string)matchValue, true) != 0)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if (!results[searchOrdinal].Equals(matchValue))
                                {
                                    break;
                                }
                            }
                        }

                        // autofill references if desired
                        if (referenceFields == null)
                        {
                            referenceFields = Entities[entityName].References.ToArray();
                        }

                        // if the entity type changed since the last Select call, re-cache some items (perf improvements)
                        if (m_lastEntity != entityName)
                        {
                            m_fields = Entities[entityName].Fields;
                            m_references = Entities[entityName].References.ToArray();
                            m_lastEntity = entityName;
                        }

                        bool fieldsSet;
                        object item = CreateEntityInstance(entityName, objectType, m_fields, results, out fieldsSet);

                        if (!fieldsSet)
                        {
                            // fill in the entity field values
                            PopulateFields(entityName, m_fields, results, item, fillReferences);
                        }
                        // autofill references if desired
                        if ((fillReferences) && (referenceFields.Length > 0))
                        {
                            FillReferences(item, null, referenceFields, false);
                        }

                        yield return item;

                        if ((fetchCount > 0) && (items.Count >= fetchCount))
                        {
                            break;
                        }
                    }
                }
            }
            finally
            {
                if ((!UseCommandCache) && (command != null))
                {
                    command.Dispose();
                }

                if (UseCommandCache)
                {
                    Monitor.Exit(CommandCache);
                }

                FlushReferenceTableCache();
                DoneWithConnection(connection, false);
            }
        }

        private void PopulateFields(string entityName, FieldAttributeCollection fields, IDataReader results, object item, bool fillReferences)
        {
            object rowPK;

            foreach (var field in fields)
            {
                var value = results[field.Ordinal];
                if (value != DBNull.Value)
                {
                    if (field.DataType == DbType.Object)
                    {
                        // get serializer
                        var itemType = item.GetType();
                        var deserializer = GetDeserializer(itemType);

                        if (deserializer == null)
                        {
                            throw new MissingMethodException(
                                string.Format("The field '{0}' requires a custom serializer/deserializer method pair in the '{1}' Entity",
                                field.FieldName, entityName));
                        }

                        var @object = deserializer.Invoke(item, new object[] { field.FieldName, value });
                        field.PropertyInfo.SetValue(item, @object, null);
                    }
                    else if (field.IsRowVersion)
                    {
                        // sql stores this an 8-byte array
                        field.PropertyInfo.SetValue(item, BitConverter.ToInt64((byte[])value, 0), null);
                    }
                    else if (field.IsTimespan)
                    {
                        // SQL Compact doesn't support Time, so we're convert to ticks in both directions
                        var valueAsTimeSpan = new TimeSpan((long)value);
                        field.PropertyInfo.SetValue(item, valueAsTimeSpan, null);
                    }
                    else
                    {
                        field.PropertyInfo.SetValue(item, value, null);
                    }
                }

                // Check if it is reference key to set, not primary.
                ReferenceAttribute attr = null;

                if (m_references != null)
                {
                    attr = m_references.Where(
                    x => x.ReferenceField == field.FieldName).FirstOrDefault();
                }

                if ((field.IsPrimaryKey) || (attr != null))
                {
                    rowPK = value;
                }
            }
        }
    }
}
 