using System;
using System.Collections.Generic;
using System.Text;

namespace MacroNutrientes.Domain.Entities.Requests
{
    public class AlimentoRequest    
    {
        public int Id { get; set; }
        public string Alimento { get; set; }
        public double Peso { get; set; }
        public double Proteina { get; set; }
        public double Carboidrato { get; set; }
        public double Gordura { get; set; }
        public double Caloria { get; set; }
    }
}
