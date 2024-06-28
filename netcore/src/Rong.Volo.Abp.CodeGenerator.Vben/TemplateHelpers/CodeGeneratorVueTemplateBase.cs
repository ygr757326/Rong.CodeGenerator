using Microsoft.Extensions.Options;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers
{
    /// <summary>
    /// vue模板基类
    /// </summary>
    public abstract class CodeGeneratorVueTemplateBase
    {
        protected CodeGeneratorVueOptions Options;

        protected CodeGeneratorVueTemplateBase(IOptions<CodeGeneratorVueOptions> options)
        {
            Options = options.Value;
        }

        /// <summary>
        /// 获取映射的组件
        /// </summary>
        /// <param name="srcComponent">原组件</param>
        /// <returns></returns>
        protected virtual string GetMapComponent(string srcComponent)
        {
            if (Options.ComponentMapForVben == null || !Options.ComponentMapForVben.ContainsKey(srcComponent))
            {
                return srcComponent;
            }

            return Options.ComponentMapForVben[srcComponent];
        }

    }

}