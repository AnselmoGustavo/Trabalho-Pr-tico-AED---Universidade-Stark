using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AED
{
    internal class Faculdade
    {
        static void Main(string[] args)
        {
            ProcessadorCursos processador= new ProcessadorCursos();
            int quantidadeCursos = processador.cursosEcandidatos[0];
            int quantidadeCandidatos = processador.listaCandidatos.Length;

            processador.ImprimirListaCandidatos();

            try
            {
                StreamWriter arq = new StreamWriter("E:\\Users\\gusta\\Área de Trabalho\\ArquivoSaidaTrabalhoAED.txt", true, Encoding.UTF8);
                int quantLinhas = quantidadeCursos * 3 + quantidadeCandidatos;

                foreach (Curso c in processador.todosOsCursos.Values)
                {
                    arq.WriteLine(c.Nome + " " + c.NotaCorte);
                    arq.WriteLine("Selecionados");
                    foreach (Candidatos aprovados in c.aprovados)
                    {
                        arq.WriteLine(aprovados.Nome + " " + aprovados.Media);
                    }
                    arq.WriteLine("Fila de Espera");
                    Celula atual = c.filaEspera.Primeiro.Prox;//Passar por cada célula da fila flexível
                    while (atual != null)
                    {
                        arq.WriteLine(atual.Elemento.Nome + " " + atual.Elemento.Media);
                        atual = atual.Prox;
                    }
                }
                arq.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            Console.WriteLine("Arquivo de Saída Criado! Muito obrigado.");
            Console.ReadLine();
        }
    }

    class ProcessadorCursos
    {
        private string linha;
        private string[] dados;
        public int[] cursosEcandidatos = new int[2];
        public Dictionary<int, Curso> todosOsCursos = new Dictionary<int, Curso>();
        private int contador = 0;
        private int contadorCandidatos = 0;
        public Candidatos[] listaCandidatos;

        public ProcessadorCursos()
        {
            ProcessarDados();
            Quicksort(listaCandidatos, 0, listaCandidatos.Length-1);
            foreach (Curso curso in todosOsCursos.Values)
            {
                curso.InserirAprovados(listaCandidatos, todosOsCursos);
            }
        }

        private void ProcessarDados()
        {
            try
            {
                //Abre arquivo para leitura
                StreamReader arquivoDados = new StreamReader("E:\\Users\\gusta\\Área de Trabalho\\Trabalho AED\\Trabalho AED\\ArquivoDados2.txt", Encoding.UTF8);
                linha = arquivoDados.ReadLine();//Começa a ler o arquivo

                //salva o número de cursos e quantidade de candidatos
                dados = linha.Split(';');
                cursosEcandidatos[0] = int.Parse(dados[0]);
                cursosEcandidatos[1] = int.Parse(dados[1]);

                listaCandidatos = new Candidatos[int.Parse(dados[1])];

                //linha = arquivoDados.ReadLine();//Pula a primeira linha
                //while (contador < cursosEcandidatos[0])
                for(int i = 0; i < cursosEcandidatos[0]; i++)
                {
                    linha = arquivoDados.ReadLine();
                    dados = linha.Split(';');
                    Curso curso = new Curso(int.Parse(dados[0]), dados[1], int.Parse(dados[2]));
                    todosOsCursos[curso.Id] = curso;
                    //contador++;
                    //linha = arquivoDados.ReadLine();
                }
                for (int i = 0; i < cursosEcandidatos[1]; i++)
                {
                    linha = arquivoDados.ReadLine();
                    dados = linha.Split(';');
                    Candidatos candidato = new Candidatos(dados[0], int.Parse(dados[1]), int.Parse(dados[2]), int.Parse(dados[3]), int.Parse(dados[4]), int.Parse(dados[5]));
                    listaCandidatos[i]=candidato;
                    //contadorCandidatos++;
                    //linha = arquivoDados.ReadLine();
                }
                arquivoDados.Close();//Fecha o arquivo
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private void Quicksort(Candidatos[] array, int esq, int dir)
        {
            int i = esq, j = dir;
            double pivo = array[(esq + dir) / 2].Media;
            double pivoRedacao = array[(esq + dir) / 2].NotaRedacao;
            while (i <= j)
            {
                while ((array[i].Media > pivo) || (array[i].Media == pivo && array[i].NotaRedacao < pivoRedacao))
                    i++;
                while ((array[j].Media < pivo) || (array[j].Media == pivo && array[j].NotaRedacao > pivoRedacao))
                    j--;
                if (i <= j)
                { Trocar(array, i, j); i++; j--; }
            }
            if (esq < j)
                Quicksort(array, esq, j);
            if (i < dir)
                Quicksort(array, i, dir);
        }

        private void Trocar(Candidatos [] array, int i, int j)
        {
            Candidatos temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public void ImprimirListaCandidatos()
        {
            for(int i=0;i<listaCandidatos.Length;i++)
            {
                Console.Write(listaCandidatos[i].Nome + ", nota Matemática: " + listaCandidatos[i].NotaMat + ", nota Linguagens: " + listaCandidatos[i].NotaLing + ", nota Redação: " + listaCandidatos[i].NotaRedacao + ", média das notas: "+ listaCandidatos[i].Media + ", primeira opção de curso: " + listaCandidatos[i].codCursoOp1 + ", segunda opção de curso: " + listaCandidatos[i].codCursoOp2);
                Console.WriteLine();
            }
        }
        public void ImprimirDicionarioCursos()
        {
            for (int i = 0; i < todosOsCursos.Count; i++)
            {
                var curso = todosOsCursos.ElementAt(i);
                Console.WriteLine("Código do curso: " + curso.Key + curso.Value);
            }
        }
    }
}
