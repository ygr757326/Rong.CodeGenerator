using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using Rong.Volo.Abp.CodeGenerator.Vue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers.Vben5
{
    /// <summary>
    /// vben代码生成器帮助器
    /// </summary>
    [ExposeServices(typeof(IRongVoloAbpVueVbenTemplate), typeof(RongVoloAbpVueVben5Template))]
    public class RongVoloAbpVueVben5Template : IRongVoloAbpVueVbenTemplate
    {
        public VbenVersionEnum Version => VbenVersionEnum.Vben5;

        protected RongVoloAbpVueVben5TemplateStringOfForm FormTemplate;
        protected RongVoloAbpVueVben5TemplateStringOfTableColumns TableColumnsTemplate;
        protected RongVoloAbpVueVben5TemplateStringOfTableSchemas TableSchemasTemplate;
        protected RongVoloAbpVueVben5TemplateStringOfDetail DetailTemplate;

        public RongVoloAbpVueVben5Template(
            RongVoloAbpVueVben5TemplateStringOfForm form,
            RongVoloAbpVueVben5TemplateStringOfTableColumns tableColumnsTemplate,
            RongVoloAbpVueVben5TemplateStringOfTableSchemas tableSchemasTemplate,
            RongVoloAbpVueVben5TemplateStringOfDetail detailTemplate
        )
        {
            FormTemplate = form;
            TableColumnsTemplate = tableColumnsTemplate;
            TableSchemasTemplate = tableSchemasTemplate;
            DetailTemplate = detailTemplate;
        }

        /// <summary>
        /// 获取table表头列插槽 模板
        /// </summary>
        /// <param name="models"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        public virtual string GetTableColumnsSlotsTemplate(List<TemplateVueEntityPropertyData> models, int space = 8)
        {
            StringBuilder b = new StringBuilder();
            b.AppendLine();

            foreach (var item in models)
            {
                var typeCode = item.PropertyType.GetMyTypeCode();

                if (!item.IsSlot)
                {
                    continue;
                }
                if (item.IsEnum)
                {
                    b.Append(TableColumnsTemplate.EnumSlots(item, space));
                }
                else if (item.IsDictionary)
                {
                    b.Append(TableColumnsTemplate.DictionarySlot(item, space));
                }
                else if (new[] { TypeCode.Boolean }.Contains(typeCode))
                {
                    b.Append(TableColumnsTemplate.BoolSlot(item, space));
                }
                else if (item.IsFile)
                {
                    b.Append(TableColumnsTemplate.FilePreviewSlot(item, space));
                }
                else if (item.IsImage)
                {
                    b.Append(TableColumnsTemplate.ImagePreviewSlot(item, space));
                }
                else if (item.IsEditor)
                {
                    b.Append(TableColumnsTemplate.EditorSlot(item, space));
                }
            }

            return b.ToString();
        }

        /// <summary>
        /// 获取vue详情页 模板
        /// </summary>
        /// <returns></returns>
        public virtual string? GetDetailTemplate(List<TemplateVueEntityPropertyData> models, int space = 8)
        {
            if (models == null)
            {
                return null;
            }

            StringBuilder b = new StringBuilder();
            b.AppendLine();

            foreach (var item in models)
            {
                var typeCode = item.PropertyType.GetMyTypeCode();

                if (item.IsDate)
                {
                    b.Append(DetailTemplate.DateTimeTemplate(item, space));
                }
                else if (item.IsEnum)
                {
                    b.Append(DetailTemplate.EnumTemplate(item, space));
                }
                else if (item.IsDictionary)
                {
                    b.Append(DetailTemplate.DictionaryTemplate(item, space));
                }
                else if (new[] { TypeCode.Boolean }.Contains(typeCode))
                {
                    b.Append(DetailTemplate.BoolTemplate(item, space));
                }
                else if (item.IsFile)
                {
                    b.Append(DetailTemplate.FilePreviewTemplate(item, space));
                }
                else if (item.IsImage)
                {
                    b.Append(DetailTemplate.ImagePreviewTemplate(item, space));
                }
                else if (item.IsEditor)
                {
                    b.Append(DetailTemplate.EditorTemplate(item, space));
                }
                else
                {
                    b.Append(DetailTemplate.DefaultTemplate(item, space));
                }
            }
            return b.ToString();
        }

        /// <summary>
        /// 获取table表头列 模板
        /// </summary>
        /// <returns></returns>
        public virtual string? GetTableColumnsTemplate(List<TemplateVueEntityPropertyData> models, int space = 4)
        {
            if (models == null)
            {
                return null;
            }

            StringBuilder b = new StringBuilder();
            foreach (var item in models)
            {
                var typeCode = item.PropertyType.GetMyTypeCode();

                if (item.IsDate)
                {
                    b.Append(TableColumnsTemplate.DateTimeTemplate(item, space));
                }
                else if (item.IsEnum)
                {
                    b.Append(TableColumnsTemplate.EnumTemplate(item, space));
                }
                else if (item.IsDictionary)
                {
                    b.Append(TableColumnsTemplate.DictionaryTemplate(item, space));
                }
                else if (new[] { TypeCode.Boolean }.Contains(typeCode))
                {
                    b.Append(TableColumnsTemplate.BoolTemplate(item, space));
                }
                else if (item.IsFile)
                {
                    b.Append(TableColumnsTemplate.FilePreviewTemplate(item, space));
                }
                else if (item.IsImage)
                {
                    b.Append(TableColumnsTemplate.ImagePreviewTemplate(item, space));
                }
                else
                {
                    b.Append(TableColumnsTemplate.DefaultTemplate(item, space));
                }
            }

            return b.ToString();
        }

        /// <summary>
        /// 获取table表单 模板
        /// </summary>
        /// <returns></returns>
        public virtual string? GetTableSchemasTemplate(List<TemplateVueEntityPropertyData> models, int space = 8)
        {
            if (models == null)
            {
                return null;
            }
            StringBuilder b = new StringBuilder();

            foreach (var item in models)
            {
                var typeCode = item.PropertyType.GetMyTypeCode();

                if (item.IsDate)
                {
                    b.Append(TableSchemasTemplate.DateTimeTemplate(item, space));
                }
                else if (item.IsEnum)
                {
                    b.Append(TableSchemasTemplate.EnumTemplate(item, space));
                }
                else if (new[]
                         {
                             TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Byte, TypeCode.Int16,
                             TypeCode.Int32, TypeCode.Int64, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                         }.Contains(typeCode))
                {
                    b.Append(TableSchemasTemplate.NumberTemplate(item, space));
                }
                else if (new[] { TypeCode.Boolean }.Contains(typeCode))
                {
                    b.Append(TableSchemasTemplate.BoolTemplate(item, space));

                }
                else if (item.IsDictionary)
                {
                    b.Append(TableSchemasTemplate.DictionaryTemplate(item, space));
                }
                else if (item.IsComponent)
                {
                    b.Append(TableSchemasTemplate.ComponentTemplate(item, space));
                }
                else
                {
                    b.Append(TableSchemasTemplate.DefaultTemplate(item, space));
                }
            }

            return b.ToString();
        }

        /// <summary>
        /// 获取新增编辑表单 模板
        /// </summary>
        /// <returns></returns>
        public virtual string? GetFormTemplate(List<TemplateVueEntityPropertyData> models, int space = 4)
        {
            if (models == null)
            {
                return null;
            }
            StringBuilder b = new StringBuilder();

            foreach (var item in models)
            {
                var typeCode = item.PropertyType.GetMyTypeCode();

                if (item.IsDate)
                {
                    b.Append(FormTemplate.DateTimeTemplate(item, space));
                }
                else if (item.IsEnum)
                {
                    b.Append(FormTemplate.EnumTemplate(item, space));
                }
                else if (new[]
                         {
                             TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Byte, TypeCode.Int16,
                             TypeCode.Int32, TypeCode.Int64, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                         }.Contains(typeCode))
                {
                    b.Append(FormTemplate.NumberTemplate(item, space));
                }
                else if (new[] { TypeCode.Boolean }.Contains(typeCode))
                {
                    b.Append(FormTemplate.BoolTemplate(item, space));

                }
                else if (item.IsDictionary)
                {
                    b.Append(FormTemplate.DictionaryTemplate(item, space));
                }
                else if (item.IsFile)
                {
                    b.Append(FormTemplate.FileUploadTemplate(item, space));
                }
                else if (item.IsImage)
                {
                    b.Append(FormTemplate.ImageUploadTemplate(item, space));
                }
                else if (item.IsTextarea)
                {
                    b.Append(FormTemplate.TextareaTemplate(item, space));
                }
                else if (item.IsEditor)
                {
                    b.Append(FormTemplate.EditorTemplate(item, space));
                }
                else if (item.IsComponent)
                {
                    b.Append(FormTemplate.ComponentTemplate(item, space));
                }
                else
                {
                    b.Append(FormTemplate.DefaultTemplate(item, space));
                }
            }

            return b.ToString();
        }
    }
}