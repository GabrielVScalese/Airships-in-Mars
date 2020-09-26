using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    class PilhaLista<Dado> where Dado : IComparable<Dado>
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

        public Dado Topo
        {
            get => listaSimples.GetDoFim();
        }

        public bool IsVazia ()
        {
            return listaSimples.IsVazia();
        }

        public override string ToString()
        {
            return listaSimples.ToString();
        }
    }
}
