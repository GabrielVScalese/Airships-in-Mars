using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace apCaminhosMarte
{
    class ListaSimples<Dado>
    {
        private No<Dado> primeiro, ultimo;

        public ListaSimples ()
        { }

        public No<Dado> Primeiro
        {
            get => primeiro;
        }

        public void InserirNoFim (Dado info)
        {
            if (info == null)
                throw new Exception("Informacao invalida");

            if (IsVazia())
            {
                primeiro = new No<Dado>(info, primeiro);
                ultimo = primeiro;
            }
            else
            {
                No<Dado> aux = new No<Dado>(info, null);
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
                No<Dado> aux = primeiro;
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
            No<Dado> aux = primeiro;
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

        public bool ExistsInfo (Dado info)
        {
            No<Dado> aux = primeiro;
            bool ret = false;
            while (aux != null)
            {
                if (aux.Info.Equals(info))
                {
                    ret = true;
                    break;
                }
                    
                aux = aux.Prox;
            }

            return ret;
        }

        public override string ToString()
        {
            string ret = "{ ";
            No<Dado> aux = primeiro;
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

        public Object Clone ()
        {
            ListaSimples<Dado> ret = null;
            try
            {
                ret = new ListaSimples<Dado>(this);
            }
            catch (Exception e)
            { }

            return ret;
        }

        public ListaSimples(ListaSimples<Dado> modelo)
        {
            if (modelo == null)
                throw new Exception("Modelo ausente");

            No<Dado> modeloAux = modelo.primeiro;
            while (modeloAux != null)
            {
                InserirNoFim(modeloAux.Info);

                modeloAux = modeloAux.Prox;
            }
        }
    }
}
