using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Helpers
{
    /// <summary>
    /// Helper for getting type information
    /// </summary>
    public static class TypeUtilities
    {
        /// <summary>
        /// Gets fields that are public and static
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<FieldInfo> GetConstants(this Type type)
        {
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        /// <summary>
        /// Gets the values from the constants
        /// </summary>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetConstantsValues<T>(this Type type) where T : class
        {
            var fieldInfos = GetConstants(type);

            return fieldInfos.Select(fi => fi.GetRawConstantValue() as T);
        }

        /// <summary>
        /// Gets the Category attribute from the field
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
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
