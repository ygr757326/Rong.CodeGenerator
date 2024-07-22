using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rong.CodeGenerator.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace Rong.CodeGenerator.App.Dictionarys
{
    /// <summary>
    /// 字典管理 表
    /// </summary>
    [Table("Dictionary")]
    [Display(Name = "字典管理")]
    public class Dictionary : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 类别编号 <see cref="DictionaryTypeEnum"/>
        /// </summary>
        [Display(Name = "类别编号")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MaxLength(64, ErrorMessage = "{0}最大长度为{1}")]
        [EnumDataType(typeof(DictionaryTypeEnum), ErrorMessage = "{0}值不存在")]
        public virtual string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Display(Name = "值")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MaxLength(64, ErrorMessage = "{0}最大长度为{1}")]
        public virtual string Value { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MaxLength(128, ErrorMessage = "{0}最大长度为{1}")]
        public virtual string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Display(Name = "显示名称")]
        [MaxLength(128, ErrorMessage = "{0}最大长度为{1}")]
        public virtual string? DisplayName { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Display(Name = "序号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public virtual int Sort { get; set; }

        /// <summary>
        /// 是否活动
        /// </summary>

        [Display(Name = "是否活动")]
        [Required(ErrorMessage = "{0}不能为空")]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(128, ErrorMessage = "{0}最大长度为{1}")]
        public virtual string? Remark { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public Dictionary()
        {
            IsActive = true;
        }

        /// <summary>
        /// 构造
        /// </summary>
        public Dictionary(Guid id) : base(id)
        {
            IsActive = true;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="isActive"></param>
        public void ChangeActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
