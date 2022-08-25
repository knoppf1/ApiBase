using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eContadi.Core.Interfaces;
using eContadi.Views.BusinessCrud;
using eContadi.Views.BusinessList;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tools;
using Ubiety.Dns.Core;

namespace eContadiAPI.Controllers
{
    [Produces("application/json")]
    [Route("usuario")]
    public class UsuarioController
    {
        private readonly IServiceUsuario service;

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_service"></param>
        /// <param name="contextAccessor"></param>

        public UsuarioController(IServiceUsuario _service, IHttpContextAccessor contextAccessor)
        {
            try
            {
                service = _service;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Usuario

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<string> Delete(long id)
        {
            var result = service.Delete(id);
            return await Task.Run(() => result);
        }

        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="idEmpresa"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("list")]
        public async Task<ViewListRetorno<List<ViewListUsuario>>> List(int tipo, int? idEmpresa, int count = 10, int page = 1, string filter = "")
        {
            var result = service.List(tipo, idEmpresa, count, page, filter);
            return await Task.Run(() => result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///[Authorize]
        [HttpGet]
        [Route("get/{id}")]
        public async Task<ViewCrudUsuario> Get(long id)
        {
            return await Task.Run(() => service.Get(id));
        }

        /// <summary>
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view">CRUD</param>
        /// <returns>CRUD</returns>
        //[Authorize]
        [HttpPost]
        [Route("new")]
        public async Task<ViewCrudUsuario> New([FromBody] ViewCrudUsuario view)
        {
            return await Task.Run(() => service.New(view));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPut]
        [Route("update")]
        public async Task<ViewCrudUsuario> Update([FromBody] ViewCrudUsuario view)
        {
            return await Task.Run(() => service.Update(view));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPut]
        [Route("ativar/{id}")]
        public async Task<ViewCrudUsuario> Ativar(long id)
        {
            return await Task.Run(() => service.Ativar(id));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("listAtivo")]
        public async Task<List<ViewListUsuario>> ListAtivo()
        {
            var result = service.ListAtivo();
            return await Task.Run(() => result);
        }
        #endregion

      





    }
}

