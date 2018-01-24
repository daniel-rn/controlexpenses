using ControleFamiliar.Mapeadores;
using ControleFamiliar.Negocio;

namespace ControleFamiliar.Processos
{
    public class ProcessoItens
    {
        public bool CadastrarItem(DtoCadastroDeItens dtoCadastroDeItens)
        {
            var itemParaComparacao = new MapeadorDeItens().ObtenhaItemPorId(dtoCadastroDeItens.Item);

            return !itemParaComparacao.Equals(dtoCadastroDeItens.Item) && new MapeadorDeItens().SalvarItem(dtoCadastroDeItens.Item);
        }

        public DtoCadastroDeItens ObtenhaItensCadastrados()
        {
            return new DtoCadastroDeItens {Items = new MapeadorDeItens().ObtenhaItensCadastrados()};
        }

        public Item ObtenhaItemPorId(Item item)
        {
            return new MapeadorDeItens().ObtenhaItemPorId(item);
        }
    }
}
