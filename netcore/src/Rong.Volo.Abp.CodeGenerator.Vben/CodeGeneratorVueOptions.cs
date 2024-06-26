using System;
using System.Collections.Generic;
using System.Text;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    /// <summary>
    /// vue 代码生成选项
    /// </summary>
    public class CodeGeneratorVueOptions
    {
        /// <summary>
        /// Vben 组件映射
        /// </summary>
        public Dictionary<string, string> ComponentMapForVben { get; set; } = new Dictionary<string, string>();

        public CodeGeneratorVueOptions()
        {

        }
    }
}
