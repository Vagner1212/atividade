using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atividade

{
    /// <summary>
    /// A classe Empregado representa um empregado da  empresa. Aqui guarda todas as informações básicas dele,
    /// como nome, sobrenome, idade, e até salário. A gente também cuida pra que ninguém coloque um salário muito baixo
    /// nada menos que 1320. Tem  algumas funcionalidades a mais como ajustar a matrícula e definir a 
    /// data que o empregado entrou na empresa. Bem direto ao ponto!
    /// </summary>
  
    internal class Empregado
    {
        public string primeironome { get; private set; }
        public string sobrenome { get; private set; }
        private int matricula { get; set; }
        public int Matricula
        {
            get
            {
                return matricula;
            }
            set
            {
                matricula = value;
            }
        }
        public int idade { get; private set; }
        public DateTime dataNascimento { get; private set; }
        private DateTime? dataContratacao { get; set; } = null;
        public DateTime? DataContratacao
        {
            get
            {
                return dataContratacao;
            }
            set
            {
                dataContratacao = value;
            }
        }

        private double salarioMensal;
        public double SalarioMensal

        {

            get
            {

                return salarioMensal;

            }
            set
            {
                if (value < 1320)
                {
                    SalarioMensal = 1320;
                }
                else
                {
                    salarioMensal = value;
                }
            }
        }
        /// <summary>
        /// Esse é um dos construtores para a classe Empregado. Com ele conseguimos criar um novo empregado
        /// só com as informações  básicas como: nome, sobrenome, idade e data de nascimento. 
        /// </summary>
        /// <param name="primeironome">Primeiro nome do empregado.</param>
        /// <param name="sobrenome">Sobrenome do empregado.</param>
        /// <param name="idade">Idade do empregado.</param>
        /// <param name="dataNascimento">Data de nascimento do empregado.</param>
        internal Empregado(string primeironome, string sobrenome, int idade, DateTime dataNascimento)
        {
            this.primeironome = primeironome;
            this.sobrenome = sobrenome;
            this.idade = idade;
            this.dataNascimento = dataNascimento;

        }
        /// <summary>
        /// Esse aqui é outro construtor da class e 'Empregado'!
        /// Com as informações básicas, ele também permite definir a matrícula, data de contratação e o salário mensal do empregado.
        /// A gente consegue criar um empregado com todas as informações de uma vez só.
        /// </summary>
        /// <param name="primeironome">Primeiro nome do empregado.</param>
        /// <param name="sobrenome">Sobrenome do empregado.</param>
        /// <param name="idade">Idade do empregado.</param>
        /// <param name="dataNascimento">Data de nascimento do empregado.</param>
        /// <param name="matricula">Matrícula do empregado.</param>
        /// <param name="dataContratacao">Data de contratação do empregado.</param>
        /// <param name="salarioMensal">Salário mensal do empregado.</param>
        internal Empregado(string primeironome, string sobrenome, int idade, DateTime dataNascimento, int matricula, DateTime dataContratacao, double salarioMensal)
        {
            this.primeironome = primeironome;
            this.sobrenome = sobrenome;
            this.idade = idade;
            this.dataNascimento = dataNascimento;
            this.matricula = matricula;
            this.SalarioMensal = salarioMensal;
            this.dataContratacao = dataContratacao;
        }
        /// <summary>
        /// Aumento de 10% no salário do empregado.
        /// </summary>
        internal void AumentarSalario()
        {
            SalarioMensal += SalarioMensal * 0.1;
        }
    }
}
