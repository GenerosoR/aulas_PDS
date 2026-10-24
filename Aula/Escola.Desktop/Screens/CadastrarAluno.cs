using Escola.Models;
using Escola.Services;

namespace Escola.Desktop.Screens
{
    public partial class CadastrarAluno : Form
    {
        private readonly AlunoService service;

        public CadastrarAluno(AlunoService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void CadastrarAluno_Load(object sender, EventArgs e)
        {

        }

        private void primaryButton1_Click(object sender, EventArgs e)
        {
            var aluno = new Aluno
            {
                Codigo = txtCodigo.Text,
                Nome = txtNome.Text,
                Email = txtEmail.Text,
                DataNascimento = pickerData.Value.Date.ToUniversalTime()
            };

            var sucesso = service.Criar(aluno);

            if (sucesso)
            {
                lblErro.Visible = false;
                this.Close();
            }
            else
            {
                lblErro.Text = "Erro ao cadastrar aluno. Verifique os dados e tente novamente.";
                lblErro.Visible = true;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblErro_Click(object sender, EventArgs e)
        {

        }
    }
}
