using System.Windows.Forms;

namespace ControleFamiliar
{
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
        }
        public void AjustaNomeDaFuncao(string nomeDaFuncao)
        {
            lbNomeDaFuncao.Text = nomeDaFuncao;
        }
    }
}
