using System;

namespace ControleFamiliar.Negocio
{
    public class Item : ObjetoComId
    {
        public int QuantidadeEmEstoque { get; set; }
        public int QuantidadeMinimaPorItem { get; set; }
        public decimal Preco { get; set; }
        public string TipoUnidade { get; set; }

        public override bool Equals(object obj)
        {
            var itemComparacao = obj as Item;

            return itemComparacao?.Descricao == Descricao && itemComparacao?.TipoUnidade == TipoUnidade;
        }

        public string ObtenhaPrecoFormatado() => Preco.ToString("###.##").Replace(',', '.');
    }
}
