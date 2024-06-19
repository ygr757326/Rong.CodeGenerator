namespace Rong.CodeGenerator
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
        public string? Project
        {
            get
            {
                if (string.IsNullOrWhiteSpace(NameSpace))
                {
                    return NameSpace;
                }
                string[] s = NameSpace.Split('.');

                if (s.Length == 0 || s.Length == 1)
                {
                    return NameSpace;
                }
                return s[1];
            }
        }

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
        /// <param name="applicationAsController"></param>
        public TemplateModel(string entity, string entityName, string? nameSpace = null, bool? applicationAsController = null)
        {
            Entity = entity;
            EntityName = entityName;
            NameSpace = nameSpace;
            ApplicationAsController = applicationAsController;
        }
    }
}
