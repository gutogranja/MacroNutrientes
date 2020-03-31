using System;
using System.Collections.Generic;
using System.Text;

namespace MacroNutrientes.Domain.Entities.Views
{   
    public class RefeicoesView
    {
        public int Id { get; set; }
        public DateTime DataRefeicao { get; set; }
        public string Refeicao { get; set; }
        public int IdAlimento { get; set; }
        public double Peso { get; set; }
        public double Proteina { get; set; }
        public double Carboidrato { get; set; }
        public double Gordura { get; set; }
        public double Caloria { get; set; }
    }
}
