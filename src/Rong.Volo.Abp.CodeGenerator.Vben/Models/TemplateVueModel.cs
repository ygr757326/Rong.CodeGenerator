namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 模板模型
    /// </summary>
    public class TemplateVueModel
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
        public TemplateVueModel()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityName"></param>
        public TemplateVueModel(string entity, string entityName)
        {
            Entity = entity;
            EntityName = entityName;
        }
    }
}
