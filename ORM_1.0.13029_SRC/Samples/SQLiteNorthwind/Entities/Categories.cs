//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5456
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenNETCF.ORM
{
    using System;
    using OpenNETCF.ORM;
    
    
    [Entity(KeyScheme.None)]
    public class Categories
    {
        
        private static Categories ORM_CreateProxy(OpenNETCF.ORM.FieldAttributeCollection fields, System.Data.IDataReader results)
        {
            var item = new Categories();
foreach(var field in fields){
            var value = results[field.Ordinal];
switch(field.FieldName){
case "CategoryID":
            // If this is a TimeSpan, use the commented line below
            // item.CategoryID = (value == DBNull.Value) ? TimeSpan.MinValue; : new TimeSpan((long)value);
            item.CategoryID = (value == DBNull.Value) ? 0 : (long)value;
            break;
case "CategoryName":
            item.CategoryName = (value == DBNull.Value) ? null : (string)value;
            break;
case "Description":
            item.Description = (value == DBNull.Value) ? null : (string)value;
            break;
case "Picture":
            item.Picture = (value == DBNull.Value) ? null : (byte[])value;
            break;
}
}
            return item;
        }
        
        private long m_categoryid;
        
        private string m_categoryname;
        
        private string m_description;
        
        private byte[] m_picture;
        
        [Field(IsPrimaryKey=true)]
        public long CategoryID
        {
            get
            {
                return this.m_categoryid;
            }
            set
            {
                this.m_categoryid = value;
            }
        }
        
        [Field()]
        public string CategoryName
        {
            get
            {
                return this.m_categoryname;
            }
            set
            {
                this.m_categoryname = value;
            }
        }
        
        [Field()]
        public string Description
        {
            get
            {
                return this.m_description;
            }
            set
            {
                this.m_description = value;
            }
        }
        
        [Field()]
        public byte[] Picture
        {
            get
            {
                return this.m_picture;
            }
            set
            {
                this.m_picture = value;
            }
        }
    }
}
