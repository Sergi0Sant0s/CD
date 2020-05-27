using System.IO;
using System.Threading.Tasks;

namespace Api.Model.IServices.Ftp
{
    public interface IFtpManagerService
    {
        /*Files*/
        Task<bool> RenameFile(string username, string folderPath, string oldName, string newName);
        Task<bool> UploadFile(string username, string folderPath, string name, MemoryStream file);
        Task<MemoryStream> DownloadFile(string username, string folderPath);
        Task<bool> DeleteFile(string username, string filePath);


        /*Folders*/
        Task<bool> RenameFolder(string username, string folderPath, string oldName, string newName);
        Task<bool> NewFolder(string username, string path);
        Task<bool> DeleteFolder(string username, string folderPath);

        /* GET ALL */
        Task<object> GetByPath(string username, string path);

        Task<object> GetFilesByPath(string username, string path);
    }
}
