using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using eContadi.Views.BusinessList;
using eContadi.Core.Interfaces;
using eContadi.Data;
using eContadi.Core.Business;
using System.Linq;
using eContadi.Services.Specific;
using Newtonsoft.Json;
using eContadi.DataDapper;

namespace eContadi.Services.Auth
{
    public class ServiceAuthentication : IServiceAuthentication
    {
        private const string Secret = "db3OIsj+BX567FGy0t8W3TcNekrF+2d/1sFnWG4la4okZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        private readonly ServiceUsuario serviceUser;
        
        private DBQuery db;

        #region Constructor
        public ServiceAuthentication(DataContext context)
        {
            try
            {
                serviceUser = new ServiceUsuario(context, db);
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Authentication Controller
        public ViewListUsuarioAutenticado Authentication(ViewAuthentication userLogin, bool manager)

        {
            string msgErro = "";
            try
            {
                if (string.IsNullOrEmpty(userLogin.Apelido))
                {
                    msgErro = "Usuário não informado!";
                }
                if (string.IsNullOrEmpty(userLogin.Senha))
                {
                    msgErro = "Senha não informada!";
                }

                Usuario user = serviceUser.GetAll(p => p.Apelido == userLogin.Apelido && p.Senha == Tools.EncryptService.GetMD5Hash(userLogin.Senha)).Result.FirstOrDefault();
                if (user == null)
                {
                    msgErro = "Usuário ou Senha inválido!";
                }

                /*

                if (user.DataExpiracao != null)
                {
                    DateTime date = DateTime.Now;
                    if (user.DataExpiracao < date)
                    {
                        msgErro = "Verifique data expiração!";
                    }
                    else
                    {
                        return Authentication(user);
                    }
                }

                */
                

                //Retorna Erro 
                if (msgErro != "")
                {
                    var person = new ViewListUsuarioAutenticado()
                    {
                        IdUsuario = 0,
                        //IdEmpresa = 0,
                        Nome = "",
                        Token = "",
                        msgErro = msgErro
                    };

                    return person;
                    //Retorna os dados do Usuario
                }

                else
                {
                    return Authentication(user);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        #endregion

        #region Authentication Internal
        // Valida autenticação para Manager
        public ViewListUsuarioAutenticado Authentication(Usuario user)
        {
            try
            {
                //Carrega a Empresa Padrao do Usuario
                //UsuarioEmpresa userempresa = serviceUserEmpresa.Get(p => p.idUsuario == user.id && p.Padrao == 1).Result;

                //Carrega as Empresas do Usuario
                /*List<ViewListEmpresa> empresas = (from usuemp in serviceUserEmpresa.GetAll(p => true).Result
                                                  join emp in serviceCompany.GetAll(p => true).Result on usuemp.idEmpresa equals emp.id
                                                  where usuemp.idUsuario == user.id
                                                  select new ViewListEmpresa()
                                                  {
                                                      id = emp.id,
                                                      Nome = emp.Nome,
                                                      Ativo = emp.Ativo,
                                                      idGrupoEmpresa = emp.idGrupoEmpresa,
                                                      //NomeGrupoEmpresa = gemp.Nome

                                                  }).ToList();
                */

                //Carrega os dados da Empresa Padrao
                //Empresa tabelapadrao = serviceCompany.GetById(userempresa.idEmpresa).Result;

                var person = new ViewListUsuarioAutenticado()
                {
                    IdUsuario = user.id,
                    //IdEmpresa = userempresa.idEmpresa,
                    Nome = user.Nome,
                    msgErro = ""
                };

                //Token
                Claim[] claims = new[]
                {
                    new Claim("nomeUsuario", person.Nome),
                    new Claim("idUsuario",person.IdUsuario.ToString()),
                    //new Claim("idEmpresa",person.IdEmpresa.ToString()),
                    //new Claim("empresas", JsonConvert.SerializeObject(empresas)),

                };

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "localhost",
                    audience: "localhost",
                    claims: claims,
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)), SecurityAlgorithms.HmacSha256)
                );
                person.Token = new JwtSecurityTokenHandler().WriteToken(token);
                return person;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion


    }
}