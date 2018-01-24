using System.Windows.Forms;

namespace ControleFamiliar
{
    public static class DgvHelper
    {
        public static DataGridViewColumn AdicionaColuna(string headerText, string dataPropertyName, int width)
        {
            return new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = width,
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                Name = dataPropertyName
            };
        }
    }
}
