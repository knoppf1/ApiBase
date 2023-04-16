using apiBase.Core.Base;
using apiBase.Views.BusinessCrud;
using apiBase.Views.BusinessList;



namespace apiBase.Core.Business
{
    //Espelho do Categoria de Dados (Campos e Tipagem)
    public class Cadastro : BaseAtivo
    {
        public string NomeEquipamento { get; set; }
        public string Patrimonio { get; set; }
        public string DepartamentoResponsavel { get; set; }
        public string DataAquisicao { get; set; }
        public string DataUltimaCalibracao { get; set; }
        public string DataProximaCalibracao { get; set; }
        public int Calibrado { get; set; }
      





        public ViewListCadastro GetViewList()
        {
            return new ViewListCadastro()
            {
                //ViewList = (BaseEntity/BaseAtivo)/This
                id = id,
                NomeEquipamento = NomeEquipamento,
                Patrimonio = Patrimonio,
                DepartamentoResponsavel = DepartamentoResponsavel,
                DataAquisicao = DataAquisicao,
                DataUltimaCalibracao = DataUltimaCalibracao,
                DataProximaCalibracao = DataProximaCalibracao,
                Calibrado = Calibrado,
                Ativo = Ativo,
            };
        }
        public ViewCrudCadastro GetViewCrud()
        {
            return new ViewCrudCadastro()
            {
                //ViewCrud = (BaseEntity/BaseAtivo)/This
                id = id,
                NomeEquipamento = NomeEquipamento,
                Patrimonio = Patrimonio,
                DepartamentoResponsavel = DepartamentoResponsavel,
                DataAquisicao = DataAquisicao,
                DataUltimaCalibracao = DataUltimaCalibracao,
                DataProximaCalibracao = DataProximaCalibracao,
                Calibrado = Calibrado,
                Ativo = Ativo,
            };
        }
    }


}
