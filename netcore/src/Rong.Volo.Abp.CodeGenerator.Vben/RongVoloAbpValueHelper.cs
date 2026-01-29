using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Volo.Abp.Reflection;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    internal static class RongVoloAbpValueHelper
    {
        /// <summary>
        /// 获取时间格式化
        /// </summary>
        /// <param name="dateType"></param>
        /// <returns></returns>
        public static string? GetDateFormat(this VueDateTypeEnum dateType)
        {
            switch (dateType)
            {
                case VueDateTypeEnum.Year:
                    return "YYYY";
                case VueDateTypeEnum.Month:
                    return "YYYY-MM";
                case VueDateTypeEnum.Date:
                    return "YYYY-MM-DD";
                case VueDateTypeEnum.DateTime:
                    return "YYYY-MM-DD HH:mm:ss";
                case VueDateTypeEnum.TimeSpan:
                    return "HH:mm:ss";
                default:
                    return null;
            }
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


        /// <summary>
        /// 是否是不可为null的引用类型
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static bool IsNonNullableReferenceType(this MemberInfo memberInfo)
        {
            var memberType = memberInfo.MemberType == MemberTypes.Field
                ? ((FieldInfo)memberInfo).FieldType
                : ((PropertyInfo)memberInfo).PropertyType;

            if (memberType.IsValueType) return false;

            var nullableAttribute = GetNullableAttribute(memberInfo);

            if (nullableAttribute == null)
            {
                return GetNullableFallbackValue(memberInfo);
            }

            if (nullableAttribute.GetType().GetField(NullableFlagsFieldName) is FieldInfo field &&
                field.GetValue(nullableAttribute) is byte[] flags &&
                flags.Length >= 1 && flags[0] == NotAnnotated)
            {
                return true;
            }

            return false;
        }
        private const string NullableAttributeFullTypeName = "System.Runtime.CompilerServices.NullableAttribute";
        private const string NullableFlagsFieldName = "NullableFlags";
        private const string NullableContextAttributeFullTypeName = "System.Runtime.CompilerServices.NullableContextAttribute";
        private const string FlagFieldName = "Flag";
        private const int NotAnnotated = 1; // See https://github.com/dotnet/roslyn/blob/af7b0ebe2b0ed5c335a928626c25620566372dd1/docs/features/nullable-metadata.md?plain=1#L40
        private static object GetNullableAttribute(MemberInfo memberInfo)
        {
            var nullableAttribute = memberInfo
                .GetCustomAttributes()
                .FirstOrDefault(attr => string.Equals(attr.GetType().FullName, NullableAttributeFullTypeName));

            return nullableAttribute;
        }

        private static bool GetNullableFallbackValue(MemberInfo memberInfo)
        {
            var declaringTypes = memberInfo.DeclaringType.IsNested
                ? GetDeclaringTypeChain(memberInfo)
                : new List<Type>(1) { memberInfo.DeclaringType };

            foreach (var declaringType in declaringTypes)
            {
                var attributes = (IEnumerable<object>)declaringType.GetCustomAttributes(false);

                var nullableContext = attributes
                    .FirstOrDefault(attr => string.Equals(attr.GetType().FullName, NullableContextAttributeFullTypeName));

                if (nullableContext != null)
                {
                    if (nullableContext.GetType().GetField(FlagFieldName) is FieldInfo field &&
                        field.GetValue(nullableContext) is byte flag && flag == NotAnnotated)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
        private static List<Type> GetDeclaringTypeChain(MemberInfo memberInfo)
        {
            var chain = new List<Type>();
            var currentType = memberInfo.DeclaringType;

            while (currentType != null)
            {
                chain.Add(currentType);
                currentType = currentType.DeclaringType;
            }

            return chain;
        }
    }
}
