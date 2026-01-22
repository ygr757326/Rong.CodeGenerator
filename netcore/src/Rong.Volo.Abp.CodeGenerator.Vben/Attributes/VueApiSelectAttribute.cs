using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Rong.Volo.Abp.CodeGenerator.Vue.Attributes
{
    /// <summary>
    /// vue api下拉数据特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VueApiSelectAttribute : VueTableSorterAttribute
    {
        public const string _component = "ApiSelect";

        /// <summary>
        /// 哪个实体的下拉
        /// </summary>
        public string? Entity { get; set; }

        /// <summary>
        /// api名称
        /// </summary>
        public string? ApiName { get; set; }

        /// <summary>
        /// 文本字段
        /// </summary>
        public string? LabelField { get; set; }

        /// <summary>
        /// 值字段
        /// </summary>
        public string? ValueField { get; set; }

        /// <summary>
        /// 是否多选
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">哪个实体的下拉，/api/ApiRootPath/EntityCase/GetDropDownList</param>
        /// <param name="multiple"></param>
        /// <param name="labelField">文本示字段，默认 label</param>
        /// <param name="valueField">值字段，默认 value</param>
        /// <param name="sorter"></param>
        /// <param name="fieldSeq"></param>
        /// <param name="apiName">api名称</param>
        /// <param name="component"></param>
        public VueApiSelectAttribute(string entity, bool multiple = false, string? labelField = null, string apiName = "GetDropDownList", string component = _component, string? valueField = null, bool sorter = true, short fieldSeq = short.MaxValue) : base(sorter, fieldSeq)
        {
            Entity = entity;
            ApiName = apiName;
            LabelField = labelField;
            ValueField = valueField;
            Multiple = multiple;
            Component = component;
        }
    }
}
