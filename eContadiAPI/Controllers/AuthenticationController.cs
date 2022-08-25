using System;
using System.Threading.Tasks;
using eContadi.Core.Interfaces;
using eContadi.Views.BusinessList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GSApi.Controllers
{
    /// <summary>
    /// Controle de Autenticação
    /// </summary>
    [Produces("application/json")]
    [Route("authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IServiceAuthentication service;

        #region Constructor
        /// <summary>
        /// Contrutor do controle
        /// </summary>
        /// <param name="_service">Serviço de autenticação</param>
        public AuthenticationController(IServiceAuthentication _service)
        {
            service = _service;
        }
        #endregion

        #region Authentication

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ViewListUsuarioAutenticado), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ObjectResult> Post([FromBody] ViewAuthentication userLogin)
        {
            try
            {
                return await Task.Run(() => Ok(service.Authentication(userLogin, true)));
            }
            catch (Exception e)
            {
                return await Task.Run(() => BadRequest(e.Message));
            }
        }

        #endregion

    }
}
