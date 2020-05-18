using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Core.Token;
using Api.Model.Entities;
using Api.Model.IServices.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Api.Application.Controllers
{
    [ApiController]
    [EnableCors]
    public class Login : ControllerBase
    {
        private ILoginService _user;

        public Login(ILoginService user)
        {
            _user = user;
        }

        /* METODOS */
        /// <summary>
        /// Autentica um utilizador
        /// </summary>
        /// <param name="username">Username do utilizador</param>
        /// <param name="password">Password do utilizador</param>
        /// <returns>Retorna um utilizador ou NotFound()</returns>
        [HttpPost]
        [Route("login")]
        [EnableCors]
        public async Task<object> LoginUser(string username, string password)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                var result = await _user.LoginUser(username, password);
                return result == null ? NotFound() : result; // 201 ok - Bem conseguida
            }
            catch (ArgumentException ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [Route("UserExists")]
        public async Task<ActionResult> UserExists(string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                return Ok(await _user.UserExists(username)); // 201 ok - Bem conseguida
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("validtoken")]
        [EnableCors]
        public async Task<object> ValidToken()
        {
            if (!ModelState.IsValid)
                return BadRequest(); // 400 bad request - solicitação invalida

            try
            {
                //verificar token do client
                object tokenValidate = new { authenticate = false };
                if (Request.Headers.ContainsKey(HeaderNames.Authorization))
                    TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate);
                return tokenValidate;
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
