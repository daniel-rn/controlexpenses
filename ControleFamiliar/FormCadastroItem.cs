using ControleFamiliar.Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ControleFamiliar.Processos;

namespace ControleFamiliar
{
    public partial class FormCadastroItem : FormBase
    {
        private readonly BindingSource _bs = new BindingSource { DataSource = new List<Item>() };

        public FormCadastroItem()
        {
            InitializeComponent();
            AjustaElementosDaTela();
            AjustaGrid();
        }

        private void AjustaGrid()
        {
            dgvItens.AutoGenerateColumns = false;
            dgvItens.DataSource = _bs;

            dgvItens.Columns.Add(DgvHelper.AdicionaColuna("Descrição!!!", nameof(Item.Descricao), 300));
            dgvItens.Columns.Add(DgvHelper.AdicionaColuna("Preco", nameof(Item.Preco), 300));
            dgvItens.Columns.Add(DgvHelper.AdicionaColuna("Quantidade em Estoque", nameof(Item.QuantidadeEmEstoque), 300));
        }

        private void AjustaElementosDaTela()
        {
            AjustaNomeDaFuncao("Cadastro de Itens !");
            cbTipoDeUnidade.Items.Add(EnumeradorDeUnidades.Kilo);
            cbTipoDeUnidade.Items.Add(EnumeradorDeUnidades.Grama);
            cbTipoDeUnidade.Items.Add(EnumeradorDeUnidades.Unidade);
        }

        private void BtnCadastrar_Click(object sender, EventArgs e)
        {
            if (!new ProcessoItens().CadastrarItem(ObtenhaParametros())) return;

            MessageBox.Show(@"Item salvo com sucesso", @"Salvo com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            FormCadastroItem_Load(sender, e);
        }

        private DtoCadastroDeItens ObtenhaParametros()
        {
            decimal.TryParse(txtPreco.Text, out var preco);

            return new DtoCadastroDeItens
            {
                Item = new Item
                {
                    Descricao = txtDescricao.Text,
                    Preco = preco,
                    QuantidadeMinimaPorItem = Convert.ToInt32(txtQuantidadeMinima.Text),
                    TipoUnidade = cbTipoDeUnidade.SelectedItem.ToString()
                }
            };
        }

        private void FormCadastroItem_Load(object sender, EventArgs e)
        {
            var lista = new ProcessoItens().ObtenhaItensCadastrados().Items;
            _bs.DataSource = lista;
        }

        private void txtPreco_Validated(object sender, EventArgs e)
        {
            if (txtPreco.Text == "" || (txtPreco.Text == @" ")) return;

            var valor = Convert.ToDecimal(txtPreco.Text);
            txtPreco.Text = $@"{valor:N}";
        }
    }
}
