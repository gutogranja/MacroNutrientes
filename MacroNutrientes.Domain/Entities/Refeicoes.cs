using System;
using Dapper.Contrib.Extensions;

namespace MacroNutrientes.Domain.Entities
{
    [Table("Refeicoes")]
    public class Refeicoes : Entity
    {
        private Refeicoes() : base("")
        {
        }

        public DateTime DataRefeicao { get; set; }
        public string Refeicao { get; set; }
        public int IdAlimento { get; set; } 
        public double Peso { get; set; }
        public double Proteina { get; set; }
        public double Carboidrato { get; set; }
        public double Gordura { get; set; }
        public double Caloria { get; set; }

        public Refeicoes(DateTime dataRefeicao, string refeicao,int idAlimento,double peso,string usuario) : base(usuario)
        {
            DataRefeicao = dataRefeicao;
            Refeicao = refeicao;
            IdAlimento = idAlimento;
            Peso = peso;
            if (dataRefeicao == null)
                AdicionarNotificacao("Refeicao", Mensagens.DataRefeicao);
            if (string.IsNullOrEmpty(refeicao))
                AdicionarNotificacao("Refeicao", Mensagens.Refeicao);
            if (idAlimento <= 0)
                AdicionarNotificacao("Refeicao", Mensagens.Alimento);
            if (peso <= 0)
                AdicionarNotificacao("Refeicao", Mensagens.Peso);
        }

        public void AlterarDataRefeicao(DateTime data)
        {
            if (data == null)
                AdicionarNotificacao("Refeicao", Mensagens.DataRefeicao);
            else
                DataRefeicao = data;
        }

        public void AlterarRefeicao(string refeicao)
        {
            if (string.IsNullOrEmpty(refeicao))
                AdicionarNotificacao("Refeicao", Mensagens.Refeicao);
            else
                Refeicao = refeicao;
        }

        public void AlterarAlimento(int idAlimento)
        {
            if (idAlimento <= 0)
                AdicionarNotificacao("Refeicao", Mensagens.Alimento);
            else
                IdAlimento = idAlimento;
        }

        public void AlterarPeso(double peso,Refeicoes refeicao)
        {
            if (peso <= 0)
                AdicionarNotificacao("Refeicao", Mensagens.Peso);
            else
            {
                Peso = peso;
                Proteina = refeicao.Proteina * peso / 100;
                Proteina = System.Math.Round(Proteina, 1);
                Carboidrato = refeicao.Carboidrato * peso / 100;
                Carboidrato = System.Math.Round(Carboidrato, 1);
                Gordura = refeicao.Gordura * peso / 100;
                Gordura = System.Math.Round(Gordura, 1);
                Caloria = refeicao.Caloria * peso / 100;
                Caloria = System.Math.Round(Caloria, 1);
            }
        }
    }
}
