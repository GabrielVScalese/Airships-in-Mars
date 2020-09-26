using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosMarte
{
    class LigacaoCidade
    {
        private int distancia;
        private int tempo;
        private int custo;

        public LigacaoCidade (int distancia, int tempo, int custo)
        {
            Distancia = distancia;
            Tempo = tempo;
            Custo = custo;
        }

        public int Distancia
        {
            get => distancia;
            set
            {
                if (value < 0)
                    throw new Exception ("Distancia invalida");

                distancia = value;
            }
        }

        public int Tempo
        {
            get => tempo;
            set
            {
                if (value < 0)
                    throw new Exception("Tempo invalido");

                tempo = value;
            }
        }

        public int Custo
        {
            get => custo;
            set
            {
                if (value < 0)
                    throw new Exception("Custo invalido");

                custo = value;
;            }
        }

        public override string ToString()
        {
            return "| Distancia: " + distancia + " | Tempo: " + tempo + " | Custo: " + custo + " |"; 
        }
    }
}
