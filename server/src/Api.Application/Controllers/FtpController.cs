using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Core.Token;
using Api.Model.IServices.Ftp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Api.Application.Controllers
{
    [ApiController]
    [EnableCors]
    public class FtpController : ControllerBase
    {
        private IFtpManagerService _ftp;

        public FtpController(IFtpManagerService ftp)
        {
            _ftp = ftp;
        }

        /// <summary>
        /// Renomear um arquivo
        /// </summary>
        /// <param name="path">Caminho/Diretorio do arquivo</param>
        /// <param name="newName">Novo nme para o arquivo</param>
        /// <returns>Retorna um status code do estado da renomeaçao do arquivo</returns>
        [Authorize("Bearer")]
        [HttpPost]
        [Route("renamefile")]
        [EnableCors]
        public async Task<ActionResult> RenameFile(string path, string newName)
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(); // 400 bad request - solicitação invalida

            try
            {
                return Ok(await _ftp.RenameFile(TokenMng.UsernameToken(Request.Headers[HeaderNames.Authorization]), path, newName));
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Apagar um arquivo
        /// </summary>
        /// <param name="path">Caminho/Diretorio do aquivo a apagar</param>
        /// <returns>Retorna um status code do estado da eliminação do arquivo</returns>
        [Authorize("Bearer")]
        [HttpDelete]
        [Route("deletefile")]
        [EnableCors]
        public async Task<IActionResult> DeleteFile(string path) //ver como aceitar ficheiros
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                return Ok(await _ftp.DeleteFile(TokenMng.UsernameToken(Request.Headers[HeaderNames.Authorization]), path));
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Download de arquivos
        /// </summary>
        /// <param name="fullPath">Caminho/Diretorio do aquivo</param>
        /// <returns>Retorna um status code do estado do download do arquivo</returns>
        [Authorize("Bearer")]
        [HttpGet]
        [Route("downloadfile")]
        [EnableCors]
        public async Task<ActionResult> DownloadFile(string fullPath, string name) //ver como aceitar ficheiros
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization)
            || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(); // 400 bad request - solicitação invalida

            try
            {
                MemoryStream memStream = await _ftp.DownloadFile(TokenMng.UsernameToken(Request.Headers[HeaderNames.Authorization]), fullPath);

                if (memStream == null)
                    return NotFound(); // Status 404 - NotFound 

                memStream.Position = 0;
                return File(memStream, "application/octet-stream", name); // returns a FileStreamResult
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Uplaod de um arquivo
        /// </summary>
        /// <param name="folderPath">Caminho/Diretorio da pasta</param>
        /// <returns>Retorna um status code do estado final do upload</returns>
        [Authorize("Bearer")]
        [HttpPost]
        [Route("uploadfile")]
        [EnableCors]
        public async Task<IActionResult> UploadFile(string folderPath) //ver como aceitar ficheiros
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                var file = Request.Form.Files;
                var name = file[0].Name;
                if (file.Count == 1)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file[0].CopyToAsync(stream);
                        return Ok(await _ftp.UploadFile(TokenMng.UsernameToken(Request.Headers[HeaderNames.Authorization]), folderPath, name, stream));
                    }
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        /*========================================================================================================*/
        /*===========================================     FOLDERS     ============================================*/
        /*========================================================================================================*/

        /// <summary>
        /// Criar uma nova pasta
        /// </summary>
        /// <param name="path">Caminho/Diretorio da pasta</param>
        /// <returns>Retorna um status code do estado da criação da pasta</returns>
        [Authorize("Bearer")]
        [HttpPost]
        [Route("newfolder")]
        [EnableCors]
        public async Task<IActionResult> NewFolder(string path)
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                return Ok(await _ftp.NewFolder(TokenMng.UsernameToken(Request.Headers[HeaderNames.Authorization]), path));
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Renomear uma pasta
        /// </summary>
        /// <param name="folderPath">Caminho/Diretorio da pasta</param>
        /// <param name="newName">Novo nome para a pasta</param>
        /// <returns>Retorna um status code do estado da renomeação da pasta</returns>
        [Authorize("Bearer")]
        [HttpPost]
        [Route("renamefolder")]
        [EnableCors]
        public async Task<IActionResult> RenameFolder(string folderPath, string newName)
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                return Ok(await _ftp.RenameFolder(TokenMng.UsernameToken(Request.Headers[HeaderNames.Authorization]), folderPath, newName));
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Apagar uma pasta
        /// </summary>
        /// <param name="path">Caminho/Diretorio da pasta</param>
        /// <returns>Retorna um status code do estado da eliminação da pasta</returns>
        [Authorize("Bearer")]
        [HttpDelete]
        [Route("deletefolder")]
        [EnableCors]
        public async Task<IActionResult> DeleteFolder(string path)
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                return Ok(await _ftp.DeleteFolder(TokenMng.UsernameToken(Request.Headers[HeaderNames.Authorization]), path));
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        /*========================================================================================================*/
        /*===========================================     Generic     ============================================*/
        /*========================================================================================================*/


        /// <summary>
        /// Obter o conteudo de uma pasta
        /// </summary>
        /// <param name="path">Caminho/Diretorio da pasta a procurar</param>
        /// <returns>Retorna um status code do estado da procura do conteudo da pasta</returns>
        [Authorize("Bearer")]
        [HttpPost]
        [Route("getbypath")]
        [EnableCors]
        public async Task<object> GetByPath(string path)
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                var aux = await _ftp.GetByPath(TokenMng.UsernameToken(Request.Headers[HeaderNames.Authorization]), path);
                if (aux == null)
                    return NotFound();
                else
                    return Ok(aux);
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("getfilesbypath")]
        [EnableCors]
        public async Task<object> GetFilesByPath(string path)
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                var aux = await _ftp.GetFilesByPath(TokenMng.UsernameToken(Request.Headers[HeaderNames.Authorization]), path);
                if (aux == null)
                    return NotFound();
                else
                    return Ok(aux);
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
