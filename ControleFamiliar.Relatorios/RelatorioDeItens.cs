using ControleFamiliar.Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ControleFamiliar.Relatorios
{
    public class RelatorioDeItens : RelatorioBuilder
    {
        public void EmitaRelatorio(IList<Item> listaDeItens)
        {
            Inicializa();

            var tabela = new PdfPTable(new float[] { 50, 50, 50 });
            tabela.DefaultCell.HasBorder(1);
            var celulaTitulo = new PdfPCell { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_CENTER };
            celulaTitulo.AddElement(new Paragraph("LISTA DE COMPRAS"));
            celulaTitulo.Colspan = 3;
            celulaTitulo.BackgroundColor = new BaseColor(Color.LightGray);
            tabela.AddCell(celulaTitulo);

            tabela.AddCell(new Phrase("DESCRIÇÃO"));
            tabela.AddCell(new Phrase("QUANTIDADE"));
            tabela.AddCell(new Phrase("PREÇO"));

            foreach (var item in listaDeItens)
            {
                tabela.AddCell(item.Descricao);
                tabela.AddCell(item.QuantidadeMinimaPorItem.ToString());
                tabela.AddCell($"R$ {item.ObtenhaPrecoFormatado()}");
            }

            var total = listaDeItens.Sum(c => c.Preco * c.QuantidadeMinimaPorItem);
            tabela.AddCell("TOTAL");
            tabela.DefaultCell.Colspan = 2;
            tabela.DefaultCell.VerticalAlignment = Element.ALIGN_LEFT;
            tabela.AddCell(total.ToString("R$###,##"));

            Documento.Add(tabela);

            Construa();
        }
    }
}