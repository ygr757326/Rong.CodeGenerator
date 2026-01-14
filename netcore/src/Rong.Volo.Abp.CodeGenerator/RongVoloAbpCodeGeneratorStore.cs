using Rong.Volo.Abp.CodeGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.TextTemplating;

namespace Rong.Volo.Abp.CodeGenerator
{
    /// <summary>
    /// 代码生成器帮助器
    /// </summary>
    public class RongVoloAbpCodeGeneratorStore : RongVoloAbpCodeGeneratorStoreBase
    {
        public RongVoloAbpCodeGeneratorStore(ITemplateDefinitionManager templateDefinitionManager,
            ITemplateRenderer templateRenderer) : base(templateDefinitionManager, templateRenderer)
        {
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <param name="applicationAsController">统一是否应用层作为控制器，如果 <see cref="TemplateModel.ApplicationAsController"/> 为空则使用 <paramref name="applicationAsController"/>，否则使用 <see cref="TemplateModel.ApplicationAsController"/>(默认true,不生成）</param>
        /// <param name="saveFolderName">要保存到的目录</param>
        /// <param name="nameSpace">统一命名空间，如果 <see cref="TemplateModel.NameSpace"/> 为空则使用 <paramref name="nameSpace"/>，否则使用 <see cref="TemplateModel.NameSpace"/></param>
        /// <param name="project">项目，若为空，则取 <paramref name="nameSpace"/>.Split('.')[1] </param>
        /// <returns></returns>
        public async Task StartAsync(List<TemplateModel> entities, string nameSpace, string? project = null, bool applicationAsController = true, string saveFolderName = "App")
        {
            Check.NotNull(entities, nameof(entities));
            Check.NotNullOrWhiteSpace(nameSpace, nameof(nameSpace));
            Check.NotNullOrWhiteSpace(saveFolderName, nameof(saveFolderName));

            var entitys = entities
                .Where(a => !string.IsNullOrWhiteSpace(a.Entity))
                .GroupBy(a => a.Entity)
                .Where(a => a.Count() > 1)
                .Select(a => a.Key)
                .ToList();
            if (entitys.Any())
            {
                throw new UserFriendlyException($"以下实体名称重复：{entitys.JoinAsString(",")}");
            }

            var tasks = new List<Task>();

            foreach (var item in entities)
            {
                item.EntityCase = item.Entity.ToCamelCase();

                if (string.IsNullOrWhiteSpace(item.SaveFolderName))
                {
                    item.SaveFolderName = saveFolderName;
                }
                if (string.IsNullOrWhiteSpace(item.NameSpace))
                {
                    item.NameSpace = nameSpace;
                }
                if (string.IsNullOrWhiteSpace(item.Project))
                {
                    item.Project = project;
                }

                item.ApplicationAsController ??= applicationAsController;

                item.SetProject();

                tasks.Add(Task.Run(async () => { await GenerateForEntityAsync(item); }));
            }

            //创建基类
            tasks.Add(Task.Run(async () => { await GenerateForBaseAsync(nameSpace, project); }));

            //等待任务执行完毕
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 生成实体相关类
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        private async Task GenerateForEntityAsync(TemplateModel entity)
        {
            string[] templates = new[]
                {
                    RongVoloAbpCodeGeneratorTemplateNames.AppService_xxxAppService,
                    RongVoloAbpCodeGeneratorTemplateNames.AppService_xxxMapper,

                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContracts_IxxxAppService,
                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContracts_xxxPermissions,

                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxBaseOutput,
                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxCreateInput,
                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxCreateOrUpdateInputBase,
                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxDetailOutput,
                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxDropDownOutput,
                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxDropDownSearchInput,
                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxPageOutput,
                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxPageSearchInput,
                    RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsDto_xxxUpdateInput,

                    RongVoloAbpCodeGeneratorTemplateNames.Domain_xxx,
                    RongVoloAbpCodeGeneratorTemplateNames.Domain_IxxxRepository,

                    RongVoloAbpCodeGeneratorTemplateNames.DomainService_xxxManager,
                    RongVoloAbpCodeGeneratorTemplateNames.DomainService_xxxMapper,

                    RongVoloAbpCodeGeneratorTemplateNames.DomainShared_xxxConsts,
                    RongVoloAbpCodeGeneratorTemplateNames.DomainShared_xxxEto,

                    RongVoloAbpCodeGeneratorTemplateNames.EntityFrameworkCore_xxxEntityTypeConfiguration,
                    RongVoloAbpCodeGeneratorTemplateNames.EntityFrameworkCore_xxxRepository,

                    RongVoloAbpCodeGeneratorTemplateNames.HttpApi_xxxController,
                };

            foreach (var template in templates)
            {
                //模板不存在
                var temp = await TemplateDefinitionManager.GetOrNullAsync(template);
                if (temp == null)
                {
                    continue;
                }

                string name = temp.Properties["name"].ToString().Replace("xxx", entity.Entity)
                    .Replace("$project", entity.Project);

                string path = temp.Properties["path"].ToString().Replace("xxx", entity.Entity)
                    .Replace("$namespace", entity.NameSpace)
                    .Replace("$folderName", entity.SaveFolderName);

                //未启用
                if (entity.ApplicationAsController == true &&
                    template == RongVoloAbpCodeGeneratorTemplateNames.HttpApi_xxxController)
                {
                    continue;
                }

                //保存
                await SaveAsync(entity, template, name, path);
            }

        }

        /// <summary>
        /// 生成基类
        /// </summary>
        /// <returns></returns>
        private async Task GenerateForBaseAsync(string? nameSpace, string? project)
        {
            var model = new TemplateModel
            {
                NameSpace = nameSpace,
                Project = project
            };

            model.SetProject();

            string[] templates = new[]
            {
                RongVoloAbpCodeGeneratorTemplateNames.Domain_DomainServiceBase,
                RongVoloAbpCodeGeneratorTemplateNames.HttpApi_ControllerBase,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionAttribute,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionConsts,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionExtensions,
                RongVoloAbpCodeGeneratorTemplateNames.ApplicationContractsPermissions_PermissionMultiTenancySideAttribute,
            };

            foreach (var template in templates)
            {
                //模板不存在
                var temp = await TemplateDefinitionManager.GetOrNullAsync(template);
                if (temp == null)
                {
                    continue;
                }

                string name = temp.Properties["name"].ToString().Replace("$project", model.Project);
                string path = temp.Properties["path"].ToString().Replace("$namespace", model.NameSpace);

                //保存
                await SaveAsync(model, template, name, path);
            }
        }
    }

}