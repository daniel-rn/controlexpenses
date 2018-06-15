using ControleFamiliar.Negocio;
using ControleFamiliar.Processos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            dgvItens.Columns.Add(DgvHelper.AdicionaColuna("Descrição!!!", nameof(Item.Descricao), 200));
            dgvItens.Columns.Add(DgvHelper.AdicionaColuna("Preco", nameof(Item.Preco), 200));
            dgvItens.Columns.Add(DgvHelper.AdicionaColuna("Quantidade em Estoque", nameof(Item.QuantidadeMinimaPorItem), 200));
            dgvItens.Columns.Add(DgvHelper.AdicionaColuna("Unidade", nameof(Item.TipoUnidade), 200));
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

            MessageBox.Show("Item salvo com sucesso", "Salvo com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            FormCadastroItem_Load(sender, e);
        }

        private DtoCadastroDeItens ObtenhaParametros()
        {
            decimal.TryParse(txtPreco.Text, out var preco);

            var itens = new DtoCadastroDeItens
            {
                Item = new Item
                {
                    Descricao = txtDescricao.Text,
                    Preco = preco,
                    QuantidadeMinimaPorItem = Convert.ToInt32(txtQuantidadeMinima.Text),
                    TipoUnidade = cbTipoDeUnidade.SelectedItem.ToString()
                }
            };

            return itens;
        }

        private void FormCadastroItem_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                var lista = new ProcessoItens().ObtenhaItensCadastrados().Items;
                _bs.DataSource = lista;
            }
        }

        private void txtPreco_Validated(object sender, EventArgs e)
        {
            if (txtPreco.Text == "" || (txtPreco.Text == @" ")) return;

            var valor = Convert.ToDecimal(txtPreco.Text);
            txtPreco.Text = $@"{valor:N}";
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            var lista = (List<Item>)_bs.DataSource;
            new ProcessoItens().EmitaRelatorio(lista);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var itemParaExcluir = (Item)_bs.Current;
            if (new ProcessoItens().ExcluirItem(itemParaExcluir))
            {
                MessageBox.Show("Item excluído com Sucesso !!");
                Application.Restart();
            }

        }
    }
}
