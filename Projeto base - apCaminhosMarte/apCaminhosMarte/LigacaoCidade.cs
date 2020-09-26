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
        private int origem;
        private int destino;
        private int distancia;
        private int tempo;
        private int custo;

        public LigacaoCidade (int origem, int destino, int distancia, int tempo, int custo)
        {
            Origem = origem;
            Destino = destino;
            Distancia = distancia;
            Tempo = tempo;
            Custo = custo;
        }

        public int Origem
        {
            get => origem;
            set
            {
                if (value < 0)
                    throw new Exception ("Origem invalida");

                origem = value;
            }
        }

        public int Destino
        {
            get => destino;
            set
            {
                if (value < 0)
                    throw new Exception ("Destino invalido");

                destino = value;
            }
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
            return "| Origem: " + origem + " | Destino: " + destino + " | Distancia: " + distancia + " | Tempo: " + tempo + " | Custo: " + custo + " |"; 
        }
    }
}
