using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class ArvoreCidades
    {
        private ArvoreBinaria<CidadeMarte> arvoreBinaria;
        private const int inicioId = 0;
        private const int tamanhoId = 3;
        private const int inicioNomeCidade = inicioId + tamanhoId;
        private const int tamamnhoCidade = 15;
        private const int inicioX = inicioNomeCidade + tamamnhoCidade;
        private const int tamanhoX = 5;
        private const int inicioY = inicioX + tamanhoX;
        private const int tamanhoY = 5;

        public void ConstruirArvore(string nomeArquivo)
        {
            if (nomeArquivo == null || nomeArquivo.Equals(""))
                throw new Exception("Nome de arquivo invalido");

            var arquivo = new StreamReader(nomeArquivo);
            arvoreBinaria = new ArvoreBinaria<CidadeMarte>();
            while (!arquivo.EndOfStream)
            {
                string linha = arquivo.ReadLine();
                int id = int.Parse(linha.Substring(inicioId, tamanhoId));
                string nomeCidade = linha.Substring(inicioNomeCidade, tamamnhoCidade);
                int x = int.Parse(linha.Substring(inicioX, tamanhoX));
                int y = int.Parse(linha.Substring(inicioY, tamanhoY));

                var cidadeMarte = new CidadeMarte(id, nomeCidade, x, y);
                arvoreBinaria.InserirInfo(cidadeMarte);
            }

            arquivo.Close();
        }

        public override string ToString ()
        {
            return arvoreBinaria.ToString();
        }
    }
}
