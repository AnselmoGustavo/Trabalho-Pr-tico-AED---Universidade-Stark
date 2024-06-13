using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AED
{
    internal class  Curso
    {
		private string nome;
		public string Nome
		{
			get { return nome; }
			set { nome = value; }
		}

		private int id;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private int numVagas;
		public int NumVagas
		{
			get { return numVagas; }
			set { numVagas = value; }
		}

        private double notaCorte;
        public double NotaCorte
        {
            get { return notaCorte; }
            set { notaCorte = value; }
        }

        private int contadorAprovados;



        public Curso(int id, string nome, int numVagas)
        {
            this.nome = nome;
            this.id = id;
            this.numVagas= numVagas;
            this.notaCorte= 0;
            this.contadorAprovados= 0;
        }

        public List<Candidatos> aprovados=new List<Candidatos>();
        public FilaFlex filaEspera =new FilaFlex();

        //Método para inserir os aprovados
        public void InserirAprovados(Candidatos [] array, Dictionary<int, Curso>x)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i].codCursoOp1 == id)
                {
                    if (!array[i].aprovadoPrimeira)
                    {
                        InserirPrimeiraOpcao(array[i]);
                    }
                }
                else if(array[i].codCursoOp2 == id)
                {
                    if (!array[i].aprovadoPrimeira)
                    {
                        InserirSegundaOpcao(array[i]);
                    }
                }
            }
            int posicaoUltimoAprovado = aprovados.Count;
            notaCorte = aprovados[posicaoUltimoAprovado - 1].Media;
        }

        public void InserirPrimeiraOpcao(Candidatos x)
        {
            if (VerificaVaga())
            {
                x.aprovadoPrimeira = true;
                aprovados.Add(x);
                contadorAprovados++;
            }
            else
            {
                filaEspera.Inserir(x);
            }
        }
        public void InserirSegundaOpcao(Candidatos x)
        {
            if (VerificaVaga())
            {
                aprovados.Add(x);
                contadorAprovados++;
            }
            else
            {
                filaEspera.Inserir(x);
            }      
        }

        private bool VerificaVaga()
        {
            if (contadorAprovados >= numVagas)
            {
                return false;
            }
            return true;
        }

        public void ImprimirAprovados()
        {
            foreach (Candidatos c in aprovados)
            {
                Console.WriteLine(c.Nome + " " + c.Media);
            }
        }
        public void ImprimirFilaEspera()
        {
            filaEspera.Mostrar();
        }
        public override string ToString()
        {
            return $"Nome: {Nome}, número de vagas: {NumVagas}, nota de corte: {NotaCorte}"; 
        }
    }
    class FilaFlex
    {
        private Celula primeiro, ultimo;
        public Celula Primeiro
        {
            get { return primeiro; }
            set { primeiro = value; }
        }
        public Celula Ultimo
        {
            get { return ultimo; }
            set { ultimo = value; }
        }
        public FilaFlex()
        {
            primeiro = new Celula();
            ultimo = primeiro;
        }

        public void Inserir(Candidatos candidato)
        {
            ultimo.Prox = new Celula(candidato);
            ultimo = ultimo.Prox;
        }

        public Candidatos Remover()
        {
            if (primeiro == ultimo)
            {
                throw new Exception("Erro!");
            }
            Celula tmp = primeiro;
            primeiro = primeiro.Prox;
            Candidatos elemento = primeiro.Elemento;
            tmp.Prox = null;
            tmp = null;
            return elemento;
        }

        public void Mostrar()
        {
            Console.Write("[");
            for (Celula i = primeiro.Prox; i != null; i = i.Prox)
            {
                Console.Write(i.Elemento.Nome + ", nota Média: " + i.Elemento.Media + ", primeira opção de curso: " + i.Elemento.codCursoOp1 + ", segunda opção de curso: " + i.Elemento.codCursoOp2);
            }
            Console.WriteLine("]");
        }
    }
    class Celula
    {
        private Candidatos elemento;
        private Celula prox;
        public Celula(Candidatos elemento)
        {
            this.elemento = elemento;
            this.prox = null;
        }
        public Celula()
        {
            this.elemento = null;
            this.prox = null;
        }
        public Celula Prox
        {
            get { return prox; }
            set { prox = value; }
        }
        public Candidatos Elemento
        {
            get { return elemento; }
            set { elemento = value; }
        }
    }
}
