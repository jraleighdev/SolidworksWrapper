using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Helpers
{
    public static class TypeUtilities
    {
        public static IEnumerable<FieldInfo> GetConstants(this Type type)
        {
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        public static IEnumerable<T> GetConstantsValues<T>(this Type type) where T : class
        {
            var fieldInfos = GetConstants(type);

            return fieldInfos.Select(fi => fi.GetRawConstantValue() as T);
        }

        public static string GetCategoryAttribute(this FieldInfo field)
        {
            object[] att = field.GetCustomAttributes(typeof(CategoryAttribute), false);

            if (att.Length > 0)
            {
                return ((CategoryAttribute)att[0]).Category;
            }
            else
            {
                return "";
            }    
        }
    }
}
