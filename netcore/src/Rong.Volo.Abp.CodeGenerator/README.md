
 ## 1.在你的 AbpModule 中 依赖 RongVoloAbpCodeGeneratorModule 模块

```
  [DependsOn(
    ……
    //代码生成模块
    typeof(RongVoloAbpCodeGeneratorModule)
   ……
)]
public class YourModule : AbpModule
```

## 2.使用

### 2.1 依赖注入 RongVoloAbpCodeGeneratorStore 来生成项目文件

例子：创建如下控制器，运行 http://localhost:端口/code/go ,返回 ok 则生成成功
```

/// <summary>
/// 代码生成器
/// </summary>
public class CodeController : AbpController
{
    private readonly RongVoloAbpCodeGeneratorStore _codeGeneratorStore;
    public CodeController(RongVoloAbpCodeGeneratorStore codeGeneratorStore)
    {
        _codeGeneratorStore = codeGeneratorStore;
    }

    /// <summary>
    /// 代码生成
    /// </summary>
    /// <returns></returns>
    public async Task<ActionResult> GoAsync()
    {
        List<TemplateModel> list = new List<TemplateModel>()
        {
             //new ("表名", "表名称"),
              new ("User", "用户"),
        };

        //开始生成：实体列表,命名空间名称
        await _codeGeneratorStore.StartAsync(list, "Rong.CodeGenerator", saveFolderName: "App");

        return Content("ok");
    }

}


```

### 2.2 依赖注入 RongVoloAbpCodeGeneratorDictionaryDataSeedStore 来生成字典种子数据文件（可选）

例子：创建如下控制器，运行 http://localhost:端口/codeDict/go ,返回 ok 则生成成功
```

/// <summary>
/// 字典代码生成器
/// </summary>
public class CodeDictController : AbpController
{
    private readonly RongVoloAbpCodeGeneratorDictionaryDataSeedStore _codeGeneratorStore;
    public CodeDictController(RongVoloAbpCodeGeneratorDictionaryDataSeedStore codeGeneratorStore)
    {
        _codeGeneratorStore = codeGeneratorStore;
    }

    /// <summary>
    /// 代码生成
    /// </summary>
    /// <returns></returns>+
    public async Task<ActionResult> GoAsync()
    {

        //开始生成
        await _codeGeneratorStore.StartAsync(typeof(DictionaryTypeEnum), "Rong.CodeGenerator", "EntityFrameworkCore", "EntityFrameworkCore/Seeds");

      
        return Content("ok");

    }
}


```

