using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Volo.Abp.Reflection;
using Volo.Abp.TextTemplating;
using Volo.Abp.TextTemplating.Razor;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vben5
{
    /// <summary>
    /// vben模板定义
    /// </summary>
    public class RongVoloAbpVueVben5TemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            string[] templates = ReflectionHelper.GetPublicConstantsRecursively(typeof(RongVoloAbpVueVbenTemplateNames));

            foreach (var item in templates)
            {
                if (item == RongVoloAbpVueVbenTemplateNames.Vben_myComponentSetting)
                {
                    continue;
                }
                string name = item.Split('_')[1];
                string itemName = string.Format(item, (int)VbenVersionEnum.Vben5);
                var def = new TemplateDefinition(itemName) //模板名称
                        .WithRazorEngine()
                        .WithVirtualFilePath(
                           $"/Templates/Vben5/{name}.cshtml", //模板路径，属性窗口中将其标记为"嵌入式资源"
                            isInlineLocalized: true
                        );

                //路径
                if (item == RongVoloAbpVueVbenTemplateNames.Vben_index ||
                    item == RongVoloAbpVueVbenTemplateNames.Vben_api)
                {
                    def.WithProperty("path", $"$rootPath/apps/web-antd/src/views/xxx");
                }
                else if (item == RongVoloAbpVueVbenTemplateNames.Vben_router)
                {
                    def.WithProperty("path", $"$rootPath/apps/web-antd/src/router/routes/modules");
                }
                else if (item == RongVoloAbpVueVbenTemplateNames.Vben_myComponentSetting)
                {
                    //def.WithProperty("path", $"$rootPath/apps/web-antd/src/settings");
                }
                else
                {
                    def.WithProperty("path", $"$rootPath/apps/web-antd/src/views/xxx/components");
                }

                //名称
                if (item == RongVoloAbpVueVbenTemplateNames.Vben_api)
                {
                    def.WithProperty("name", $"{name}.ts");
                }
                else if (item == RongVoloAbpVueVbenTemplateNames.Vben_router)
                {
                    def.WithProperty("name", $"xxx.ts");
                }
                else if (item == RongVoloAbpVueVbenTemplateNames.Vben_myComponentSetting)
                {
                    // def.WithProperty("name", $"myComponentSetting.ts");
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