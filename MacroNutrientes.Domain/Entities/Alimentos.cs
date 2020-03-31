using Dapper.Contrib.Extensions;

namespace MacroNutrientes.Domain.Entities
{
    [Table("Alimentos")]
    public class Alimentos : Entity
    {
        private Alimentos() : base("")
        {
        }

        public string Alimento { get; set; }
        public double Peso { get; set; }
        public double Proteina { get; set; }
        public double Carboidrato { get; set; }
        public double Gordura { get; set; }
        public double Caloria { get; set; }

        public Alimentos(string alim, double peso, double proteina, double carbo, double gordura, double caloria, string usuario) : base(usuario)
        {
            Alimento = alim;
            Peso = peso;
            Proteina = proteina;
            Carboidrato = carbo;
            Gordura = gordura;
            Caloria = caloria;
            if (string.IsNullOrEmpty(alim))
                AdicionarNotificacao("Alimentos", Mensagens.Alimento);
            if (peso <= 0)
                AdicionarNotificacao("Alimentos", Mensagens.Peso);
            if (peso > 0 && proteina <= 0 && carbo <= 0 && gordura <= 0 && caloria <= 0)
                AdicionarNotificacao("Alimentos", "Alimento deve conter proteína ou carboidrato ou gordura ou caloria");
        }

        public void AlterarPeso(double peso)
        {
            if (peso <= 0)
                AdicionarNotificacao("Alimentos", Mensagens.Peso);
            else
                Peso = peso;
        }

        public void AlterarProteina(double proteina)
        {
            Proteina = proteina;
        }

        public void AlterarCarboidrato(double carbo)
        {
            Carboidrato = carbo;
        }

        public void AlterarGordura(double gordura)
        {
            Gordura = gordura;
        }

        public void AlterarCaloria(double caloria)
        {
            Caloria = caloria;
        }
    }
}
