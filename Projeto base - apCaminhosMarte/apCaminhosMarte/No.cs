using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class No<Dado>
    {
        private Dado info;
        private No<Dado> prox;

        public No(Dado info, No<Dado> prox)
        {
            Info = info;
            Prox = prox;
        }

        public No(Dado info)
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

        public No<Dado> Prox
        {
            get => prox;
            set
            {
                prox = value;
            }
        }
    }
}
