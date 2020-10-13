using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class ConjuntoCidades
    {
        private int umaCidade;
        private int cidadeAnt;

        public ConjuntoCidades(int umaCidade, int cidadeAnt)
        {
            UmaCidade = umaCidade;
            CidadeAnt = cidadeAnt;
        }

        public int UmaCidade
        {
            get => umaCidade;
            set => umaCidade = value;
        }

        public int CidadeAnt
        {
            get => cidadeAnt;
            set => cidadeAnt = value;
        }

        public override bool Equals (Object obj)
        {
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            if (!this.GetType().Equals(obj.GetType()))
                return false;

            ConjuntoCidades cc = (ConjuntoCidades)obj;

            if (umaCidade != cc.umaCidade)
                return false;

            if (cidadeAnt != cc.cidadeAnt)
                return false;

            return true;
        }

        public override string ToString()
        {
            return "umaCidade: " + umaCidade + " cidadeAnt: " + cidadeAnt;
        }
    }
}
