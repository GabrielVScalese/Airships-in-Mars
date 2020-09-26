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

        public void ConstruirArvore(string nomeArquivo)
        {
            if (nomeArquivo == null || nomeArquivo.Equals(""))
                throw new Exception("Nome de arquivo invalido");

            var arquivo = new StreamReader(nomeArquivo);
            arvoreBinaria = new ArvoreBinaria<CidadeMarte>();
            while (!arquivo.EndOfStream)
            {
                string linha = arquivo.ReadLine();
                int id = int.Parse(linha.Substring(0, 3));
                string nomeCidade = linha.Substring(3,15);
                int x = int.Parse(linha.Substring(18, 5));
                int y = int.Parse(linha.Substring(23, 5));

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
