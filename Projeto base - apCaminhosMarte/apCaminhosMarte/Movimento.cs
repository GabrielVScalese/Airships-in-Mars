using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class Movimento : IComparable<Movimento>
    {
        private int origem, destino;
        private LigacaoCidade lc;

        public Movimento (int origem, int destino, LigacaoCidade lc)
        {
            Origem = origem;
            Destino = destino;
            Lc = lc;
        }

        public int Origem
        {
            get => origem;
            set
            {
                if (value < 0)
                    throw new Exception("Origem invalida");

                origem = value;
            }
        }

        public int Destino
        {
            get => destino;
            set
            {
                if (value < 0)
                    throw new Exception("Destini invalido");

                destino = value;
            }
        }

        public LigacaoCidade Lc
        {
            get => lc;
            set
            {
                if (value == null)
                    throw new Exception("Ligacao de cidade invalida");

                lc = value;
            }
        }
        
        public int CompareTo (Movimento m)
        {
            return origem.CompareTo(m.origem);
        }

        public override string ToString()
        {
            return "| O: " + origem + " | D:" + destino + " | Dados: " + lc;
        }
    }
}
