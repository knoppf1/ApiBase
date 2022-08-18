using eContadi.Core.Base;
using eContadi.Core.Business;
using eContadi.Core.Interfaces;
using eContadi.Data;
using eContadi.Services.Commons;
using eContadi.Views.BusinessCrud;
using eContadi.Views.BusinessList;
//using eContadi.Views.Enumns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools;

namespace eContadi.Services.Specific

{
    //Regras de Negócio e Validação antes de persistir no Cadastro
    public class ServiceCadastro : Repository<Cadastro>, IServiceCadastro
    {
        private ServiceGenerico<Cadastro> serviceCadastro;
        //Chaves Estrangeiras
        //private ServiceGenerico<Empresa> serviceEmpresa;



        #region Constructor
        public ServiceCadastro(DataContext context) : base(context)
        {
            try
            {
                serviceCadastro = new ServiceGenerico<Cadastro>(context);
                //Chaves Estrangeiras
                //serviceEmpresa = new ServiceGenerico<Empresa>(context);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Cadastro


        public string Delete(long id)
        {
            try
            {
                serviceCadastro.Delete(id);
                return "deleted";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lista todos os campos da Cadastro com Filtro e Paginação (NOVO)
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ViewListRetorno<List<ViewListCadastro>> List(int idEmpresa, int count, int page, string filter)
        {
            try
            {
                List<ViewListCadastro> result = (from Cadastro in serviceCadastro.GetAll(p => true).Result
                                                  
                                                  where Cadastro.Nome.Contains(filter)

                                                  select new ViewListCadastro()
                                                    {
                                                      id = Cadastro.id,
                                                        
                                                      Nome = Cadastro.Nome,
                                                      Cpf = Cadastro.Cpf,
                                                      DataNascimento = Cadastro.DataNascimento,
                                                      Email = Cadastro.Email,
                                                      Telefone = Cadastro.Telefone,
                                                      Endereco = Cadastro.Endereco,
                                                      Corhexa = Cadastro.Corhexa,
                                                      Lat = Cadastro.Lat,
                                                      Lng = Cadastro.Lng,

                                                      Ativo = Cadastro.Ativo,
                                                        

                                                    }).ToList();


                var retorno = new ViewListRetorno<List<ViewListCadastro>>()
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
        public ViewCrudCadastro Get(long id)
        {
            try
            {
                return serviceCadastro.GetById(id).Result.GetViewCrud();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ViewCrudCadastro New(ViewCrudCadastro view)
        {
            try
            {
                var model = new Cadastro()
                {
                    Nome = view.Nome,
                    Cpf = view.Cpf,
                    DataNascimento = view.DataNascimento,
                    Email = view.Email,
                    Telefone = view.Telefone,
                    Endereco = view.Endereco,
                    Corhexa = view.Corhexa,
                    Lat = view.Lat,
                    Lng = view.Lng,

                    Ativo = view.Ativo,
                    
                    

                };
                model = serviceCadastro.Insert(model).Result;
                return model.GetViewCrud();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ViewCrudCadastro Update(ViewCrudCadastro view)
        {
            try
            {
                Cadastro model = serviceCadastro.GetById(view.id).Result;
                model.Nome = view.Nome;
                model.Cpf = view.Cpf;
                model.DataNascimento = view.DataNascimento;
                model.Email = view.Email;
                model.Telefone = view.Telefone;
                model.Endereco = view.Endereco;
                model.Corhexa = view.Corhexa;
                model.Lat = view.Lat;
                model.Lng = view.Lng;
                
                model.Ativo = view.Ativo;
                
                

                serviceCadastro.Update(model).Wait();
                return model.GetViewCrud();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<ViewListCadastro> ListAtivo(int idEmpresa)
        {
            try
            {
                var result = serviceCadastro.GetAll(p => p.Ativo == 1 ).Result
                    .Select(x => x.GetViewList()).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ViewCrudCadastro Ativar(long id)
        {
            try
            {
                Cadastro model = serviceCadastro.GetById(id).Result;
                model.Ativo = (model.Ativo == 1 ? 0 : 1);

                serviceCadastro.Update(model).Wait();
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
