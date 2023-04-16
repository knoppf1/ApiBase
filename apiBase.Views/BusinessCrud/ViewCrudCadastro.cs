using System;
using System.Collections.Generic;
using System.Text;

namespace apiBase.Views.BusinessCrud
{
    public class ViewCrudCadastro : _ViewCrudBase
    {
        public string NomeEquipamento { get; set; }
        public string Patrimonio { get; set; }
        public string DepartamentoResponsavel { get; set; }
        public string DataAquisicao { get; set; }
        public string DataUltimaCalibracao { get; set; }
        public string DataProximaCalibracao { get; set; }
        public int Calibrado { get; set; }
    }
}

