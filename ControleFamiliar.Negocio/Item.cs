namespace ControleFamiliar.Negocio
{
    public class Item : Entidade
    {
        public int QuantidadeEmEstoque { get; set; }
        public int QuantidadeMinimaPorItem { get; set; }
        public decimal Preco { get; set; }
        public string TipoUnidade { get; set; }

        public override bool Equals(object obj)
        {
            return (obj as Item)?.Descricao == Descricao && (obj as Item)?.TipoUnidade == TipoUnidade;
        }

        public override int GetHashCode() => Id.GetHashCode() * 17;

        public string ObtenhaPrecoFormatado()
        {
            return Preco.ToString("###.##").Replace(',', '.');
        }

        public override string ToString()
        {
            return $"DESCRICAO: {Descricao}, PRECO: {Preco}, QUANTIDADE EM ESTOQUE: {QuantidadeMinimaPorItem}, TIPO: {TipoUnidade}";
        }
    }
}
