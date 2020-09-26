using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace apCaminhosMarte
{
    // Nome: Gabriel Villar Scalese    RA: 19171
    // Nome: Nícolas Maisonette Duarte RA: 19192
    public class ArvoreBinaria<Dado> where Dado : IComparable<Dado>
    {
        // Atributos que representa a raiz
        private NoArvore<Dado> raiz;
        // Atributo que representa o nó atual
        private NoArvore<Dado> atual; 
        // Atributo que representa o nó anterior ao nó atual
        private NoArvore<Dado> antecessor;

        // Construtor da classe
        public ArvoreBinaria ()
        {}

        // Propriedade do atributo raiz
        public NoArvore<Dado> Raiz
        {
            get => raiz;
            set
            {
                raiz = value;
            }
        }

        // Propriedade do atributo atual
        public NoArvore<Dado> Atual
        {
            get => atual;
            set
            {
                atual = value;
            }
        }

        // Propriedade do atributo antecessor
        public NoArvore<Dado> Antecessor
        {
            get => antecessor;
            set
            {
                antecessor = value;
            }
        }
        
        // Método de inserção de informações na árvore binária (PRÉ-ORDEM)
        public void InserirInfo(Dado info)
        {
            if (info == null)
                throw new Exception("Parametro invalido");

            if (raiz == null)
            {
                raiz = new NoArvore<Dado>(info, null, null);
                atual = raiz;
                antecessor = raiz;
                return;
            }
            else
            {
                if (atual.Info.CompareTo(info) > 0)
                {
                    antecessor = raiz;
                    PercorrerParaInserir(info, atual.Esq);
                }
                else
                {
                    antecessor = raiz;
                    PercorrerParaInserir(info, atual.Dir);
                }
                    
            }
        }

        // Método auxiliar que percorre a árvore (PRÉ-ORDEM) e insere a informação desejada
        private void PercorrerParaInserir(Dado info, NoArvore<Dado> atual)
        {
            if (atual == null)
            {
                if (antecessor.Esq == null)
                {
                    atual = new NoArvore<Dado>(info, null, null);
                    antecessor.Esq = atual;
                }
                else
                {
                    atual = new NoArvore<Dado>(info, null, null);
                    antecessor.Dir = atual;
                }

                return;
            }

            if (atual.Info.CompareTo(info) > 0)
            {
                antecessor = atual;
                PercorrerParaInserir(info, atual.Esq);
            }
            else
            {
                antecessor = atual;
                PercorrerParaInserir(info, atual.Dir);
            }
        }

        /*// Método provisório
        public void ExibirArvoreBinaria()
        {
            Percorrer(raiz);
        }*/

        // Método provisório
        private void Percorrer(NoArvore<Dado> atual, ref string ret)
        {
            if (atual != null)
            {
                //Console.WriteLine(atual.Info);
                ret += atual.Info.ToString() + ", ";
                Percorrer(atual.Esq, ref ret);
                Percorrer(atual.Dir, ref ret);
            }
        }

        public override string ToString ()
        {
            string ret = "";
            Percorrer(raiz, ref ret);
            return ret;
        }
    }
}
