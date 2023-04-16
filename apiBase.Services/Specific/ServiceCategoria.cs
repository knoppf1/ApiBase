using apiBase.Core.Base;
using apiBase.Core.Business;
using apiBase.Core.Interfaces;
using apiBase.Data;
using apiBase.Services.Commons;
using apiBase.Views.BusinessCrud;
using apiBase.Views.BusinessList;
//using apiBase.Views.Enumns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools;

namespace apiBase.Services.Specific

{
    //Regras de Negócio e Validação antes de persistir no Categoria
    public class ServiceCategoria : Repository<Categoria>, IServiceCategoria
    {
        private ServiceGenerico<Categoria> serviceCategoria;
        //Chaves Estrangeiras
        //private ServiceGenerico<Empresa> serviceEmpresa;



        #region Constructor
        public ServiceCategoria(DataContext context) : base(context)
        {
            try
            {
                serviceCategoria = new ServiceGenerico<Categoria>(context);
                //Chaves Estrangeiras
                //serviceEmpresa = new ServiceGenerico<Empresa>(context);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Categoria


        public string Delete(long id)
        {
            try
            {
                serviceCategoria.Delete(id);
                return "deleted";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lista todos os campos da Categoria com Filtro e Paginação (NOVO)
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ViewListRetorno<List<ViewListCategoria>> List(int idEmpresa, int count, int page, string filter)
        {
            try
            {
                List<ViewListCategoria> result = (from categoria in serviceCategoria.GetAll(p => true).Result
                                                  
                                                  where categoria.Nome.Contains(filter)

                                                  select new ViewListCategoria()
                                                    {
                                                        id = categoria.id,
                                                        
                                                        Nome = categoria.Nome,
                                                        Ativo = categoria.Ativo,
                                                        

                                                    }).ToList();


                var retorno = new ViewListRetorno<List<ViewListCategoria>>()
                {
                    Total = result.Count(),
                    Dados = result.Skip((page - 1) * count).Take(count).ToList(),
                };
                return retorno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ViewCrudCategoria Get(long id)
        {
            try
            {
                return serviceCategoria.GetById(id).Result.GetViewCrud();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ViewCrudCategoria New(ViewCrudCategoria view)
        {
            try
            {
                var model = new Categoria()
                {
                    Nome = view.Nome,
                    
                    Ativo = view.Ativo,
                    
                    

                };
                model = serviceCategoria.Insert(model).Result;
                return model.GetViewCrud();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ViewCrudCategoria Update(ViewCrudCategoria view)
        {
            try
            {
                Categoria model = serviceCategoria.GetById(view.id).Result;
                model.Nome = view.Nome;
                
                model.Ativo = view.Ativo;
                //Chaves estrangeiras
                

                serviceCategoria.Update(model).Wait();
                return model.GetViewCrud();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<ViewListCategoria> ListAtivo(int idEmpresa)
        {
            try
            {
                var result = serviceCategoria.GetAll(p => p.Ativo == 1 ).Result
                    .Select(x => x.GetViewList()).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ViewCrudCategoria Ativar(long id)
        {
            try
            {
                Categoria model = serviceCategoria.GetById(id).Result;
                model.Ativo = (model.Ativo == 1 ? 0 : 1);

                serviceCategoria.Update(model).Wait();
                return model.GetViewCrud();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
