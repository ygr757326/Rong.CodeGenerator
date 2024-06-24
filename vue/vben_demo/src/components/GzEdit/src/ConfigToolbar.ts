/**
 * https://www.wangeditor.com/v5/toolbar-config.html
 *
 * toolbar.getConfig().toolbarKeys 查看当前的默认配置
 * this.editor.getAllMenuKeys() 查询编辑器注册的所有菜单 key （可能有的不在工具栏上）
 */
const ConfigToolbar = {
  //toolbarKeys: []
  //自定义菜单
  // insertKeys: {
  //     index: 5, // 插入的位置，基于当前的 toolbarKeys
  //     keys: ['menu-key1', 'menu-key2']
  // },
  //排除掉某些菜单
  excludeKeys: [],
};
export default ConfigToolbar;
