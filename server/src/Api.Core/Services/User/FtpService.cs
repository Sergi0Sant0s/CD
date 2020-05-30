using System.IO;
using System.Threading.Tasks;
using Api.Model.IServices.Ftp;

namespace Api.Core.Services.User
{
    public class FtpService : IFtpManagerService
    {
        private IFtpManagerRepository _ftp;
        public FtpService(IFtpManagerRepository ftp)
        {
            _ftp = ftp;
        }

        /* Files */
        public async Task<bool> RenameFile(string username, string folderPath, string newName)
        {
            return await _ftp.RenameFileAsync(username, folderPath, newName);
        }

        public async Task<bool> UploadFile(string username, string folderPath, string name, MemoryStream file)
        {
            return await _ftp.UploadFileAsync(username, folderPath, name, file);
        }

        public async Task<MemoryStream> DownloadFile(string username, string folderPath)
        {
            return await _ftp.DownloadFileAsync(username, folderPath);
        }

        public async Task<bool> DeleteFile(string username, string filePath)
        {
            return await _ftp.DeleteFileAsync(username, filePath);
        }



        /* FOLDERS */
        public async Task<bool> DeleteFolder(string username, string folderPath)
        {
            return await _ftp.DeleteFolderAsync(username, folderPath);
        }

        public async Task<bool> NewFolder(string username, string path)
        {
            return await _ftp.NewFolderAsync(username, path);
        }

        public async Task<bool> RenameFolder(string username, string folderPath, string newName)
        {
            return await _ftp.RenameFolderAsync(username, folderPath, newName);
        }

        public async Task<object> GetByPath(string username, string path)
        {
            return await _ftp.GetByPathAsync(username, path);
        }

        public async Task<object> GetFilesByPath(string username, string path)
        {
            return await _ftp.GetFilesByPathAsync(username, path);
        }
    }
}
