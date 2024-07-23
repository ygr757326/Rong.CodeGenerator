using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Volo.Abp.Reflection;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    internal static class RongVoloAbpValueHelper
    {

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
            var type1 = type.GetFirstGenericArgumentIfNullable();
            return Type.GetTypeCode(type1);
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
