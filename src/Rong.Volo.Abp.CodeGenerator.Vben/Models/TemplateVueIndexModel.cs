namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 主页模型
    /// </summary>
    public class TemplateVueIndexModel
    {
        /// <summary>
        /// 实体
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public TemplateVueIndexModel()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityName"></param>
        public TemplateVueIndexModel(string entity, string entityName)
        {
            Entity = entity;
            EntityName = entityName;
        }
    }
}
