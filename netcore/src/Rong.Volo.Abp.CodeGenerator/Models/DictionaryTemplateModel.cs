using System;

namespace Rong.Volo.Abp.CodeGenerator.Models
{
    /// <summary>
    /// 模板模型
    /// </summary>
    public class DictionaryTemplateModel
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string? NameSpace { get; set; }
        /// <summary>
        /// 命名空间分层名称
        /// </summary>
        public string? NameSpaceLayerName { get; set; }
        /// <summary>
        /// 要保存到的文件夹路径
        /// </summary>
        public string? SaveFolderPath { get; set; }

        /// <summary>
        /// 要保存到的文件夹路径
        /// </summary>
        public string? SaveFolderPathNormalization => string.IsNullOrWhiteSpace(SaveFolderPath)
            ? SaveFolderPath
            : SaveFolderPath.Replace("/", ".").Replace("\\", ".");

        /// <summary>
        /// 字典枚举类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public DictionaryTemplateModel()
        {
        }
    }
}
