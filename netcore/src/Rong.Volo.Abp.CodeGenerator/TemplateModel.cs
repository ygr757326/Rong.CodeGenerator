using System;

namespace Rong.Volo.Abp.CodeGenerator
{
    /// <summary>
    /// 模板模型
    /// </summary>
    public class TemplateModel
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string? NameSpace { get; set; }

        /// <summary>
        /// 实体
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// 实体小写
        /// </summary>
        public string? EntityCase { get; internal set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 是否应用层作为控制器（默认不生成）
        /// <para>true：不会生成.HttpApi，false：会生成.HttpApi</para>
        /// </summary>
        public bool? ApplicationAsController { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string? Project { get; set; }

        /// <summary>
        /// 要保存到的文件夹名称（默认App）
        /// </summary>
        public string? SaveFolderName { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public TemplateModel()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityName"></param>
        /// <param name="nameSpace"></param>
        /// <param name="project"></param>
        /// <param name="applicationAsController"></param>
        /// <param name="saveFolderName"></param>
        public TemplateModel(string entity, string entityName, string? nameSpace = null, string? project = null, bool? applicationAsController = null, string? saveFolderName = null)
        {
            Entity = entity;
            EntityName = entityName;
            NameSpace = nameSpace;
            Project = project;
            ApplicationAsController = applicationAsController;
            SaveFolderName = saveFolderName;
        }

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <returns></returns>
        public void SetProject()
        {
            if (!string.IsNullOrWhiteSpace(Project))
            {
                return;
            }

            Project = GetProject(NameSpace);
        }

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <returns></returns>
        public static string? GetProject(string? nameSpace)
        {
            if (string.IsNullOrWhiteSpace(nameSpace))
            {
                return nameSpace;
            }

            string[] s = nameSpace.Split('.');

            if (s.Length == 0 || s.Length == 1)
            {
                return nameSpace;
            }
            return s[1];
        }
    }
}
