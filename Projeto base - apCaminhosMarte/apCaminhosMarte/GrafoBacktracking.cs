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

        public PilhaLista<Movimento> BuscarCaminho (int origem, int destino)
        {
            int cidadeAtual = origem;
            int saidaAtual = 0;
            bool[] passou = new bool[23];
            PilhaLista<Movimento> pilhaLista = new PilhaLista<Movimento>();

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
                        break;
                }
                else
                {
                    passou[cidadeAtual] = true;
                    var cidadeAnterior = pilhaLista.Desempilhar();
                    cidadeAtual = cidadeAnterior.Origem;

                    if (pilhaLista.IsVazia())
                        break;
                }
            }

            return pilhaLista;

            Movimento VerificarCidades (int cdAtual, ref bool encontrou)
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

            bool IsFree(int cdAtual, int sAtual)
            {
                bool ret = false;
                if (matriz[cdAtual, sAtual] != null)
                    if (passou[sAtual] != true)
                        ret = true;

                return ret;
            }
        }

        
    }
}
