using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class GrafoBacktracking
    {
        private LigacaoCidade[,] matriz;
        private string nomeArquivo;

        public GrafoBacktracking (string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
            matriz = new LigacaoCidade[23, 23];
            ConstruirGrafo();
        }

        public string NomeArquivo
        {
            get => nomeArquivo;
            set
            {
                if (value == null || value.Equals(""))
                    throw new Exception("Nome de arquivo invalido");

                nomeArquivo = value;
            }
        }

        public LigacaoCidade[,] Matriz
        {
            get => matriz;
        }

        private void ConstruirGrafo ()
        {
            var arquivo = new StreamReader(nomeArquivo);
            while (!arquivo.EndOfStream)
            {
                string linha = arquivo.ReadLine();
                int origem = int.Parse(linha.Substring(0, 3));
                int destino = int.Parse(linha.Substring(3, 3));
                int distancia = int.Parse(linha.Substring(6, 5));
                int tempo = int.Parse(linha.Substring(11, 4));
                int custo = int.Parse(linha.Substring(15, 5));

                var ligacaoCidade = new LigacaoCidade(distancia, tempo, custo);
                matriz[origem, destino] = ligacaoCidade;
            }

            arquivo.Close();
        }

        public PilhaLista<PilhaLista<Movimento>> GerarCaminhos (int origem, int destino)
        {
            int anterior = 0;
            var pilhaLista = new PilhaLista<Movimento>();
            var caminhos = new PilhaLista<PilhaLista<Movimento>>();
            var anteriores = new PilhaLista<int>();
            var passou = new bool[23];

            return BuscarCaminhos(origem, destino, pilhaLista, passou, caminhos, anteriores);
        }

        private PilhaLista<PilhaLista<Movimento>> BuscarCaminhos (int origem, int destino, PilhaLista<Movimento> caminho, bool[] passouCidades, PilhaLista<PilhaLista<Movimento>> caminhos, PilhaLista<int> anteriores)
        {
            int cidadeAtual = origem;
            bool[] passou = passouCidades;
            PilhaLista<Movimento> pilhaLista = caminho;
            PilhaLista<int> ant = anteriores;

            for (; ; )
            {
                bool achouCidade = false;
                var cidadeEncontrada = VerificarCidades(cidadeAtual, ref achouCidade);
                if (achouCidade == true)
                {
                    pilhaLista.Empilhar(new Movimento(cidadeAtual, cidadeEncontrada.Destino, cidadeEncontrada.Lc));
                    passou[cidadeAtual] = true;
                    cidadeAtual = cidadeEncontrada.Destino;

                    if (cidadeAtual == destino)
                    {
                        PilhaLista<Movimento> caminhoClone = (PilhaLista<Movimento>)pilhaLista.Clone();
                        caminhos.Empilhar(caminhoClone);
                        var cidadeAnterior = pilhaLista.Desempilhar();
                        cidadeAtual = cidadeAnterior.Origem;
                        passou[cidadeAtual] = true;
                        ant.Empilhar(destino);
                        BuscarCaminhos (cidadeAtual, destino, pilhaLista, passou, caminhos, ant);
                        return caminhos;
                    }
                }
                else
                {

                    if (pilhaLista.IsVazia())
                        break;

                    passou[cidadeAtual] = true;
                    var cidadeAnterior = pilhaLista.Desempilhar();
                    cidadeAtual = cidadeAnterior.Origem;
                }
            }

            return caminhos;

            Movimento VerificarCidades(int cdAtual, ref bool encontrou)
            {
                Movimento ret = null;
                for (int j = 0; j < 23; j++)
                    if (IsFree(cdAtual, j))
                    {
                        ret = new Movimento(cdAtual, j, matriz[cdAtual, j]);
                        encontrou = true;
                        break;
                    }


                return ret;
            }

            bool ExistsMovimento (int cdAtual, int sAtual)
            {
                bool ret = false;
                No<PilhaLista<Movimento>> aux = caminhos.Inicio;
                while (aux != null)
                {
                    if (matriz[cdAtual, sAtual] == null)
                    {
                        aux = aux.Prox;
                        continue;
                    }
                       
                    if (aux.Info.ExistsInfo(new Movimento(cdAtual, destino, matriz[cdAtual, sAtual])))
                    {
                        ret = true;
                        break;
                    }

                    aux = aux.Prox;
                }

                return ret;
            }

            bool IsFree(int cdAtual, int sAtual)
            {
                bool ret = false;

                if (ExistsMovimento(cdAtual, sAtual))
                    return false;
                
                if (matriz[cdAtual, sAtual] != null)
                    if (passou[sAtual] != true)
                        ret = true;
               
                return ret;
            }
        }
        
    }
}
