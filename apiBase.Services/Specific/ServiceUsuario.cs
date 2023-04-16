using apiBase.Core.Base;
using apiBase.Core.Business;
using apiBase.Core.Interfaces;
using apiBase.Data;
using apiBase.Services.Commons;
using apiBase.Views.BusinessCrud;
using apiBase.Views.BusinessList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools;
using apiBase.DataDapper;

namespace apiBase.Services.Specific
{
    public class ServiceUsuario : Repository<Usuario>, IServiceUsuario
    {
        private ServiceGenerico<Usuario> serviceUsuario;
        
        private DBQuery db;
        


        #region Constructor
        public ServiceUsuario(DataContext context, DBQuery dbQuery) : base(context)
        {
            try
            {
                serviceUsuario = new ServiceGenerico<Usuario>(context);
                
                
                
                
                
                db = dbQuery;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Usuario


        public string Delete(long id)
        {
            try
            {
                serviceUsuario.Delete(id);
                return "deleted";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ViewListUsuario> ListUsuarioGrupoEmpresa(int idGrupoEmpresa, int count, int page)
        {
            try
            {

                List<ViewListUsuario> result = (from usuario in serviceUsuario.GetAll(p => true).Result
                                                
                                                where usuario.Tipo == 1

                                                select new ViewListUsuario()
                                                {
                                                    id = usuario.id,
                                                    Nome = usuario.Nome,
                                                    Apelido = usuario.Apelido,
                                                    Cargo = usuario.Cargo,
                                                    
                                                    Email = usuario.Email,
                                                    Ativo = usuario.Ativo,
                                                    Tipo = usuario.Tipo,
                                                    
                                                    //Chaves estrangeiras
                                                    

                                                }).Skip((page - 1) * count).Take(count).ToList();

                return result;


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ViewListRetorno<List<ViewListUsuario>> List(int tipo, int? idEmpresa, int count, int page, string filter)
        {
            try
            {
                try
                {
                    List<ViewListUsuario> result = (from usu in serviceUsuario.GetAll(p => p.Nome.Contains(filter)).Result
                                                    
                                                    where usu.Tipo == tipo 

                                                    select new ViewListUsuario()
                                                    {
                                                        id = usu.id,
                                                        Nome = usu.Nome,
                                                        Apelido = usu.Apelido,
                                                        Email = usu.Email,
                                                        Cargo = usu.Cargo,
                                                        
                                                        Senha = usu.Senha,
                                                        Ativo = usu.Ativo,
                                                        
                                                        
                                                        Tipo = usu.Tipo,
                                                        
                                                        //DataExpiracao = Coopertec.DateTimeToStr(usu.DataExpiracao)

                                                    }).ToList();

                    var retorno = new ViewListRetorno<List<ViewListUsuario>>()
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public ViewCrudUsuario Get(long id)
        {
            try
            {
                return serviceUsuario.GetById(id).Result.GetViewCrud();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ViewCrudUsuario New(ViewCrudUsuario view)
        {
            try
            {
                var model = new Usuario()
                {
                    Nome = view.Nome,
                    Apelido = view.Apelido,
                    Cargo = view.Cargo,
                    
                    Email = view.Email,
                    Senha = view.Senha,
                    Ativo = view.Ativo,
                    
                    Tipo = view.Tipo,
                    
                    //DataExpiracao = (DateTime)Coopertec.StrToDate(view.DataExpiracao),
                    
                };

                //Encriptação da Senha recebida pelo JSON
                if ((model.Senha != "") && (model.Senha != null))
                    model.Senha = EncryptService.GetMD5Hash(view.Senha);

                model = serviceUsuario.Insert(model).Result;

                return model.GetViewCrud();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ViewCrudUsuario Update(ViewCrudUsuario view)
        {
            try
            {
                var model = serviceUsuario.GetById(view.id).Result;
                model.Nome = view.Nome;
                model.Apelido = view.Apelido;
                model.Cargo = view.Cargo;
                
                model.Email = view.Email;
                //model.Senha = view.Senha;
                model.Ativo = view.Ativo;
                model.Tipo = view.Tipo;
                
                //model.DataExpiracao = (DateTime)Coopertec.StrToDate(view.DataExpiracao);
                

                if (model.Senha != view.Senha)
                    model.Senha = EncryptService.GetMD5Hash(view.Senha);

                serviceUsuario.Update(model).Wait();
                return model.GetViewCrud();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ViewListUsuario> ListAtivo()
        {
            try
            {
                var result = serviceUsuario.GetAll(p => p.Ativo == 1).Result
                    .Select(x => x.GetViewList()).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ViewCrudUsuario Ativar(long id)
        {
            try
            {
                Usuario model = serviceUsuario.GetById(id).Result;
                model.Ativo = (model.Ativo == 1 ? 0 : 1);

                serviceUsuario.Update(model).Wait();
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

