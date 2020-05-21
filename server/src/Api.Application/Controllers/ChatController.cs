using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Core.Token;
using Api.Model.Entities;
using Api.Model.IServices.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Api.Application.Controllers
{
    [ApiController]
    [EnableCors]
    public class Chat : ControllerBase
    {
        private IChatService _chat;

        public Chat(IChatService chat)
        {
            _chat = chat;
        }

        /// <summary>
        /// Criação de um chat novo
        /// </summary>
        /// <param name="name">Nome do chat</param>
        /// <param name="description">Descrição do chat</param>
        /// <returns>Retorna um status code do estado da criação do chat.</returns>
        [Authorize("Bearer")]
        [HttpGet]
        [Route("newchat")]
        [EnableCors]
        public async Task<ActionResult> NewChat(string name, string description)
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                var result = await _chat.NewChat(name, description);
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


        /// <summary>
        /// Update/Atualização de um chat
        /// </summary>
        /// <param name="id">id do chat a ser atualizado</param>
        /// <param name="name">novo nome para o chat</param>
        /// <param name="description">nova descrição para o chat</param>
        /// <returns>Retorna um status code do estado da atualização do chat</returns>
        [Authorize("Bearer")]
        [HttpPost]
        [Route("updatechat")]
        [EnableCors]
        public async Task<ActionResult> UpdateChat(int id, string name, string description)
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                var result = await _chat.UpdateName(id, name, description);
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

        /// <summary>
        /// Listagem de todos os chats de um client.
        /// </summary>
        /// <returns>Retorna um status code do estado da listagem dos chats.</returns>
        [Authorize("Bearer")]
        [HttpPost]
        [Route("getallchats")]
        [EnableCors]
        public async Task<ActionResult> GetAllChats()
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                return Ok(await _chat.GetAllChats());
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        /// <summary>
        /// Listagem de todas as mensagens de um client
        /// </summary>
        /// <returns>Retorna um status code do estado da listagem de todas as mensagens.</returns>
        [Authorize("Bearer")]
        [HttpPost]
        [Route("getallmessages")]
        [EnableCors]
        public async Task<ActionResult> GetAllMessages()
        {
            //verificar token do client
            object tokenValidate;
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization) || !TokenMng.ValidateToken(Request.Headers[HeaderNames.Authorization], out tokenValidate))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 bad request - solicitação invalida

            try
            {
                return Ok(await _chat.GetAllMessages());
            }
            catch (Exception ex)
            {
                // 500 Internal error - O server encontrou um erro com o qual não consegue lidar
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
