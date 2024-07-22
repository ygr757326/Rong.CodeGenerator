using Rong.CodeGenerator.Enums;
using System.Threading.Tasks;
using Volo.Abp.Data;

namespace Rong.CodeGenerator.EntityFrameworkCore.Seeds.Dictionarys
{
    /// <summary>
    /// 字典数据种子 - 民族
    /// </summary>
    public class Dictionary_Nation_DataSeed : DictionaryDataSeedBase
    {
        /// <summary>
        /// 民族
        /// </summary>
        protected override DictionaryTypeEnum DictionaryType => DictionaryTypeEnum.Nation;

        public override async Task SeedAsync(DataSeedContext context)
        {
            await AddAsync("1", "测试值1");
            await AddAsync("2", "测试值2");
        }
    }
}
