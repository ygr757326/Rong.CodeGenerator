using Volo.Abp.Reflection;
using Volo.Abp.TextTemplating;
using Volo.Abp.TextTemplating.Razor;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vbens
{
    /// <summary>
    /// vben模板定义
    /// </summary>
    public class RongVoloAbpVueTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            string[] templates = ReflectionHelper.GetPublicConstantsRecursively(typeof(RongVoloAbpVueVbenTemplateNames));

            foreach (var item in templates)
            {
                string name = item.Split('_')[1];

                var def = new TemplateDefinition(item) //模板名称
                        .WithRazorEngine()
                        .WithVirtualFilePath(
                           $"/Templates/Vben/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                            isInlineLocalized: true
                        );
                if (item == RongVoloAbpVueVbenTemplateNames.Vben_index ||
                    item == RongVoloAbpVueVbenTemplateNames.Vben_api)
                {
                    def.WithProperty("path", $"$rootPath/src/views/xxx");
                }
                else if (item == RongVoloAbpVueVbenTemplateNames.Vben_router)
                {
                    def.WithProperty("path", $"$rootPath/src/router/routes/modules");
                }
                else
                {
                    def.WithProperty("path", $"$rootPath/src/views/xxx/components");
                }
                if (item == RongVoloAbpVueVbenTemplateNames.Vben_api)
                {
                    def.WithProperty("name", $"{name}.ts");
                }
                else if (item == RongVoloAbpVueVbenTemplateNames.Vben_router)
                {
                    def.WithProperty("name", $"xxx.ts");
                }
                else
                {
                    def.WithProperty("name", $"{name}.vue");
                }

                context.Add(def);
            }
        }
    }
}