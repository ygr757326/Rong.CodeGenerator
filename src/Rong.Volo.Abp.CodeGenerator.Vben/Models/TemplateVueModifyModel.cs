namespace Rong.Volo.Abp.CodeGenerator.Vue.Models
{
    /// <summary>
    /// 修改页模型
    /// </summary>
    public class TemplateVueModifyModel
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
        public TemplateVueModifyModel()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityName"></param>
        public TemplateVueModifyModel(string entity, string entityName)
        {
            Entity = entity;
            EntityName = entityName;
        }
    }
}
