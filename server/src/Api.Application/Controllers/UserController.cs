using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Token;
using Api.Model;
using Api.Model.IServices.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Api.Application.Controllers
{
    [ApiController]
    [EnableCors]
    public class User : ControllerBase
    {
        private IUserService _user;

        public User(IUserService user)
        {
            _user = user;
        }


        /// <summary>
        /// Novo utilizador
        /// </summary>
        /// <param name="name">nome</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <param name="email">email</param>
        /// <param name="imagePath">caminho para a imagem do utilizador</param>
        /// <param name="folderPath">caminho para o ftp do utilizador</param>
        /// <returns>Retorna o novo utilizador</returns>
        [Authorize("Bearer")]
        [HttpPost]
        [Route("newuser")]
        [EnableCors]
        public async Task<ActionResult> NewUser(string name, string username, string password, string email, string imagePath, string folderPath)
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                var result = await _user.NewUser(name, username, password, email, imagePath, folderPath);
                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("updateuser")]
        [EnableCors]
        public async Task<ActionResult> UpdateUser(string name, string email, string imagePath, string folderPath)
        {
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                string username = string.Empty;
                var result = await _user.UpdateUser(name, username, email, imagePath, folderPath);
                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("deleteuser")]
        [EnableCors]
        public async Task<ActionResult> DeleteUser(int id)
        {
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(false); // 400 bad request - solicitação invalida

            try
            {
                return Ok(await _user.RemoveUser(id)); // 50 ok - Bem conseguida
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
