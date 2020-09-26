using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    public partial class Form1 : Form
    {
        private ArvoreCidades arvoreCidades;
        private GrafoBacktracking grafo;
        public Form1()
        {
            InitializeComponent();
        }

        private void TxtCaminhos_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Buscar caminhos entre cidades selecionadas");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grafo = new GrafoBacktracking(@"C:\Users\gabri\Downloads\CaminhosEntreCidadesMarte.txt");
            arvoreCidades = new ArvoreCidades();
            arvoreCidades.ConstruirArvore(@"C:\Users\gabri\Downloads\CidadesMarte.txt");
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            lsbCidades.Items.Clear();
            string[] cidadesMarte = arvoreCidades.ToString().Split(',');
            for (int i = 0; i < cidadesMarte.Length; i++)
                lsbCidades.Items.Add("\n" + cidadesMarte[i]);

            lsbCidades.Items.Add("\n");
            LigacaoCidade[,] aux = grafo.Matriz;
            for (int i = 0; i < 22; i++)
                for (int j = 0; j < 23; j++)
                    if (aux[i, j] != null)
                        lsbCidades.Items.Add(aux[i, j]);
        }
    }
}
