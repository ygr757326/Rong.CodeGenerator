// import Vue from 'vue';FileUploadCateV
// import { FileUploadCate } from '@/components/_myComponents/Upload/FileUploadCate';
// import { UploadForm, Preview } from '@/api/modular/main/CommonManage';
import { message } from 'ant-design-vue';
import {
  // getFile0ssPreviewThumbnail,
  // getFileGetFileInfos,
  // getFileOssPreview,
  getDownFileBase64,
  postFileOssAppServiceUploadForm,
} from '@/api/manageCenter/fileManage';

import { defHttp } from '@/utils/http/axios';
/**
 * https://www.wangeditor.com/v5/menu-config.html#%E8%87%AA%E5%AE%9A%E4%B9%89%E5%8A%9F%E8%83%BD
 */
const ConfigEditor = {
  placeholder: '请输入内容...',
  MENU_CONF: {},
};

/**
 * 上传图片的配置
 */
ConfigEditor.MENU_CONF['uploadImage'] = {
  // server: '/api/Shop/fileOss/uploadForm',
  allowedFileTypes: ['image/*'], //如不想限制，则设置为 []
  fieldName: 'file',
  maxFileSize: 1024 * 1024 * 10, //mb
  maxNumberOfFiles: 100,
  timeout: 1000 * 20, //秒
  async customUpload(file, insertFn) {
    defHttp
      .uploadFile(
        {
          url: import.meta.env.VITE_API_URL + '/api/Shop/fileOss/uploadForm',
        },
        {
          file: file,
          data: {
            Category: '1',
          },
        },
      )
      .then((r) => {
        console.log(r);
        const { data } = r;
        console.log(data);
        getDownFileBase64({ FileId: data.fileId })
          .then((res) => {
            const src = res.base64;
            insertFn('data:image/png;base64,' + src);

            // this.$viewerApi({
            //   images: [src],
            // })
          })
          .catch((err) => {
            reject(err);
          });
      });
    return;
    const formData = new FormData();
    formData.append('file', file);
    formData.append('category', '1');

    // console.log('file', formData.get('file'));
    postFileOssAppServiceUploadForm(formData)
      .then((res) => {
        if (res.success) {
          insertFn(`/api/Shop/fileOss/getPreview?fileId=${res?.result?.fileId}`);
          // Preview({
          //   fileId: res.result?.fileId,
          // })
          //   .then((ress) => {
          //     insertFn(window.URL.createObjectURL(new Blob([ress])))
          //   })
          //   .catch((ress) => {
          //     message.error('上传预览错误：' + ress.message)
          //   })
        } else {
          message.error('编辑器上传图片失败：' + res.message);
        }
      })
      .catch((err) => {
        message.error('预览错误：' + err.message);
      });
  },
};
/**
 *  上传视频的配置
 */
ConfigEditor.MENU_CONF['uploadVideo'] = {
  server: '/api/Shop/fileOss/uploadForm',
  allowedFileTypes: ['video/*'], //如不想限制，则设置为 []
  fieldName: 'file',
  maxFileSize: 1024 * 1024 * 100, //mb
  maxNumberOfFiles: 100,
  timeout: 1000 * 60 * 20, //秒

  // 自定义上传
  //  async customUpload(file, insertFn) {                   // JS 语法
  //     // file 即选中的文件
  //     // 自己实现上传，并得到视频 url poster
  //     // 最后插入视频
  //     insertFn(url, poster)
  // }
};
export default ConfigEditor;
