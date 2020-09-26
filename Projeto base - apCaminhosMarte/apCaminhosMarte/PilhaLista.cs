using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    class PilhaLista<Dado>
    {
        private ListaSimples<Dado> listaSimples;

        public PilhaLista ()
        {
            listaSimples = new ListaSimples<Dado>();
        }

        public void Empilhar (Dado info)
        {
            listaSimples.InserirNoFim(info);
        }

        public Dado Desempilhar ()
        {
            Dado topo = listaSimples.GetDoFim();
            listaSimples.RemoverDoFim();

            return topo;
        }

        public No<Dado> Inicio
        {
            get => listaSimples.Primeiro;
        }

        public Dado Topo
        {
            get => listaSimples.GetDoFim();
        }

        public bool IsVazia ()
        {
            return listaSimples.IsVazia();
        }

        public int GetQtd ()
        {
            return listaSimples.GetQtd();
        }

        public Object Clone ()
        {
            PilhaLista<Dado> ret = null;
            try
            {
                ret = new PilhaLista<Dado>(this);
            }
            catch (Exception e)
            { }

            return ret;
        }

        public bool ExistsInfo (Dado info)
        {
            return listaSimples.ExistsInfo(info);
        }

        public PilhaLista(PilhaLista<Dado> modelo)
        {
            if (modelo == null)
                throw new Exception("Modelo ausente");

            listaSimples = (ListaSimples<Dado>) modelo.listaSimples.Clone();
        }

        public override string ToString()
        {
            return listaSimples.ToString();
        }
    }
}
