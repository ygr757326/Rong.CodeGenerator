using System;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue 内容文本特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueTextareaAttribute : VueFieldSeqAttribute
    {
        public VueTextareaAttribute(short fieldSeq = short.MaxValue) : base(fieldSeq)
        {
        }
    }
}
