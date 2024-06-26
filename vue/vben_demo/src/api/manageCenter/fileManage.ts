import { ContentType } from './../../components/Qrcode/src/typing';
import {
  File0ssAppServicePreviewThumbnailParams,
  FileGetFileInfosParams,
  FileOssAppServicePreviewParams,
  FileOssAppServiceUploadFormResult,
} from './model/fileManageModel';
import { defHttp } from '@/utils/http/axios';
import qs from 'qs';
// 文件
enum Api {
  // 原图预览
  fileOssAppServicePreview = '/api/Asset/fileOss/getPreviewUrl',
  // 下载文件base64
  getDownFileBase64 = '/api/Asset/fileOss/getDownFileBase64',
  // 缩略图预览
  file0ssAppServicePreviewThumbnail = '/api/Asset/fileOss/getThumbnailUrl',
  // 单文件上传
  fileOssAppServiceUploadForm = '/api/Asset/fileOss/uploadForm',
  // 获取文件基本信息集合
  fileGetFileInfos = '/api/Asset/file/getFileInfos',
  // 下载文件
  fileOssGetDownFileUrl = '/api/Asset/fileOss/getDownFileUrl',
}

export const getDownFileBase64 = (params: FileOssAppServicePreviewParams) =>
  defHttp.get({
    url: Api.getDownFileBase64,
    params,
  });

export const getFileOssPreview = (params: FileOssAppServicePreviewParams) =>
  defHttp.get({
    url: Api.fileOssAppServicePreview,
    params,
  });

export const getFile0ssPreviewThumbnail = (params: File0ssAppServicePreviewThumbnailParams) =>
  defHttp.get({
    url: Api.file0ssAppServicePreviewThumbnail,
    params,
  });
export const postFileOssAppServiceUploadForm = (data) => {
  const formData = new FormData();
  formData.append('file', data.file);
  formData.append('name', 'file');
  formData.append('category', '0');

  defHttp.post<FileOssAppServiceUploadFormResult>({
    url: Api.fileOssAppServiceUploadForm,
    data: formData,
    headers: {
      'Content-type': 'multipart/form-data',
    },
  });
};
export const getFileGetFileInfos = (params: FileGetFileInfosParams) =>
  defHttp.get({
    url: Api.fileGetFileInfos,
    params: params,
    paramsSerializer: function (params) {
      return qs.stringify(params, { arrayFormat: 'repeat' });
    },
  });

export const getFileOssGetDownFileUrl = (params) =>
  defHttp.get({
    url: Api.fileOssGetDownFileUrl,
    params,
  });
