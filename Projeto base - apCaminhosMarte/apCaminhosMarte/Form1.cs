﻿using System;
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
        private Image imageClone;

        public FrmMapa()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            int idOrigem = GetOrigem();
            int idDestino = GetDestino();

            if (idOrigem == -1 || idDestino == -1)
            {
                MessageBox.Show("Cidades inválidas");
                return;
            }

            if (idOrigem == idDestino)
            {
                MessageBox.Show("Destino é igual à origem!");
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
            if (lsbDestino.SelectedItem == null)
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

        private int MaiorNumeroMovimentos ()
        {
            No<PilhaLista<Movimento>> umCaminho = caminhos.Inicio;
            int qtd = 0;
            int ant = 0;
            while (umCaminho != null)
            {
                qtd = umCaminho.Info.GetQtd();

                if (ant < qtd)
                    ant = qtd;

                umCaminho = umCaminho.Prox;
            }

            return ant;
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

        private void InicializarColunas (int numeroColunas, DataGridView dgv)
        {
            for (int col = 0; col < numeroColunas; col++)
                dgv.Columns[col].HeaderText = "Cidade";
        }

        private void ExibirCaminhos ()
        {
            dgvCaminhos.RowCount = caminhos.GetQtd();
            dgvCaminhos.ColumnCount = MaiorNumeroMovimentos() + 1;

            InicializarColunas(dgvCaminhos.ColumnCount, dgvCaminhos);

            var umCaminho = caminhos.Inicio;
            for (int lin = 0; lin < caminhos.GetQtd(); lin++)
            {
                var umMovimento = umCaminho.Info.Inicio;
                for (int col = 0; col < dgvCaminhos.ColumnCount; col++)
                {
                    if (umMovimento == null)
                        break;

                    var primeiraCidade = arvoreCidades.GetCidade(umMovimento.Info.Origem);
                    var segundaCidade = arvoreCidades.GetCidade(umMovimento.Info.Destino);
                    dgvCaminhos[col, lin].Value = primeiraCidade.NomeCidade;
                    dgvCaminhos[col + 1, lin].Value = segundaCidade.NomeCidade;

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
            pbMapa.Refresh();
        }

        private void ExibirMelhorCaminho ()
        {
            var melhorCaminho = MelhorCaminho();
            dgvMelhorCaminho.RowCount = 1;
            dgvMelhorCaminho.ColumnCount = melhorCaminho.GetQtd() + 1;

            var umMovimento = melhorCaminho.Inicio;
            for (int col = 0; col < dgvMelhorCaminho.ColumnCount; col++)
            {
                if (umMovimento == null)
                    break;

                var primeiraCidade = arvoreCidades.GetCidade(umMovimento.Info.Origem);
                var segundaCidade = arvoreCidades.GetCidade(umMovimento.Info.Destino);
                dgvMelhorCaminho[col, 0].Value = primeiraCidade.NomeCidade;
                dgvMelhorCaminho[col + 1, 0].Value = segundaCidade.NomeCidade;

                umMovimento = umMovimento.Prox;
            }
            
            dgvMelhorCaminho.Refresh();
        }

        private void FrmMapa_Load(object sender, EventArgs e)
        {
            grafo = new GrafoBacktracking(@"C:\Users\gabri\Downloads\CaminhosEntreCidadesMarte.txt");
            arvoreCidades = new ArvoreCidades(@"C:\Users\gabri\Downloads\CidadesMarte.txt");

            imageClone = (Image)pbMapa.Image.Clone();
        }

        private void tbControl_Click(object sender, EventArgs e)
        {
            /*Graphics g = lsbCidades.CreateGraphics();
            arvoreCidades.DesenharCidades(lsbCidades.Width / 2, 600, g, 100, 10);*/
        }

        private void DesenharCaminho (PilhaLista<Movimento> umCaminho)
        {
            pbMapa.Refresh();
            No<Movimento> umMovimento = umCaminho.Inicio;
            while (umMovimento != null)
            {
                var pontoInicial = arvoreCidades.GetCidade(umMovimento.Info.Origem);
                var pontoFinal = arvoreCidades.GetCidade(umMovimento.Info.Destino);

                double x = pontoInicial.X;
                double y = pontoInicial.Y;
                double xf = pontoFinal.X;
                double yf = pontoFinal.Y;

                GetProporcao(ref x, ref y);
                GetProporcao(ref xf, ref yf);

                Pen caneta = new Pen(Color.FromArgb(128, 0, 0, 255), 15);
                caneta.Width = 2;

                Graphics g = pbMapa.CreateGraphics();
                g.FillEllipse(new SolidBrush(Color.Black), Convert.ToInt32(x), Convert.ToInt32(y), 6, 6);
                g.DrawString(pontoInicial.NomeCidade.Trim(), new Font("Comic Sans", 10), new SolidBrush(Color.Black), Convert.ToInt32(x - 10), Convert.ToInt32(y - 20));
                g.FillEllipse(new SolidBrush(Color.Black), Convert.ToInt32(xf), Convert.ToInt32(yf), 6, 6);
                g.DrawString(pontoFinal.NomeCidade.Trim() + " (" + umMovimento.Info.Lc.Distancia.ToString() + ")", new Font("Comic Sans", 10), new SolidBrush(Color.Black), Convert.ToInt32(xf - 10), Convert.ToInt32(yf - 20));
                g.DrawLine(caneta, Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(xf), Convert.ToInt32(yf));
             
                umMovimento = umMovimento.Prox;
            }
        }

        private PilhaLista<Movimento> ObterUmCaminho(int indiceCaminho)
        {
            No<PilhaLista<Movimento>> aux = caminhos.Inicio;
            for (int i = 0; i < caminhos.GetQtd(); i++)
            {
                if (i == indiceCaminho)
                    return aux.Info;
                else
                    aux = aux.Prox;
            }

            return null;
        }

        private void GetProporcao (ref double x, ref double y)
        {
            double proporcaoX = pbMapa.Size.Width / 4096.0;
            double proporcaoY = pbMapa.Size.Height / 2048.0;

            x = x * proporcaoX;
            y = y * proporcaoY;
        }

        private void dgvCaminhos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var umCaminho = ObterUmCaminho(dgvCaminhos.SelectedCells[0].RowIndex);
            DesenharCaminho(umCaminho);
        }

        private void dgvMelhorCaminho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var umCaminho = ObterUmCaminho(dgvMelhorCaminho.SelectedCells[0].RowIndex);
            DesenharCaminho(umCaminho);
        }
    }
}
