// 原图查看参数
export interface FileOssAppServicePreviewParams {
  FileId: string;
}
// 缩略图查看参数
export interface File0ssAppServicePreviewThumbnailParams {
  FileId?: string;
  Width?: number;
  Height?: number;
  Quality?: number;
}
// 单文件上传参数
export interface FileOssAppServiceUploadFormParams {
  File: string;
  Category: string;
}
// 单文件上传返回参数
export interface FileOssAppServiceUploadFormResult {
  fileId: string;
  fileName: string;
  fileSuffix: string;
}
// 获取文件信息集合参数
export type FileGetFileInfosParams = {
  fileIds: string[];
};
