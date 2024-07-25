using System;
using System.Collections.Generic;
using System.Text;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Enums
{
    /// <summary>
    /// Ant 的 Tabled 的 DataIndex 嵌套模式
    /// <para>2.x 版本 为 a.b.c</para>
    /// <para>3.x，4.x 版本 为 ['a','b','c']</para>
    /// </summary>
    public enum AntTabledDataIndexModeEnum
    {
        /// <summary>
        /// 点分隔
        /// </summary>
        Dotted,
        /// <summary>
        /// 字符串数组
        /// </summary>
        Array
    }
}
