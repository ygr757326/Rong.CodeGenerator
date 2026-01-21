
 ## 1.在你的 AbpModule 中 依赖 RongVoloAbpCodeGeneratorVueModule 模块

```
  [DependsOn(
    ……
    //代码生成模块
    typeof(RongVoloAbpCodeGeneratorVueModule)
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

VueComponentAttribute: 使用组件

针对 枚举下拉组件，字典下拉组件，bool 下拉组件，文件上传组件，文件预览组件，编辑器 组件,可通过 CodeGeneratorVueOptions 配置来指定，不指定则使用默认组件：

```
        //vue代码生成器
        Configure<RongVoloAbpCodeGeneratorVueOptions>(options =>
        {
            //指定版本
            options.VbenVersion = VbenVersionEnum.Vben2;

            //Ant 的 Tabled 的 DataIndex 嵌套模式:。默认 Array
            //2.x 版本 为 a.b.c
            //3.x,4.x 版本 为 ['a','b','c']
            options.AntTabledDataIndexMode = AntTabledDataIndexModeEnum.Array;

            //Vben 组件替换
            options.ComponentMapForVben = new();

            //枚举Select
            options.EnumSelectComponent = "MyEnumSelect";
            options.EnumSelectComponentProp = "code";

            //枚举Radio
            options.EnumRadioComponent = "MyEnumRadio";
            options.EnumRadioComponentProp = "code";

             //枚举Checkbox
            options.EnumCheckboxComponent = "MyEnumCheckbox";
            options.EnumCheckboxComponentProp = "code";

            //字典Select
            options.DictionarySelectComponent = "MyDictSelect";
            options.DictionarySelectComponentProp = "code";

            //字典Radio
            options.DictionaryRadioComponent = "MyDictRadio";
            options.DictionaryRadioComponentProp = "code";

            //字典Checkbox
            options.DictionaryCheckboxComponent = "MyDictCheckbox";
            options.DictionaryCheckboxComponentProp = "code";

            //bool
            options.BoolSelectComponent = "MyBoolSelect";
            options.BoolRadioComponent = "MyBoolRadio";

            //文件上传
            options.FileUploadComponent = "MyUploadFile";

            //图片上传
            options.ImageUploadComponent = "MyUploadFile";

            //文件预览
            options.FilePreviewComponent = "MyFileViewList";
            options.FilePreviewComponentProp = "fileList";

            //图片预览
            options.ImagePreviewComponent = "MyImagePreview";
            options.ImagePreviewComponentProp = "fileId";

            //编辑器
            options.EditorComponent = "MyEditor";

            //使用组件进行展示 prop,默认 value
            options.VueComponentViewProp = "value";
        });

```

## 3.使用

依赖注入 RongVoloAbpCodeGeneratorVueStore 来生成

例子：创建如下控制器，运行 http://localhost:端口/codevue/go ,返回 ok 则生成成功
```
public class CodeVueController : AbpController
{

    private readonly RongVoloAbpCodeGeneratorVueStore _codeGeneratorStore;
    public CodeVueController(RongVoloAbpCodeGeneratorVueStore codeGeneratorStore)
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
        await _codeGeneratorStore.StartAsync(list, CodeGeneratorRemoteServiceConsts.RootPath, "E:\\MY\\Rong.CodeGenerator\\vue\\vben_demo", new []{"app"});

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
            "E:\\MY\\Rong.CodeGenerator\\vue\\vben_demo",
            //要忽略生成的实体
            new []{typeof(App)});

        
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