using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Model.IServices.Ftp;

namespace Api.Data.Ftp
{
    public class FtpManager : IFtpManagerRepository
    {
        private string basepath = AppContext.BaseDirectory + "ftp\\";

        public string Basepath { get => basepath; }


        /// <summary>
        /// Apagar um arquivo
        /// </summary>
        /// <param name="username">Nome do user que quer apagar o arquivo</param>
        /// <param name="filePath">Caminho/Diretório do arquivo</param>
        /// <returns>Retorna se o arquivo foi apagado</returns>
        public async Task<bool> DeleteFileAsync(string username, string filePath)
        {
            string fullPath = basepath + username + @"\" + filePath;

            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                    return true;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return false;
        }

        /// <summary>
        /// Apagar uma pasta
        /// </summary>
        /// <param name="username">Nome do user que quer apagar a pasta</param>
        /// <param name="folderPath">Caminho/Diretório da pasta</param>
        /// <returns>Retorna se a pasta foi apagada</returns>
        public async Task<bool> DeleteFolderAsync(string username, string folderPath)
        {
            string fullPath = basepath + username + @"\" + folderPath;

            if (Directory.Exists(fullPath))
            {
                try
                {
                    Directory.Delete(fullPath);
                    return true;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return false;
        }


        /// <summary>
        /// Criar uma pasta
        /// </summary>
        /// <param name="username">Nome do user que está a criar a pasta</param>
        /// <param name="path">Caminho/Diretório que queremos para a pasta</param>
        /// <returns>Retorna se a pasta foi criada</returns>
        public async Task<bool> NewFolderAsync(string username, string path)
        {
            string fullPath = basepath + username + path;

            if (!Directory.Exists(fullPath))
            {
                try
                {
                    Directory.CreateDirectory(fullPath);
                    return true;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return false;
        }

        /// <summary>
        /// /// Renomear um arquivo 
        /// </summary>
        /// <param name="username">Username criador do arquivo</param>
        /// <param name="folderPath">Caminho/Diretório do arquivo</param>
        /// <param name="oldName">Nome do arquivo</param>
        /// <param name="newName">Novo nome para o arquivo</param>
        /// <returns>Retorna se foi possivel renomear o arquivo</returns>
        public async Task<bool> RenameFileAsync(string username, string folderPath, string newName)
        {
            string oldFullPath = basepath + username + folderPath;
            string aux;
            while (folderPath[folderPath.Length - 1] != '\\')
            {
                aux = folderPath.Substring(0, folderPath.Length - 1);
                folderPath = aux;
            }
            string newFullPath = basepath + username + @"\" + folderPath + @"\" + newName;

            if (File.Exists(oldFullPath))
            {
                try
                {
                    File.Move(oldFullPath, newFullPath);
                    return true;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return false;
        }

        /// <summary>
        /// Renomear uma pasta
        /// </summary>
        /// <param name="username">Username criador da pasta</param>
        /// <param name="folderPath">Caminho/Diretório da pasta</param>
        /// <param name="oldName">Nome da pasta</param>
        /// <param name="newName">Novo nome para a pasta</param>
        /// <returns>Retorna se foi possivel renomear a pasta</returns>
        public async Task<bool> RenameFolderAsync(string username, string folderPath, string newName)
        {
            string oldFullPath, newFullPath;
            string temp = folderPath, aux;
            while (temp[temp.Length - 1] != '\\')
            {
                aux = temp.Substring(0, temp.Length - 1);
                temp = aux;
            }
            oldFullPath = basepath + username + folderPath;
            newFullPath = basepath + username + temp + newName;


            if (Directory.Exists(oldFullPath))
            {
                try
                {
                    Directory.Move(oldFullPath, newFullPath);
                    return true;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return false;
        }


        /// <summary>
        /// Upload de arquivos
        /// </summary>
        /// <param name="username">Username do criador do upload</param>
        /// <param name="folderPath">Caminho/Diretório do arquivo</param>
        /// <param name="name">Nome do arquivo</param>
        /// <param name="file">Arquivo</param>
        /// <returns>Retorna se foi possivel fazer o upload</returns>
        public async Task<bool> UploadFileAsync(string username, string folderPath, string name, MemoryStream file)
        {
            string filePath = basepath + username + folderPath + "\\" + name;

            if (!File.Exists(filePath))
            {
                try
                {
                    file.Seek(0, SeekOrigin.Begin);

                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    return true;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return false;
        }


        /// <summary>
        /// Download de um arquivo
        /// </summary>
        /// <param name="username">Username do criador do arquivo</param>
        /// <param name="folderPath">Caminho/Diretório do arquivo</param>
        /// <returns>Download do arquivo</returns>
        public async Task<MemoryStream> DownloadFileAsync(string username, string folderPath)
        {
            string fullPath = basepath + username + @"\" + folderPath;

            if (File.Exists(fullPath))
            {
                try
                {
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(fullPath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    return memory;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        /// Obter um arquivo pelo seu caminho/diretorio
        /// </summary>
        /// <param name="username">Username do criador do arquivo</param>
        /// <param name="path">Caminho/Diretório do arquivo</param>
        /// <returns>Retorna uma lista com todos os arquivos no diretorio escolhido</returns>
        public async Task<object> GetByPathAsync(string username, string path)
        {
            string fullPath = path == "\\" ? basepath + username + @"\" : basepath + username + path;

            if (Directory.Exists(fullPath))
            {
                try
                {
                    var allFolders = Directory.GetDirectories(fullPath).Select(p => new
                    {
                        Path = p,
                        Name = Path.GetFileName(p)
                    }).ToArray();
                    var allFiles = Directory.GetFiles(fullPath).Select(p => new
                    {
                        Path = p,
                        Name = Path.GetFileName(p)
                    }).ToArray();
                    List<object> listFolders = new List<object>();
                    List<object> listFiles = new List<object>();

                    foreach (var aux in allFolders)
                    {
                        string tempName = aux.Name;
                        string tempPath = path == @"\\" ? aux.Path.Replace(basepath + username + @"\", "") : aux.Path.Replace(basepath + username, "");
                        //
                        listFolders.Add(new
                        {
                            name = tempName,
                            path = tempPath
                        });
                    }

                    foreach (var aux in allFiles)
                    {
                        string tempName = aux.Name;
                        string tempPath = path == @"\\" ? aux.Path.Replace(basepath + username + @"\", "") : aux.Path.Replace(basepath + username, "");

                        listFiles.Add(new
                        {
                            name = tempName,
                            path = tempPath
                        });
                    }



                    return new
                    {
                        folders = listFolders,
                        files = listFiles
                    };

                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return null;
        }

        public async Task<object> GetFilesByPathAsync(string username, string path)
        {
            try
            {
                string fullPath = path == "\\" ? basepath + username + @"\" : basepath + username + path;

                List<object> listFiles = new List<object>();

                var allFiles = Directory.GetFiles(fullPath).Select(p => new
                {
                    Path = p,
                    Name = Path.GetFileName(p)
                }).ToArray();

                foreach (var aux in allFiles)
                {
                    string tempName = aux.Name;
                    string tempPath = path == @"\\" ? aux.Path.Replace(basepath + username + @"\", "") : aux.Path.Replace(basepath + username, "");

                    listFiles.Add(new
                    {
                        name = tempName,
                        path = tempPath
                    });
                }

                return new
                {
                    files = listFiles
                };
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
