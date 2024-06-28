using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    internal static class ValueHelper
    {
        /// <summary>
        /// 是否是Nullable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullableValueType(this Type type)
        {
            // 如果类型是Nullable<>，并且不是对Nullable结构体的引用，则是可空值类型
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && !type.IsGenericTypeDefinition;
        }

        public static StringBuilder Space(this StringBuilder b, int length)
        {
            return b.Append("".PadLeft(length));
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeCode GetMyTypeCode(this Type type)
        {
            if (type.IsNullableValueType())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            return Type.GetTypeCode(type);
        }

        /// <summary>
        /// 获取  Decimal 小数位
        /// </summary>
        /// <returns></returns>
        public static int GetDecimalPlaceFromColumnAttribute(this PropertyInfo propertyInfo)
        {
            int length = 2;
            var column = propertyInfo.GetCustomAttribute<ColumnAttribute>();

            if (column?.TypeName != null)
            {
                string pattern = @"\((.*?)\)";
                var match = Regex.Match(column.TypeName, pattern);
                if (match.Success)
                {
                    var de = match.Groups[1].Value.Split(",");
                    if (de.Length == 2)
                    {
                        int.TryParse(de[1], out length);
                    }
                }
            }

            return length;
        }
    }
}
