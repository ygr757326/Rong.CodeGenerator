using System;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue 编辑器特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueEditorAttribute : Attribute
    {
        public VueEditorAttribute()
        {
        }
    }
}
