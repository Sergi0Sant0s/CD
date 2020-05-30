using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Api.Model.IServices.Ftp
{
    public interface IFtpManagerRepository
    {
        /*Files*/
        Task<bool> RenameFileAsync(string username, string folderPath, string newName);
        Task<bool> UploadFileAsync(string username, string folderPath, string name, MemoryStream file);
        Task<MemoryStream> DownloadFileAsync(string username, string folderPath);
        Task<bool> DeleteFileAsync(string username, string filePath);


        /*Folders*/
        Task<bool> RenameFolderAsync(string username, string folderPath, string newName);
        Task<bool> NewFolderAsync(string username, string path);
        Task<bool> DeleteFolderAsync(string username, string folderPath);

        /* GET ALL */
        Task<object> GetByPathAsync(string username, string path);

        Task<object> GetFilesByPathAsync(string username, string path);
    }
}
