using Rong.CodeGenerator.Enums;
using System.Threading.Tasks;
using Volo.Abp.Data;

namespace Rong.CodeGenerator.EntityFrameworkCore.Seeds.Dictionarys
{
    /// <summary>
    /// 字典数据种子 - 婚姻状态
    /// </summary>
    public class Dictionary_MaritalStatus_DataSeed : DictionaryDataSeedBase
    {
        /// <summary>
        /// 婚姻状态
        /// </summary>
        protected override DictionaryTypeEnum DictionaryType => DictionaryTypeEnum.MaritalStatus;

        public override async Task SeedAsync(DataSeedContext context)
        {
            await AddAsync("1", "测试值1");
            await AddAsync("2", "测试值2");
        }
    }
}
