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
                                                  
                                                  where Cadastro.NomeEquipamento.Contains(filter)

                                                  select new ViewListCadastro()
                                                    {
                                                      id = Cadastro.id,

                                                      NomeEquipamento = Cadastro.NomeEquipamento,
                                                      Patrimonio = Cadastro.Patrimonio,
                                                      DepartamentoResponsavel = Cadastro.DepartamentoResponsavel,
                                                      DataAquisicao = Cadastro.DataAquisicao,
                                                      DataUltimaCalibracao = Cadastro.DataUltimaCalibracao,
                                                      DataProximaCalibracao = Cadastro.DataProximaCalibracao,
                                                      Calibrado = Cadastro.Calibrado,
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
                    NomeEquipamento = view.NomeEquipamento,
                    Patrimonio = view.Patrimonio,
                    DepartamentoResponsavel = view.DepartamentoResponsavel,
                    DataAquisicao = view.DataAquisicao,
                    DataUltimaCalibracao = view.DataUltimaCalibracao,
                    DataProximaCalibracao = view.DataProximaCalibracao,
                    Calibrado = view.Calibrado,
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
                model.NomeEquipamento = view.NomeEquipamento;
                model.Patrimonio = view.Patrimonio;
                model.DepartamentoResponsavel = view.DepartamentoResponsavel;
                model.DataAquisicao = view.DataAquisicao;
                model.DataUltimaCalibracao = view.DataUltimaCalibracao;
                model.DataProximaCalibracao = view.DataProximaCalibracao;
                model.Calibrado = view.Calibrado;
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
