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
    public partial class FrmMapa : Form
    {
        private ArvoreCidades arvoreCidades;
        private GrafoBacktracking grafo;
        private PilhaLista<PilhaLista<Movimento>> caminhos;
        public FrmMapa()
        {
            InitializeComponent();
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

            LimparDados();
            if (caminhos.GetQtd() > 0)
            {
                ExibirCaminhos();
                ExibirMelhorCaminho();
            }
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

        private PilhaLista<Movimento> MelhorCaminho ()
        {
            No<PilhaLista<Movimento>> umCaminho = caminhos.Inicio;
            PilhaLista<Movimento> melhorCaminho = umCaminho.Info;
            while (umCaminho != null)
            {
                if (umCaminho.Prox == null)
                    break;

                if (ObterDistancia(umCaminho.Info) > ObterDistancia(umCaminho.Prox.Info))
                    melhorCaminho = umCaminho.Prox.Info;

                umCaminho = umCaminho.Prox;
            }

            return melhorCaminho;
        }

        private int ObterDistancia (PilhaLista<Movimento> umCaminho)
        {
            No<Movimento> aux = umCaminho.Inicio;
            int distancia = 0;
            while (aux != null)
            {
                distancia += aux.Info.Lc.Distancia;

                aux = aux.Prox;
            }

            return distancia;
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

        private void LimparDados ()
        {
            dgvCaminhos.Rows.Clear();
            dgvMelhorCaminho.Rows.Clear();
        }

        private void ExibirMelhorCaminho ()
        {
            dgvMelhorCaminho.RowCount = 1;

            var melhorCaminho = MelhorCaminho();
            var umMovimento = melhorCaminho.Inicio;
            for (int col = 0; col < 6; col++)
            {
                if (umMovimento == null)
                    break;

                dgvMelhorCaminho[col, 0].Value = umMovimento.Info.ToString();
                umMovimento = umMovimento.Prox;
            }
            
            dgvMelhorCaminho.Refresh();
        }

        private void FrmMapa_Load(object sender, EventArgs e)
        {
            grafo = new GrafoBacktracking(@"C:\Users\gabri\Downloads\CaminhosEntreCidadesMarte.txt");
            arvoreCidades = new ArvoreCidades(@"C:\Users\gabri\Downloads\CidadesMarteOrdenado.txt");
        }

        private void tbControl_Click(object sender, EventArgs e)
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
