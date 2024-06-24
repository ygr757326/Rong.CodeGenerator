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
  fileOssAppServicePreview = '/api/Shop/fileOss/getPreviewUrl',
  // 下载文件base64
  getDownFileBase64 = '/api/Shop/fileOss/getDownFileBase64',
  // 缩略图预览
  file0ssAppServicePreviewThumbnail = '/api/Shop/fileOss/getThumbnailUrl',
  // 单文件上传
  fileOssAppServiceUploadForm = '/api/Shop/fileOss/uploadForm',
  // 获取文件基本信息集合
  fileGetFileInfos = '/api/Shop/file/getFileInfos',
  // 下载文件
  fileOssGetDownFileUrl = '/api/Shop/fileOss/getDownFileUrl',
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
  defHttp.post<FileOssAppServiceUploadFormResult>({
    url: Api.fileOssAppServiceUploadForm,
    data,
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
