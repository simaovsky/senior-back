using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senior_back.Models.ViewModels
{
    public class CheckinViewModel
    {
        public ClientViewModel hospede { get; set; }
        public DateTimeOffset dataEntrada { get; set; }
        public DateTimeOffset dataSaida { get; set; }
        public bool adicionalVeiculo { get; set; }
        public decimal value { get; set; }
    }
}
