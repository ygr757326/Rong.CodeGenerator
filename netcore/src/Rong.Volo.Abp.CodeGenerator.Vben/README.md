
 ## 1.在你的 AbpModule 中 依赖 CodeGeneratorModule 模块

```
  [DependsOn(
    ……
    //代码生成模块
    typeof(CodeGeneratorModule)
   ……
)]
public class YourModule : AbpModule
```

# 2.使用

依赖注入 CodeGeneratorStore 来生成

例子：创建如下控制器，运行 http://localhost:端口/code/go ,返回 ok 则生成成功
```

/// <summary>
/// 代码生成器
/// </summary>
public class CodeController : AbpController
{
    private readonly CodeGeneratorStore _codeGeneratorStore;
    public CodeController(CodeGeneratorStore codeGeneratorStore)
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

        //开始生成
        await _codeGeneratorStore.StartAsync(list, false, "命名空间名称");

        return Content("ok");
    }

}


```