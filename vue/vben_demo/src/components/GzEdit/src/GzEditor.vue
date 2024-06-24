<template>
  <div>
    <div style="border: 1px solid #ccc">
      <Toolbar
        style="border-bottom: 1px solid #ccc"
        :editor="editorRef"
        :defaultConfig="toolbarConfig"
        :mode="mode"
      />
      <Editor
        v-bind="$attrs"
        style="height: 500px; overflow-y: hidden"
        v-model="valueHtml"
        :defaultConfig="editorConfig"
        :mode="mode"
        @onCreated="handleCreated"
      />
    </div>
    <slot></slot>
  </div>
</template>
<script lang="ts" setup>
  import '@wangeditor/editor/dist/css/style.css'; // 引入 css
  import { onBeforeUnmount, ref, shallowRef, onMounted, watch } from 'vue';
  import { Editor, Toolbar } from '@wangeditor/editor-for-vue';
  import ConfigEditor from './ConfigEditor';
  import ConfigToolbar from './ConfigToolbar';
  import { defHttp } from '@/utils/http/axios';

  const $emit = defineEmits(['update:modelValue']);

  // 编辑器实例，必须用 shallowRef
  const editorRef = shallowRef();

  const valueHtml = ref();

  const toolbarConfig = ref(ConfigToolbar);
  const editorConfig = ref(ConfigEditor);
  const props = defineProps({
    modelValue: {
      type: String,
    },
    editorConfig: {
      type: [Object, Array],
      default: () => {},
    },
    toolbarConfig: {
      type: Object,
      default: () => {},
    },
    mode: {
      type: String,
      default: 'default',
    },
    uploadConfig: {
      type: Object,
      default: () => {
        return {
          method: 'http', // 支持custom(objurl)和http(服务器)和base64
          // url: '/api/app/',
        };
      },
    },
  });
  // watch(
  //   [() => props.modelValue, valueHtml],
  //   ([value1, value121]) => {
  //     console.log('first', props.modelValue);
  //     $emit('update:modelValue', value121);
  //   },
  //   {
  //     immediate: true,
  //   },
  // );

  watch(
    () => props.modelValue,
    (value) => {
      valueHtml.value = value;
    },
    {
      immediate: true,
    },
  );
  watch(
    valueHtml,
    (value) => {
      $emit('update:modelValue', value);
    },
    {
      immediate: true,
    },
  );
  // 组件销毁时，也及时销毁编辑器
  onBeforeUnmount(() => {
    const editor = editorRef.value;
    if (editor == null) return;
    editor.destroy();
  });

  const handleCreated = (editor) => {
    editorRef.value = editor; // 记录 editor 实例，重要！
    // let upload = {
    //   async customUpload(file, insertFn) {
    //     // 自定义上传：file 即选中的文件
    //     if (props.uploadConfig.method === 'custom') {
    //       files.forEach((file) => {
    //         var fileUrl = URL.createObjectURL(file);
    //         insert(fileUrl);
    //       });
    //     }
    //     if (props.uploadConfig.method === 'base64') {
    //       files.forEach((file) => {
    //         readBlobAsDataURL(file, function (dataurl) {
    //           insert(dataurl);
    //         });
    //       });
    //     }
    //     if (props.uploadConfig.method === 'http') {
    //       if (props.uploadConfig.callback) {
    //         props.uploadConfig.callback(files, insert);
    //       } else {
    //         // const formData = new FormData();
    //         // formData.append('File', file);
    //         // formData.append('Category ', '1');
    //         defHttp
    //           .uploadFile(
    //             {
    //               url: import.meta.env.VITE_API_URL + '/api/Shop/fileOss/uploadForm',
    //               onUploadProgress: (progressEvent) => {
    //                 info.onProgress(
    //                   {
    //                     percent: (progressEvent.loaded / progressEvent.total) * 100,
    //                   },
    //                   info.file,
    //                 ); //e.percent, file
    //               },
    //               timeout: 5 * 60 * 1000,
    //             },
    //             {
    //               file: info.file,
    //               data: {
    //                 Category: '1',
    //               },
    //             },
    //           )
    //           .then((res) => {
    //             if (res.success) {
    //               insertFn(`/api/Shop/fileOss/getPreview?fileId=${res?.data}`);
    //               // Preview({
    //               //   fileId: res.result?.fileId,
    //               // })
    //               //   .then((ress) => {
    //               //     insertFn(window.URL.createObjectURL(new Blob([ress])))
    //               //   })
    //               //   .catch((ress) => {
    //               //     message.error('上传预览错误：' + ress.message)
    //               //   })
    //             } else {
    //               message.error('编辑器上传图片失败：' + res.message);
    //             }
    //           })
    //           .catch((err) => {
    //             message.error('预览错误：' + err.message);
    //           });
    //       }
    //     }
    //   },
    // };
    // Object.assign(editorConfig.value.MENU_CONF['uploadImage'], upload);
    Object.assign(editorConfig.value, props.editorConfig);
    console.log('editorConfig.value', editorConfig.value);
    Object.assign(toolbarConfig.value, props.toolbarConfig);
  };
</script>

<style>
  .w-e-toolbar {
    flex-wrap: wrap;
  }
</style>
