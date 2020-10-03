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
        private Image imageClone;

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

        private void ExibirCaminhos ()
        {
            dgvCaminhos.RowCount = caminhos.GetQtd();
            dgvCaminhos.ColumnCount = MaiorNumeroMovimentos() ;

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
            arvoreCidades = new ArvoreCidades(@"C:\Users\gabri\Downloads\CidadesMarte.txt");

            imageClone = (Image)pbMapa.Image.Clone();
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

        private void DesenharCaminho (PilhaLista<Movimento> umCaminho)
        {
            pbMapa.Refresh();
            No<Movimento> umMovimento = umCaminho.Inicio;
            int movimentos = 0;
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


                Pen caneta = new Pen(new SolidBrush(Color.Red));
                caneta.Width = 2;
                Graphics g = pbMapa.CreateGraphics();
                g.DrawString(pontoInicial.NomeCidade.Trim(), new Font("Comic Sans", 10), new SolidBrush(Color.Black), Convert.ToInt32(x + 10), Convert.ToInt32(y));
                g.FillEllipse(new SolidBrush(Color.Black), Convert.ToInt32(x), Convert.ToInt32(y), 6, 8);
                g.DrawLine(caneta, pontoInicial.X / 4, pontoInicial.Y / 4, pontoFinal.X / 4, pontoFinal.Y / 4);
                g.DrawString(umMovimento.Info.Lc.Distancia.ToString(), new Font("Comic Sans", 10), new SolidBrush(Color.Black), Convert.ToInt32((xf + x) / 2), Convert.ToInt32((y + yf)/2));
                g.DrawString(pontoFinal.NomeCidade.Trim(), new Font("Comic Sans", 10), new SolidBrush(Color.Black), Convert.ToInt32(xf + 10), Convert.ToInt32(yf));
                g.FillEllipse(new SolidBrush(Color.Black), Convert.ToInt32(xf), Convert.ToInt32(yf), 6, 8);
                

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
            double proporcao = pbMapa.Size.Height / 2048.0;

            x = x * proporcao;
            y = y * proporcao;
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
