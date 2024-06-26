<template>
  <div>
    <!-- :preview-file="previewFile" 
       @preview="preview" 
            v-on="listeners"
      -->
    <a-upload
      :accept="accept"
      v-bind="$attrs"
      v-bind:value="currentValue"
      :customRequest="customRequest"
      :multiple="multiple"
      :name="name"
      :list-type="listType"
      :file-list="myFileList"
      @change="change"
      @preview="previewNew"
      :before-upload="beforeUpload"
      :show-upload-list="showUploadList"
    >
      <div v-if="listType == 'picture-card'">
        <PlusOutlined />
        <div class="ant-upload-text">选择文件</div>
      </div>
      <div v-else-if="listType == 'text'">
        <a-button :icon="h(PlusOutlined)">选择文件 </a-button>
      </div>
    </a-upload>
    <!-- <FileViewModal
      :visible="previewData.visible"
      :type="viewType"
      :src="previewData.fileId"
      :name="previewData.name"
      @cancel="cancelPreview"
    ></FileViewModal> -->
    <a-modal :visible="previewData.visible" :footer="null" @cancel="cancelPreview">
      <a-spin :spinning="previewData.loading">
        <img style="width: 100%" :src="previewData.fileId" />
      </a-spin>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
  import { message } from 'ant-design-vue';
  import { reactive, ref, watch, h } from 'vue';
  import { FileUploadCate } from '../FileUploadCate.js';
  import { PlusOutlined } from '@ant-design/icons-vue';
  import {
    getFile0ssPreviewThumbnail,
    getFileGetFileInfos,
    getFileOssPreview,
  } from '@/api/manageCenter/fileManage';
  import { defHttp } from '@/utils/http/axios';
  import { log } from 'console';
  //  import { FileViewModal } from '@/components/_myComponents/FileView';

  const $emit = defineEmits(['error', 'success', 'update:modelValue']);
  const props = defineProps({
    /**
     * v-model:value 是否为 fileId
     * */
    fileIdValue: {
      type: Boolean,
      default: () => true,
    },
    /**
     * 文件分类
     */
    cate: {
      type: Number,
      default: FileUploadCate.Default,
    },
    /**
     * 最大文件大小 MB
     */
    maxSize: {
      type: Number,
      default: () => 0,
    },
    //----------------------以上为自定义------------------------------------
    /**
     * v-model
     */
    modelValue: {
      type: [String, Array, Object],
      default: null,
    },
    listType: {
      type: String,
      default: () => 'picture-card',
    },
    name: {
      type: String,
      default: () => 'file',
    },
    accept: {
      type: String,
      default: () => null,
    },
    multiple: {
      type: Boolean,
      default: () => false,
    },
    showUploadList: {
      type: Boolean,
      default: () => true,
    },
    /**
          * uid: '-1',
            name: 'xxx.png',
            status: 'done',//uploading done error removed
            url: 'http://www.baidu.com/xxx.png',
          // */
    fileList: {
      type: Array,
      default: () => null,
    },
    //  viewType: {
    //    type: 'modal' | 'drawer' | 'open',
    //  }
  });
  let previewData = reactive({
    fileId: '',
    fileName: null,
    visible: false,
    loading: true,
  });
  let currentValue = ref<String | Array<string> | Object>('');
  let uploading = ref<boolean>(false);
  let myFileList: any = ref([]);
  let fileInfos: any = ref([]);
  let url = ref<string>('/api/Asset/fileOss/uploadForm');

  watch(
    () => props.modelValue,
    (newValue: any) => {
      console.log('文件上传：' + newValue);
      currentValue.value = newValue;
      // 防止文件列表累计
      // myFileList.value = [];
      if (!newValue) {
        myFileList.value = [];
      }
      setFileListByValue(newValue);
    },
    {
      immediate: true,
      deep: true,
    },
  );

  /**
   * 上传状态改变：file.status 状态有：uploading done error removed
   */
  function change(info) {
    //设置并显示文件
    setChangeFileList(info);
    //触发自定义方法
    if (isError(info.file)) {
      //上传错误
      $emit('error', info.file, info.fileList);
    } else if (isDone(info.file)) {
      //上传成功
      $emit('success', info.file, info.fileList);
      $emit('update:modelValue', setChangeValue(info));
      console.log('上传成功', info);
    } else if (isRemoved(info.file)) {
      $emit('update:modelValue', setChangeValue(info));
      console.log('文件移除', info);
    }
  }
  /**
   * 当 fileIdValue 设为 true 时，获取响应后的fileId集合，作为双向绑定数据
   * @param {'{file,fileList}'} info
   */
  function setChangeValue(info) {
    if (!info) {
      return null;
    }

    if (isUploading(info.file)) {
      return info;
    }

    if (props.fileIdValue) {
      //多张
      if (props.multiple) {
        return info.fileList?.filter((a) => isDone(a)).map((a) => a?.response?.fileId || a?.uid);
      }
      //一张
      if (isDone(info.file)) {
        return info.file?.response?.fileId;
      }

      return null;
    } else {
      return info.fileList.length == 0 ? null : info;
    }
  }
  /**
   * 设置上传时的：文件列表
   */
  function setChangeFileList(info) {
    if (!props.multiple && info.fileList && info.fileList.length > 1) {
      info.fileList = info.fileList.filter((item) => item.uid === info.file.uid);
    }
    myFileList.value = info.fileList;
  }
  /**
   * 设置赋值时的：文件列表
   */
  async function setFileListByValue(value) {
    if (!value || !props.fileIdValue || value.length == 0) {
      return;
    }

    let fileIds: any = [];
    if (value instanceof Array) {
      fileIds = !props.multiple ? [value[0]] : [...value];
    } else if (typeof value == 'string') {
      fileIds = [value];
    }

    await GetFileIdNames(fileIds);

    fileIds.forEach((id) => {
      setMyFileList(id);
    });
  }

  async function setMyFileList(fileId) {
    if (!fileId || !props.fileIdValue) {
      return;
    }
    let index = myFileList.value.findIndex((item) => item.uid === fileId);
    let index1 = myFileList.value.findIndex((item) => item.response?.fileId === fileId);
    let list: any = [];
    if (index == -1 && index1 == -1) {
      var url = await PreviewThumbnail(fileId);
      list.push({
        uid: fileId,
        name: getFileNameByPath(fileId),
        status: 'done',
        url: url,
        thumbUrl: url,
        //type: "image/jpeg"
        //size:0
      });
    }
    myFileList.value = [...myFileList.value, ...list];
  }

  //  function  removeFromFileList(file) {
  //    let index = myFileList.value.findIndex((item) => item.uid === file.uid);
  //    if (index != -1) {
  //      myFileList.value.splice(index, 1);
  //    }
  //  }
  /**
   * 验证文件大小
   * @param {*} file
   */
  function validFileSize(file) {
    if (props.maxSize <= 0) return true;
    const isOK = file.size / 1024 / 1024 < props.maxSize;
    if (!isOK) {
      message.error(`文件大小不能超过 ${props.maxSize} MB：${file.name}`);
    }
    return isOK;
  }
  /**
   * 验证文件类型
   * @param {文件} file
   */
  function validFileType(file: any) {
    return true;
    // if (!this.accept) return true
    // const isOK = this.accept.indexOf(file.type) != -1
    // if (!isOK) {
    //   this.$message.error(`只能上传格式为【${this.accept}】的文件：${file.name}`)
    // }
    // return isOK
  }
  /**
   * 文件是否验证
   * @param {} file
   */
  function isValid(file) {
    return validFileType(file) && validFileSize(file);
  }
  /**
   * 上传之前处理
   */
  function beforeUpload(file) {
    if (!isValid(file)) {
      return false;
    }
    return true;
  }
  /**
   * 自定义上传
   * { action, file, onSuccess, onError, onProgress }
   */
  function customRequest(info) {
    uploading.value = true;
    //上传
    defHttp
      .uploadFile(
        {
          url: import.meta.env.VITE_GLOB_API_URL + url.value,
          onUploadProgress: (progressEvent) => {
            info.onProgress(
              {
                percent: (progressEvent.loaded / progressEvent.total) * 100,
              },
              info.file,
            ); //e.percent, file
          },
          timeout: 5 * 60 * 1000,
        },
        {
          file: info.file,
          data: {
            Category: props.cate,
          },
        },
      )
      .then((res: any) => {
        info.onSuccess(res.data, info.file); //response, file, xhr
      })
      .catch((res) => {
        info.onError(res, res, info.file); //error, response, file
      })
      .finally(() => {
        uploading.value = false;
      });
  }
  function isDone(file) {
    return file?.status == 'done';
  }
  function isError(file) {
    return file?.status == 'error';
  }
  function isUploading(file) {
    return file?.status == 'uploading';
  }
  function isRemoved(file) {
    return file?.status == 'removed';
  }
  /**
   * 点击文件链接或预览图标时的回调
   */
  // async function previewNew(file) {

  //   previewData.visible = true;
  //   previewData.fileName = file.name;

  //   previewData.loading = true;

  //   if (file.response?.fileId) {
  //     previewData.fileId = file.response?.fileId;
  //   } else {
  //     previewData.fileId = file.uid;
  //   }

  //   previewData.loading = false;
  // }

  /**
   * 点击文件链接或预览图标时的回调
   */
  async function previewNew(file) {
    if (props.listType == 'picture-card') {
      previewData.visible = true;
      previewData.loading = true;
      previewData.fileName = file.name;

      if (file.response) {
        if (!file.url && !file.preview) {
          file.preview = await getBase64(file.originFileObj);
        }
        previewData.fileId = file.url || file.preview;
      } else {
        file.preview = await goPreview(file.uid);

        previewData.fileId = file.preview;
      }

      previewData.loading = false;
    }
    // previewData.visible = true;
    //   previewData.loading = true;
    //   previewData.fileName = file.name;

    //   if (file.response) {
    //     if (!file.url && !file.preview) {
    //       file.preview = await getBase64(file.originFileObj);
    //     }
    //     previewData.fileId = file.url || file.preview;
    //   } else {
    //     file.preview = await goPreview(file.uid);

    //     previewData.fileId = file.preview;
    //   }

    //   previewData.loading = false;
  }
  /**
   * 取消预览
   */
  function cancelPreview() {
    previewData.visible = false;
  }
  /**
   * 获取文件base64编码
   */
  function getBase64(file) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      // 转化为base64
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });
  }
  /**
   * 缩略图
   */
  function PreviewThumbnail(fileId) {
    return new Promise((resolve, reject) => {
      if (!IsOssFile(fileId)) {
        resolve(fileId);
        return;
      }
      getFile0ssPreviewThumbnail({ FileId: fileId })
        .then((res) => {
          resolve(res.url);
          // this.getBase64(new Blob([res])).then((r) => {
          //   resolve(r)
          // })
        })
        .catch((error) => {
          reject(error);
        });
    });
  }
  // /**
  //  * 转图片类型
  //  */
  // function createObjectURL(res) {
  //   const blob = new Blob([res]);
  //   return window.URL.createObjectURL(blob);
  // }

  /**
   * 原图
   * @param {*} fileId
   */
  function goPreview(fileId) {
    return new Promise((resolve, reject) => {
      if (!fileId) {
        return;
      }
      if (!IsOssFile(fileId)) {
        resolve(fileId);
        return;
      }
      getFileOssPreview({ FileId: fileId })
        .then((res) => {
          let src = res.url;
          resolve(src);

          // this.$viewerApi({
          //   images: [src],
          // })
        })
        .catch((err) => {
          reject(err);
        });
    });
  }
  /**
   * 自定义上传时的本地预览，用于处理非图片格式文件（例如视频文件）
   */
  // function previewFile(file) {
  //   return new Promise((resolve) => {
  //     resolve('thumbUrl');
  //   });
  // }
  function IsOssFile(fileId) {
    if (!fileId) return false;

    var guid = fileId.match(/^[0-9a-z]{8}-[0-9a-z]{4}-[0-9a-z]{4}-[0-9a-z]{4}-[0-9a-z]{12}$/i);
    return !!guid;
  }
  /**
   * 获取文件id信息集合
   */
  async function GetFileIdNames(fileIds) {
    var ids = fileIds.filter((fileId) => IsOssFile(fileId));
    if (fileIds.length == 0 || ids.length == 0 || fileInfos.value?.length > 0) {
      return;
    }
    fileInfos.value = [];
    let list = await getFileGetFileInfos({ fileIds: ids });
    fileInfos.value = list;
  }
  /**
   * 获取文件名称
   */
  function getFileNameByPath(path) {
    var name = fileInfos.value?.find(
      (a) => a.id.toUpperCase() == path.toUpperCase(),
    )?.fileOriginName;
    if (name) {
      return name;
    } else {
      var pos1 = path.lastIndexOf('/');
      var pos2 = path.lastIndexOf('\\');
      var pos = Math.max(pos1, pos2);
      if (pos < 0) return path;
      else return path.substring(pos + 1);
    }
  }
</script>
