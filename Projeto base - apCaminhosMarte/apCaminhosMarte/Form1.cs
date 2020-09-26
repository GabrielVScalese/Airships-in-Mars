using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    public partial class Form1 : Form
    {
        private ArvoreCidades arvoreCidades;
        private GrafoBacktracking grafo;
        private PilhaLista<PilhaLista<Movimento>> caminhos;
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
            int idOrigem = GetOrigem();
            int idDestino = GetDestino();

            if (idOrigem == -1 || idDestino == -1)
            {
                MessageBox.Show("Cidades inválidas");
                return;
            }

            caminhos = grafo.GerarCaminhos(idOrigem, idDestino);
            if (caminhos.GetQtd() == 0)
                MessageBox.Show("Nenhum caminho foi encontrado!");
            else
                MessageBox.Show("Número de caminhos encontrados: " + caminhos.GetQtd().ToString());

            ExibirCaminhos();
        }

        private int GetOrigem ()
        {
            if (lsbOrigem.SelectedItem == null)
                return -1;

            string linhaSelecionada = lsbOrigem.SelectedItem.ToString();
            int id = int.Parse(linhaSelecionada.Split('-')[0].Trim());

            return id;
        }

        private int GetDestino ()
        {
            if (lsbOrigem.SelectedItem == null)
                return -1;

            string linhaSelecionada = lsbDestino.SelectedItem.ToString();
            int id = int.Parse(linhaSelecionada.Split('-')[0].Trim());

            return id;
        }

        private void ExibirCaminhos ()
        {
            dgvCaminhos.RowCount = caminhos.GetQtd();

            var umCaminho = caminhos.Inicio;
            for (int lin = 0; lin < caminhos.GetQtd(); lin++)
            {
                var umMovimento = umCaminho.Info.Inicio;
                for (int col = 0; col < 6; col++)
                {
                    if (umMovimento == null)
                        break;

                    dgvCaminhos[col, lin].Value = umMovimento.Info.ToString();
                    umMovimento = umMovimento.Prox;
                }

                umCaminho = umCaminho.Prox;
            }

            dgvCaminhos.Refresh();
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
                if (i == 0)
                    lsbCidades.Items.Add(" " + cidadesMarte[i]);
                else
                    lsbCidades.Items.Add("\n" + cidadesMarte[i]);
        }
    }
}
