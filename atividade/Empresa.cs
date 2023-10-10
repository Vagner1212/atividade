using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


/// <summary>
/// Essa é a classe Empresa onde ela guarda todos os empregados na lista EmpregadosList.
/// </summary>

namespace atividade
{
    internal class Empresa
    {
        public List<Empregado> EmpregadosList { get; set; } = new List<Empregado>();

        /// <summary>
        /// Aqui é o menu onde mostra as opções enquanto o usuário não escolher Sair.
        /// Tendo as opções para listar, cadastrar, promover e demitir empregados e  completar cadastros.
        /// </summary>
        internal void Menu()
        {
            bool rodando = true;
            while (rodando)
            {
                Console.Clear();
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1: Lista de Empregados");
                Console.WriteLine("2: Completar Cadastro");
                Console.WriteLine("3: Cadastrar um Empregado");
                Console.WriteLine("4: Promover um Empregado");
                Console.WriteLine("5: Demitir um Empregado");
                Console.WriteLine("6: Sair");

                switch (Console.ReadLine())
                {
                    case "1":
                        ListaEmpregados();
                        break;
                    case "2":
                        CompletarCadastro();
                        break;
                    case "3":
                        CadastrarEmpregado();
                        break;
                    case "4":
                        PromoverEmpregado();
                        break;
                    case "5":
                        DemitirEmpregado();
                        break;
                    case "6":
                        rodando = false;
                        break;
                }
            }
        }
        /// <summary>
        /// Esse método  mostra os empregados que colocados na lista.
        /// Basicamente, ele vai passando de um por um e mostrando as informações deles, tipo nome, idade e o quanto ganham.
        /// Se adicionar alguém ou ainda não colocado ninguém, ele avisa que não tem ninguém cadastrado.
        /// </summary>
        internal void ListaEmpregados()
        {
            Console.Clear();
            if (EmpregadosList.Count > 0)
            {
                foreach (var item in EmpregadosList)
                {
                    Console.WriteLine("Nome: " + item.primeironome);
                    Console.WriteLine("Sobrenome: " + item.sobrenome);
                    Console.WriteLine("Idade: " + item.idade);
                    Console.WriteLine("Matricula: " + item.Matricula);
                    Console.WriteLine("Data de contratação: " + item.DataContratacao);
                    Console.WriteLine("Data de nascimento: " + item.dataNascimento);
                    Console.WriteLine("Salário mensal: " + item.SalarioMensal.ToString("C"));
                    Console.WriteLine("Salário anual: " + SalarioAnual(item).ToString("C"));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Sem empregados cadastrados.");
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Esse método pra ajudar a cadastrar um empregado.
        /// O usuário pode escolher entre cadastrar  todas as informações ou só o básico,
        /// dependendo do que ele escolher, o método pede pra ele digitar os detalhes, 
        /// como nome, idade, data de nascimento, e outras coisinhas mais.
        /// Se o empregado já tiver sido cadastrado antes, ele avisa. 
        /// Se não, ele adiciona o empregado na nossa lista e dá um feedback.
        /// </summary>
        internal void CadastrarEmpregado()
        {
            Console.Clear();
            Console.WriteLine("1: Cadastrar empregado com todas as informações.");
            Console.WriteLine("2: Cadastrar empregado com informações básicas.");

            Empregado? empregadoNovo = null;
            string nome;
            string sobrenome;
            int idade;
            DateTime dataNascimento;
            int matricula;
            DateTime dataContratacao;
            double salarioMensal;

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Digite o nome do empregado:");
                    nome = Console.ReadLine();
                    Console.WriteLine("Digite o sobrenome:");
                    sobrenome = Console.ReadLine();
                    Console.WriteLine("Digite a Idade:");
                    idade = int.Parse(Console.ReadLine());
                    Console.WriteLine("Digite a Data de nascimento (DD/MM/AAAA):");
                    dataNascimento = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Digite a matricula:");
                    matricula = int.Parse(Console.ReadLine());
                    Console.WriteLine("Digite a data de contratação:");
                    dataContratacao = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("digite o salario:");
                    salarioMensal = double.Parse(Console.ReadLine());

                    empregadoNovo = new Empregado(nome, sobrenome, idade, dataNascimento, matricula, dataContratacao, salarioMensal);
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Digite o nome do empregado:");
                    nome = Console.ReadLine();
                    Console.WriteLine("Digite o sobrenome:");
                    sobrenome = Console.ReadLine();
                    Console.WriteLine("Digite a Idade:");
                    idade = int.Parse(Console.ReadLine());
                    Console.WriteLine("Digite a Data de nascimento:");
                    dataNascimento = DateTime.Parse(Console.ReadLine());

                    empregadoNovo = new Empregado(nome, sobrenome, idade, dataNascimento);
                    break;
            }

            if (empregadoNovo != null)
            {
                if (BuscaEmpregado(empregadoNovo.primeironome, empregadoNovo.sobrenome) < 0)
                {
                    EmpregadosList.Add(empregadoNovo);
                    Console.WriteLine("Empregado cadastrado!");
                }
                else
                {
                    Console.WriteLine("Empregado já cadastrado.");
                }
            }
            else
            {
                Console.WriteLine("Empregado não cadastrado!");
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Esse método aqui é para dar aquele "up" no salário do empregado.
        /// Primeiro, ele busca o empregado pelo nome e sobrenome. Se encontrar e o salário já tiver sido definido, 
        /// ele dá um aumento usando o método 'AumentarSalario'.
        /// Mas se o salário não estiver definido ou se não encontrar o empregado, ele irá avisar.
        /// </summary>
        internal void PromoverEmpregado()
        {
            Console.Clear();
            int indiceEmpregado = BuscaEmpregado();
            if (indiceEmpregado >= 0)
            {
                Empregado empregado = EmpregadosList[indiceEmpregado];

                if (indiceEmpregado >= 0 && empregado.SalarioMensal != 0)
                {
                    empregado.AumentarSalario();
                    Console.WriteLine($"Empregado {empregado.primeironome} " + $"{empregado.sobrenome} promovido com sucesso");
                    Console.WriteLine("Salário definido: " + empregado.SalarioMensal.ToString("C"));
                }
                else
                {
                    Console.WriteLine("Salario não definido, verifica o nome ou se funcionário já possui salário!");
                }
            }
            else
            {
                Console.WriteLine("Empregado não encontrado.");
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Esse método serve pra demitir o empregado.
        /// Primeiro, busco o empregado pela sua identificação que seria nome e sobrenome.
        /// Se ele encontrar, ele remove o empregado da lista e avisa que foi demitido.
        /// Mas se não achar, ele avisa que o empregado não foi encontrado.
        /// </summary>
        internal void DemitirEmpregado()
        {
            Console.Clear();
            int indiceEmpregado = BuscaEmpregado();
            if (indiceEmpregado >= 0)
            {
                Empregado empregado = EmpregadosList[indiceEmpregado];
                Console.WriteLine($"Empregado {empregado.primeironome} " + $"{empregado.sobrenome} Demitido com sucesso");
                EmpregadosList.Remove(empregado);
            }
            else
            {
                Console.WriteLine("Empregado não encontado.");
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Aqui completa o cadastro de um empregado. Se por acaso 
        /// esquecer  de adicionar algumas informações quando cadastrar pela primeira vez, 
        /// esse método irá ajudar.
        /// Ele verifica se faltam alguns detalhes como a matrícula, data de contratação ou o salário mensal,
        /// e então me pede para preencher esses campos. Se o empregado que estou tentando completar 
        /// não existir, ele dá um aviso.
        /// </summary>
        internal void CompletarCadastro()
        {
            Console.Clear();
            int index = BuscaEmpregado();
            if (index >= 0)
            {
                Empregado empregadoEncontrado = EmpregadosList[index];

                if (empregadoEncontrado.Matricula == 0)
                {
                    Console.WriteLine("digite a matricula");
                    empregadoEncontrado.Matricula = int.Parse(Console.ReadLine());
                }
                if (empregadoEncontrado.DataContratacao == null)
                {
                    Console.WriteLine("Digite a data de contratação");
                    empregadoEncontrado.DataContratacao = DateTime.Parse(Console.ReadLine());
                }
                if (empregadoEncontrado.SalarioMensal == 0)
                {
                    Console.WriteLine("digite o salario mensal");
                    empregadoEncontrado.SalarioMensal = double.Parse(Console.ReadLine());
                }
                Console.WriteLine("Cadastro completado.");
            }
            else
            {
                Console.WriteLine("Empregado não encontrado!");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// Este método é para buscar um empregado na lista.
        /// Primeiro, pede para digitar o nome e sobrenome do empregado.
        /// Irá procurar na  lista para ver se encontro alguém com esses dados.
        /// Se encontrar, retorno o índice do empregado na lista. Caso contrário, esse valor fica como -1.
        /// </summary>
        private int BuscaEmpregado()
        {
            Console.WriteLine("Digite o nome");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o sobrenome");
            string sobrenome = Console.ReadLine();

            int indiceEmpregado = EmpregadosList.FindIndex(e => e.primeironome.Equals(nome, StringComparison.OrdinalIgnoreCase)
            && e.sobrenome.Equals(sobrenome, StringComparison.OrdinalIgnoreCase));

            return indiceEmpregado;
            /// <summary>
            /// Procura um empregado na lista usando seu nome e sobrenome.
            /// </summary>
            /// <param name="nome">Nome do empregado que está tentando encontrar.</param>
            /// <param name="sobrenome">Sobrenome do empregado que está tentando encontrar.</param>
            /// <returns>Retorna o índice do empregado na lista se encontrado; caso contrário, retorna -1.</returns>

            private int BuscaEmpregado(string? nome, string? sobrenome)
        {
            int indiceEmpregado = EmpregadosList.FindIndex(e => e.primeironome.Equals(nome, StringComparison.OrdinalIgnoreCase)
            && e.sobrenome.Equals(sobrenome, StringComparison.OrdinalIgnoreCase));

            return indiceEmpregado;
        }
            /// <summary>
            /// Aqui  calcula o salário anual do empregado. 
            /// O salário mensal  multiplicado por 12.
            /// </summary>
            internal double SalarioAnual(Empregado empregado)
        {
            return empregado.SalarioMensal * 12;
        }
    }
}
