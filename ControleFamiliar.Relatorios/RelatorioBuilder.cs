using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;

namespace ControleFamiliar.Relatorios
{
    public class RelatorioBuilder
    {
        protected Document Documento;
        private MemoryStream _ms;

        protected void Inicializa()
        {
            Documento = new Document(PageSize.A4, 10, 10, 42, 35);
            _ms = new MemoryStream();
            PdfWriter.GetInstance(Documento, _ms);

            Documento.Open();
        }

        private void Finaliza()
        {
            Documento.Close();
        }

        private void Processe()
        {
            const string nomeDoRelatorio = "teste.pdf";
            File.WriteAllBytes(nomeDoRelatorio, _ms.ToArray());
            var test = new ProcessStartInfo(nomeDoRelatorio);
            Process.Start(test);
        }

        protected void Construa()
        {
            Finaliza();
            Processe();
        }
    }
}
