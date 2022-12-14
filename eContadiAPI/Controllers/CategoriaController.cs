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
    //Controladores e Rotas -> Recebe as requisições e direciona para os Serviços

    /// <summary>
    /// Controle (Categoria)
    /// </summary>
    [Produces("application/json")]
    [Route("Categoria")]
    public class CategoriaController
    {
        private readonly IServiceCategoria service;

        #region Constructor
        /// <summary>
        /// Interface (Categoria)
        /// </summary>
        /// <param name="_service"></param>
        /// <param name="contextAccessor"></param>
        public CategoriaController(IServiceCategoria _service, IHttpContextAccessor contextAccessor)
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

        #region Categoria

        /// <summary>
        /// Apaga um registro na tabela pelo {id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///[Authorize]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<string> Delete(long id)
        {
            var result = service.Delete(id);
            return await Task.Run(() => result);
        }

        /// <summary>
        /// Listagem da tabela 
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        ///[Authorize]
        [HttpGet]
        [Route("list/{idEmpresa}")]
        public async Task<ViewListRetorno<List<ViewListCategoria>>> List(int idEmpresa, int count = 10, int page = 1, string filter = "")
        {
            var result = service.List(idEmpresa, count, page, filter);

            return await Task.Run(() => result);
        }


        /// <summary>
        /// Lista registros pelo {Id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///[Authorize]
        [HttpGet]
        [Route("get/{id}")]
        public async Task<ViewCrudCategoria> Get(long id)
        {
            return await Task.Run(() => service.Get(id));
        }

        /// <summary>
        /// Cria um registro na tabela
        /// </summary>
        /// <param name="view">CRUD</param>
        /// <returns>CRUD</returns>
        ///[Authorize]
        [HttpPost]
        [Route("new")]
        public async Task<ViewCrudCategoria> New([FromBody] ViewCrudCategoria view)
        {
            return await Task.Run(() => service.New(view));
        }

        /// <summary>
        /// Atualiza a tabela
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ///[Authorize]
        [HttpPut]
        [Route("update")]
        public async Task<ViewCrudCategoria> Update([FromBody] ViewCrudCategoria view)
        {
            return await Task.Run(() => service.Update(view));
        }


        /// <summary>
        /// Ativa/Inativa registro pelo {id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///[Authorize]
        [HttpPut]
        [Route("ativar/{id}")]
        public async Task<ViewCrudCategoria> Ativar(long id)
        {
            return await Task.Run(() => service.Ativar(id));
        }

        /// <summary>
        /// Lista os registros ativos
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        ///[Authorize]
        [HttpGet]
        [Route("listAtivo/{idEmpresa}")]
        public async Task<List<ViewListCategoria>> ListAtivo(int idEmpresa)
        {
            var result = service.ListAtivo(idEmpresa);
            return await Task.Run(() => result);
        }
        #endregion
    }
}
