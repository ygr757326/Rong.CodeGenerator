﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rong.CodeGenerator.App.Apps.Dto
{
    /// <summary>
    /// 创建输入
    /// </summary>
    public class AppCreateInput : AppCreateOrUpdateInputBase
    {
        /// <summary>
        /// 验证、归一化输入值
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
    }
}
