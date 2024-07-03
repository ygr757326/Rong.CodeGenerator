
 ## 1.在你的 AbpModule 中 依赖 CodeGeneratorVueModule 模块

```
  [DependsOn(
    ……
    //代码生成模块
    typeof(CodeGeneratorVueModule)
   ……
)]
public class YourModule : AbpModule
```
## 2.特殊组件 和 特性

VueBoolAttribute：bool 组件

VueDictionaryAttribute： 字典组件

VueEditorAttribute：编辑器组件

VueEnumAttribute：枚举组件

VueFileAttribute：文件组件

VueTableSorterAttribute：table是否排序组件

VueTextareaAttribute：内容输入组件

VueValueNameAttribute：对象点拼接显示的属性名称, 如 student.name

针对 枚举下拉组件，字典下拉组件，bool 下拉组件，文件上传组件，文件预览组件，编辑器 组件,可通过 CodeGeneratorVueOptions 配置来指定，不指定则使用默认组件：

```
        //vue代码生成器
        Configure<CodeGeneratorVueOptions>(options =>
        {
          //Vben 组件替换
            options.ComponentMapForVben = new();

            //枚举Select
            options.EnumSelectComponent = "EnumSelect";
            options.EnumSelectComponentProp = "code";

            //枚举Radio
            options.EnumRadioComponent = "EnumRadio";
            options.EnumRadioComponentProp = "code";

            //字典Select
            options.DictionarySelectComponent = "DictSelect";
            options.DictionarySelectComponentProp = "code";

            //字典Radio
            options.DictionaryRadioComponent = "DictRadio";
            options.DictionaryRadioComponentProp = "code";

            //bool
            options.BoolSelectComponent = "BoolSelect";
            options.BoolRadioComponent = "BoolRadio";

            //文件上传
            options.FileUploadComponent = "GzUploadFile";

            //图片上传
            options.ImageUploadComponent = "GzUploadFile";

            //文件预览
            options.FilePreviewComponent = "GzUploadFile";
            options.FilePreviewComponentProp = "v-model";

            //图片预览
            options.ImagePreviewComponent = "GzImagePreview";
            options.ImagePreviewComponentProp = "fileId";

            //编辑器
            options.EditorComponent = "GzEditor";
        });

```

## 3.使用

依赖注入 CodeGeneratorVueStore 来生成

例子：创建如下控制器，运行 http://localhost:端口/codevue/go ,返回 ok 则生成成功
```
public class CodeVueController : AbpController
{

    private readonly CodeGeneratorVueStore _codeGeneratorStore;
    public CodeVueController(CodeGeneratorVueStore codeGeneratorStore)
    {
        _codeGeneratorStore = codeGeneratorStore;
    }

    /// <summary>
    /// 代码生成 - 方式1
    /// </summary>
    /// <returns></returns>+
    public async Task<ActionResult> GoAsync()
    {
        List<TemplateVueModel> list = new List<TemplateVueModel>();

        var entitys = typeof(CodeGeneratorDomainModule).Assembly.GetTypes()
            .Where(x => typeof(IEntity).IsAssignableFrom(x));

        var dtos = typeof(CodeGeneratorApplicationContractsModule).Assembly.GetTypes();

        foreach (var entity in entitys)
        {
            var name = entity.Name;
            var displayName = entity.GetCustomAttribute<DisplayAttribute>()?.Name ?? name;
            var page = dtos.FirstOrDefault(a => a.Name == $"{name}PageOutput");
            var search = dtos.FirstOrDefault(a => a.Name == $"{name}PageSearchInput");
            var create = dtos.FirstOrDefault(a => a.Name == $"{name}CreateInput");
            var update = dtos.FirstOrDefault(a => a.Name == $"{name}UpdateInput");
            var detail = dtos.FirstOrDefault(a => a.Name == $"{name}DetailOutput");
            var permission = dtos.FirstOrDefault(a => a.Name == $"{name}Permissions");
            string? permissionGroup = permission?.GetField("GroupName")?.GetValue(null)?.ToString();

            list.Add(new TemplateVueModel(name, displayName, new TemplateVueModelType()
            {
                SearchType = search,
                PageType = page,
                DetailType = detail,
                CreateType = create,
                UpdateType = update,
            }, permissionGroup));
        }


        //开始生成
        await _codeGeneratorStore.StartAsync(list, CodeGeneratorRemoteServiceConsts.RootPath, "E:\\MY\\Rong.CodeGenerator\\vue\\vben_demo");

        return Content("ok");

    }}


    /// <summary>
    /// 代码生成 - 方式二
    /// </summary>
    /// <returns></returns>+
    public async Task<ActionResult> GoAsync()
    {

        //开始生成
        await _codeGeneratorStore.StartAsync(typeof(IEntity), 
            typeof(CodeGeneratorDomainModule), 
            typeof(CodeGeneratorApplicationContractsModule), 
            CodeGeneratorRemoteServiceConsts.RootPath, 
            "E:\\MY\\Rong.CodeGenerator\\vue\\vben_demo");

        
        return Content("ok");

    }
 }


```


## 4. 页面位置和命名

1.生成的页面文件在 src/views/ 下，以“小驼峰实体名”为文件夹名：

    新增页：add.vue
    修改页：modify.vue
    主页：index.vue
    详情页：detail.vue
    详情弹框页: detailDrawer.vue
    api接口：api.ts 

2.路由： 在src/router/routes/modules/ 下,以“小驼峰实体名”命名：
  
    xxx.ts