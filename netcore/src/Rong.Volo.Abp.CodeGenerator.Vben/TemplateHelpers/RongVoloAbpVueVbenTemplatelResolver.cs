using Rong.Volo.Abp.CodeGenerator.Vue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace Rong.Volo.Abp.CodeGenerator.Vue.TemplateHelpers;

/// <summary>
/// 版本解析
/// </summary>
public class RongVoloAbpVueVbenTemplatelResolver : ITransientDependency
{
    private readonly IEnumerable<IRongVoloAbpVueVbenTemplate> _versions;

    public RongVoloAbpVueVbenTemplatelResolver(IEnumerable<IRongVoloAbpVueVbenTemplate> versions)
    {
        _versions = versions;
    }

    /// <summary>
    /// 解析
    /// </summary>
    /// <param name="version">版本</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public IRongVoloAbpVueVbenTemplate Resolve(VbenVersionEnum version)
    {
        var data = _versions.FirstOrDefault(q => q.Version.Equals(version));
        if (data == null)
        {
            throw new ArgumentException("版本未找到", version.ToString());
        }

        return data;
    }
}
