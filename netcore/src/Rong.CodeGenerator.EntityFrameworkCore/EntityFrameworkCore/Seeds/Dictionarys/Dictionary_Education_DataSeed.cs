using Rong.CodeGenerator.Enums;
using System.Threading.Tasks;
using Volo.Abp.Data;

namespace Rong.CodeGenerator.EntityFrameworkCore.Seeds.Dictionarys
{
    /// <summary>
    /// 字典数据种子 - 学历
    /// </summary>
    public class Dictionary_Education_DataSeed : DictionaryDataSeedBase
    {
        /// <summary>
        /// 学历
        /// </summary>
        protected override DictionaryTypeEnum DictionaryType => DictionaryTypeEnum.Education;

        public override async Task SeedAsync(DataSeedContext context)
        {
            await AddAsync("1", "测试值1");
            await AddAsync("2", "测试值2");
        }
    }
}
