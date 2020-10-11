﻿using System;
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
    // Nome: Gabriel Villar Scalese     RA: 19171
    // Nome: Nícolas Maisonnette Duarte RA: 19192
    class GrafoBacktracking
    {
        // Atributo que representa um grafo
        private LigacaoCidade[,] matriz;
        // Atributo que representa o nome de um arquivo texto
        private string nomeArquivo;

        // Construtor da classe
        public GrafoBacktracking (string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
            Matriz = new LigacaoCidade[23, 23];
            ConstruirGrafo();
        }

        // Propriedade do atributo nomeArquivo
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

        // Propriedade do atributo matriz
        public LigacaoCidade[,] Matriz
        {
            get => matriz;
            set
            {
                if (value == null)
                    throw new Exception("Matriz invalida");

                matriz = value;
            }
        }

        // Método que constroe um grafo a partir de um arquivo texto
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

        // Método que irá obter todos os caminhos possíveis entre duas cidades
        public PilhaLista<PilhaLista<Movimento>> GerarCaminhos (int origem, int destino)
        {
            var pilhaLista = new PilhaLista<Movimento>();
            var caminhos = new PilhaLista<PilhaLista<Movimento>>();
            var passou = new bool[23];

            return BuscarCaminhos(origem, destino, pilhaLista, passou, caminhos);
        }

        // Método que procura todos os caminhos possíveis entre duas cidades
        private PilhaLista<PilhaLista<Movimento>> BuscarCaminhos (int origem, int destino, PilhaLista<Movimento> caminho, bool[] passouCidades, PilhaLista<PilhaLista<Movimento>> caminhos)
        {
            int cidadeAtual = origem;
            bool[] passou = passouCidades;
            PilhaLista<Movimento> pilhaLista = caminho;

            for (; ; )
            {
                bool achouCidade = false;
                var cidadeEncontrada = VerificarCidades (cidadeAtual, ref achouCidade); // Obtenção ou não de uma saída
                if (achouCidade == true)
                {
                    pilhaLista.Empilhar(new Movimento(cidadeAtual, cidadeEncontrada.Destino, cidadeEncontrada.Lc)); // Empilha na pilha parcial de soluções, o movimento realizado
                    passou[cidadeAtual] = true;
                    cidadeAtual = cidadeEncontrada.Destino;

                    if (cidadeAtual == destino) // Uma solução foi encontrada
                    {
                        PilhaLista<Movimento> caminhoClone = (PilhaLista<Movimento>)pilhaLista.Clone();
                        caminhos.Empilhar(caminhoClone);
                        var cidadeAnterior = pilhaLista.Desempilhar();
                        cidadeAtual = cidadeAnterior.Origem;
                        BuscarCaminhos (cidadeAtual, destino, pilhaLista, passou, caminhos);

                        return caminhos;
                    }
                }
                else
                {
                    if (pilhaLista.IsVazia()) // Soluções esgotaram
                        break;

                    passou[cidadeAtual] = true;
                    var cidadeAnterior = pilhaLista.Desempilhar();
                    cidadeAtual = cidadeAnterior.Origem;
                }
            }

            return caminhos;

            Movimento VerificarCidades(int cdAtual, ref bool encontrou) // Verifica alguma saída possível
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

            bool ExistsMovimento (int cdAtual, int sAtual) // Verifica se o movimento gerado já foi feito
            {
                if (matriz[cdAtual, sAtual] == null)
                    return false;

                bool ret = false;
                No<PilhaLista<Movimento>> aux = caminhos.Inicio;

                while (aux != null)
                { 
                    if (aux.Info.ExistsInfo(new Movimento(cdAtual, destino, matriz[cdAtual, sAtual])))
                    {
                        ret = true;
                        break;
                    }

                    aux = aux.Prox;
                }

                return ret;
            }

            bool IsFree(int cdAtual, int sAtual) // Verifica se posição atual é válida
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
