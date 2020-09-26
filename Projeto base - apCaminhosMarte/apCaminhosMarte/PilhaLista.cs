using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class PilhaLista<Dado> where Dado : IComparable<Dado>
    {
        private ListaSimples<Dado> listaSimples;

        public PilhaLista ()
        { }

        public void Empilhar (Dado info)
        {
            listaSimples.InserirNoFim(info);
        }

        public void Desempilhar ()
        {
            listaSimples.RemoverDoFim();
        }

        public Dado Topo
        {
            get => listaSimples.GetDoFim();
        }
    }
}
