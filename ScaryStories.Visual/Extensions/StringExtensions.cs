using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaryStories.Entities.Dto;

namespace ScaryStories.Visual.Extensions
{
    public static class StringExtensions
    {
        public static string SerializeListOfIdsToString(IEnumerable<int> ids)
        {
            string res = "";
            foreach (var id in ids)
            {
                res += id + "|";
            }
            return res;
        }

        public static List<int> DeserializeListOfIdsToString(string idString)
        {
           
            var idsStr=idString.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries).ToList();
             var result = new int[idsStr.Count];
            var check = 0;
            foreach (var id in idsStr)
            {
                result[check] = int.Parse(id);
                check++;
            }
            var withoutZero = result.ToList();
            return withoutZero;
        }

        
    }
}
