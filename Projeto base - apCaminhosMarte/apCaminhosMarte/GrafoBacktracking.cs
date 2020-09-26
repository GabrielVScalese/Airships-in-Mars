using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class GrafoBacktracking
    {
        private LigacaoCidade[,] matriz;
        private string nomeArquivo;
        private const int inicioOrigem = 0;
        private const int tamanhoOrigem = 3;
        private const int inicioDestino = inicioOrigem + tamanhoOrigem;
        private const int tamanhoDestino = 3;
        private const int inicioDistancia = inicioDestino + tamanhoDestino;
        private const int tamanhoDistancia = 5;
        private const int inicioTempo = inicioDistancia + tamanhoDistancia;
        private const int tamanhoTempo = 4;
        private const int inicioCusto = inicioTempo + tamanhoTempo;
        private const int tamanhoCusto = 5;

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
                int origem = int.Parse(linha.Substring(inicioOrigem, tamanhoOrigem));
                int destino = int.Parse(linha.Substring(inicioDestino, tamanhoDestino));
                int distancia = int.Parse(linha.Substring(inicioDistancia, tamanhoDistancia));
                int tempo = int.Parse(linha.Substring(inicioTempo, tamanhoTempo));
                int custo = int.Parse(linha.Substring(inicioCusto, tamanhoCusto));

                var ligacaoCidade = new LigacaoCidade(distancia, tempo, custo);
                matriz[origem, destino] = ligacaoCidade;
            }

            arquivo.Close();
        }
    }
}
