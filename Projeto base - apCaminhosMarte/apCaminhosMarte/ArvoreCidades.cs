using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    // Nome: Gabriel Villar Scalese    RA: 19171
    // Nome: Nícolas Maisonette Duarte RA: 19192
    class ArvoreCidades
    {
        // Atributo que representa uma árvore
        private ArvoreBinaria<CidadeMarte> arvoreBinaria;

        // Construtor da classe
        public ArvoreCidades (string nomeArquivo)
        {
            ConstruirArvore(nomeArquivo);
        }

        // Método que lê de um arquivo texto e constroe uma árvore a partir dele
        private void ConstruirArvore(string nomeArquivo)
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

        // Método que encontra uma cidade na da árvore a partir do id
        public CidadeMarte GetCidade (int idCidade) 
        {
            CidadeMarte ret = null;
            return VisitarCidades(arvoreBinaria.Raiz, idCidade, ret);
        }

        // Método que percorre a árvore para encontrar a cidade a partir do id
        private CidadeMarte VisitarCidades (NoArvore<CidadeMarte> atual, int idCidade, CidadeMarte cm)
        {
            CidadeMarte ret = null;
            if (atual != null)
            {
                if (atual.Info.Id == idCidade)
                {
                    ret = atual.Info;
                    return ret;
                }
                   
               ret = VisitarCidades(atual.Esq, idCidade, ret);
               if (ret == null)
                   ret = VisitarCidades(atual.Dir, idCidade, ret);
            }

            return ret;
        }

        // Método que retorna o valor do objeto da classe em formato string 
        public override string ToString ()
        {
            return arvoreBinaria.ToString();
        }
    }
}
