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
