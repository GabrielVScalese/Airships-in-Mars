using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace apCaminhosMarte
{
    // Nome: Gabriel Villar Scalese     RA: 19171
    // Nome: Nícolas Maisonnette Duarte RA: 19192
    public class ArvoreBinaria<Dado> where Dado : IComparable<Dado>
    {
        // Atributos que representa a raiz
        private NoArvore<Dado> raiz;
        // Atributo que representa o nó atual
        private NoArvore<Dado> atual;
        // Atributo que representa o nó anterior ao nó atual
        private NoArvore<Dado> antecessor;

        // Construtor da classe
        public ArvoreBinaria()
        { }

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
                if (antecessor.Info.CompareTo(info) > 0)
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

        // Método auxiliar que compõe numa string a árvore
        private void Percorrer(NoArvore<Dado> atual, ref string ret)
        {
            if (atual != null)
            {
                ret += atual.Info.ToString() + ", ";
                Percorrer(atual.Esq, ref ret);
                Percorrer(atual.Dir, ref ret);
            }
        }

        // Método que retorna o valor do objeto da classe em formato string
        public override string ToString()
        {
            string ret = "";
            Percorrer(raiz, ref ret);
            return ret;
        }

        // Método que desenha a árvore e seus valores em um componente
        public void DesenharArvore(bool primeiraVez, NoArvore<Dado> raiz, int x, int y, double angulo, double incremento, double comprimento, Graphics g)
        {
            int xf, yf;
            if (raiz != null)
            {
                Pen caneta = new Pen(Color.Red);
                xf = (int)Math.Round(x + Math.Cos(angulo) * comprimento);
                yf = (int)Math.Round(y + Math.Sin(angulo) * comprimento);

                if (primeiraVez)
                    yf = 25;

                g.DrawLine(caneta, x, y, xf, yf);
                DesenharArvore(false, raiz.Esq, xf, yf, Math.PI / 2 + incremento,
                incremento * 0.60, comprimento * 0.8, g);
                DesenharArvore(false, raiz.Dir, xf, yf, Math.PI / 2 - incremento,
                incremento * 0.60, comprimento * 0.8, g);
                SolidBrush preenchimento = new SolidBrush(Color.Blue);
                g.FillEllipse(preenchimento, xf - 25, yf - 15, 42, 30);
                g.DrawString(Convert.ToString(raiz.Info.ToString()), new Font("Comic Sans", 10, FontStyle.Bold),
                new SolidBrush(Color.Black), xf - 23, yf - 7);
            }
        }
    }
}
