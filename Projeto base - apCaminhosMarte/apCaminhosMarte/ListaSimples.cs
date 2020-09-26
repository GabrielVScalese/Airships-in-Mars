using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace apCaminhosMarte
{
    class ListaSimples<Dado> where Dado : IComparable<Dado>
    {
        private class No
        {
            private Dado info;
            private No prox;

            public No (Dado info, No prox)
            {
                Info = info;
                Prox = prox;
            }

            public No (Dado info)
            {
                Info = info;
            }

            public Dado Info
            {
                get => info;
                set
                {
                    info = value;
                }
            }

            public No Prox
            {
                get => prox;
                set
                {
                    prox = value;
                }
            }
        }

        private No primeiro, ultimo;

        public ListaSimples ()
        { }


        public void InserirNoFim (Dado info)
        {
            if (info == null)
                throw new Exception("Informacao invalida");

            if (IsVazia())
            {
                primeiro = new No(info, primeiro);
                ultimo = primeiro;
            }
            else
            {
                No aux = new No(info, null);
                ultimo.Prox = aux;
                ultimo = aux;
            }
        }

        public void RemoverDoFim ()
        {
            if (IsVazia())
                throw new Exception("Nada a remover");

            if (GetQtd() == 1)
            {
                primeiro = null;
                ultimo = null;
            }
            else
            {
                No aux = primeiro;
                while (aux != null)
                {
                    if (aux.Prox.Equals(ultimo))
                    {
                        aux.Prox = null;
                        ultimo = aux;
                        break;
                    }

                    aux = aux.Prox;
                }
            }
        }

        public bool IsVazia ()
        {
            return primeiro == null;
        }

        public int GetQtd ()
        {
            int qtd = 0;
            No aux = primeiro;
            while (aux != null)
            {
                qtd++;
                aux = aux.Prox;
            }

            return qtd;
        }

        public Dado GetDoFim ()
        {
            if (IsVazia())
                throw new Exception("Lista vazia");

            return ultimo.Info;
        }

        public override string ToString()
        {
            String ret = "{ ";
            No aux = primeiro;
            while (aux != null)
            {
                if (aux.Prox == null)
                    ret += aux.Info;
                else
                    ret += aux.Info + ", ";

                aux = aux.Prox;
            }

            return ret + " }";
        }
    }
}
