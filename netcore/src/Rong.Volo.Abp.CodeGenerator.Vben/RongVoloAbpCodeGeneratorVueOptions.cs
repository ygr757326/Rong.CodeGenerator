using System;
using System.Collections.Generic;
using System.Text;
using Rong.Volo.Abp.CodeGenerator.Vue.Enums;

namespace Rong.Volo.Abp.CodeGenerator.Vue
{
    /// <summary>
    /// vue 代码生成选项
    /// </summary>
    public class RongVoloAbpCodeGeneratorVueOptions
    {
        /// <summary>
        /// Antdv 的 Tabled 的 DataIndex 嵌套模式。默认 Array
        /// <para>2.x 版本 为 a.b.c</para>
        /// <para>3.x,4.x 版本 为 ['a','b','c']</para>
        /// </summary>
        public AntTabledDataIndexModeEnum? AntTabledDataIndexMode { get; set; }

        /// <summary>
        /// Vben 组件偷替换映射：原组件,新组件
        /// </summary>
        public Dictionary<string, string> ComponentMapForVben { get; set; }

        /// <summary>
        /// 枚举Select组件名称
        /// </summary>
        public string? EnumSelectComponent { get; set; }
        /// <summary>
        /// 枚举Select组件 prop
        /// </summary>
        public string? EnumSelectComponentProp { get; set; }
        /// <summary>
        /// 枚举Radio组件名称
        /// </summary>
        public string? EnumRadioComponent { get; set; }
        /// <summary>
        /// 枚举Radio组件 prop
        /// </summary>
        public string? EnumRadioComponentProp { get; set; }
        /// <summary>
        /// 枚举Checkbox组件名称
        /// </summary>
        public string? EnumCheckboxComponent { get; set; }
        /// <summary>
        /// 枚举Checkbox组件 prop
        /// </summary>
        public string? EnumCheckboxComponentProp { get; set; }

        /// <summary>
        /// 字典Select组件名称
        /// </summary>
        public string? DictionarySelectComponent { get; set; }
        /// <summary>
        /// 字典Select组件 prop
        /// </summary>
        public string? DictionarySelectComponentProp { get; set; }
        /// <summary>
        /// 字典Radio组件名称
        /// </summary>
        public string? DictionaryRadioComponent { get; set; }
        /// <summary>
        /// 字典Radio组件 prop
        /// </summary>
        public string? DictionaryRadioComponentProp { get; set; }
        /// <summary>
        /// 字典Checkbox组件名称
        /// </summary>
        public string? DictionaryCheckboxComponent { get; set; }
        /// <summary>
        /// 字典Checkbox组件 prop
        /// </summary>
        public string? DictionaryCheckboxComponentProp { get; set; }

        /// <summary>
        /// bool Select组件
        /// </summary>
        public string? BoolSelectComponent { get; set; }
        /// <summary>
        /// bool Radio组件
        /// </summary>
        public string? BoolRadioComponent { get; set; }

        /// <summary>
        /// 文件上传组件名称
        /// </summary>
        public string? FileUploadComponent { get; set; }
        /// <summary>
        /// 文件预览组件
        /// </summary>
        public string? FilePreviewComponent { get; set; }
        /// <summary>
        /// 文件预览组件 prop
        /// </summary>
        public string? FilePreviewComponentProp { get; set; }

        /// <summary>
        /// 图片上传组件名称
        /// </summary>
        public string? ImageUploadComponent { get; set; }
        /// <summary>
        /// 图片预览组件
        /// </summary>
        public string? ImagePreviewComponent { get; set; }
        /// <summary>
        /// 图片预览组件 prop
        /// </summary>
        public string? ImagePreviewComponentProp { get; set; }

        /// <summary>
        /// 编辑器组件
        /// </summary>
        public string? EditorComponent { get; set; }

        public RongVoloAbpCodeGeneratorVueOptions()
        {
            AntTabledDataIndexMode = AntTabledDataIndexModeEnum.Array;
            ComponentMapForVben = new Dictionary<string, string>();
        }
    }
}
