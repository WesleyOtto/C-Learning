using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjetoFinalWesleyOtto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GlobalDB.Conectar();
            //Seta o próximo ID
            textBoxId.Text = ProximoID();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //Método Recupera o último id do banco de dados
        private string ProximoID()
        {
            try
            {
                int proximoId = Convert.ToInt32(GlobalDB.SelectUltimoId()) + 1;
                return proximoId.ToString();
            }catch(FormatException erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha na conversao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return "";
        }

        //Método Limpa os campos no formulário
        private void LimparCampos()
        {
            textBoxId.Text = ProximoID();
            textBoxIdade.Text = "";
            textBoxCPF.Text = "";
            textBoxEmail.Text = "";
            textBoxLogin.Text = "";
            textBoxSenha.Text = "";
            textBoxNome.Text = "";
            //Define o foco no campo Nome do Formulário
            textBoxNome.Focus();

        }

        private void buttonLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void buttonIncluir_Click(object sender, EventArgs e)
        {
            GlobalDB.Cadastrar(textBoxNome.Text, Convert.ToInt32(textBoxIdade.Text), textBoxCPF.Text, textBoxEmail.Text, textBoxLogin.Text, textBoxSenha.Text);
            LimparCampos();
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            Cadastro c = new Cadastro(textBoxNome.Text, Convert.ToInt32(textBoxIdade.Text), textBoxCPF.Text, textBoxEmail.Text, textBoxLogin.Text, textBoxSenha.Text, Convert.ToInt32(textBoxId.Text));
            if (MessageBox.Show("Deseja Alterar este registro?", "Alterado!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) ;
                 GlobalDB.Alterar(c);
            LimparCampos();
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que quer excluir este registro?", "Excluir!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) ;
                GlobalDB.Excluir(textBoxId.Text);

            LimparCampos();
        }

        private void buttonConsultar_Click(object sender, EventArgs e)
        {
            string lista = GlobalDB.Listar();
            MessageBox.Show(lista);
        }

        private void buttonConsultaID_Click(object sender, EventArgs e)
        {

            Cadastro c = new Cadastro();
            c = GlobalDB.RetonarCadastro(textBoxId.Text);
            textBoxNome.Text = c.Nome;
            textBoxIdade.Text = c.Idade.ToString();
            textBoxCPF.Text = c.Cpf;
            textBoxEmail.Text = c.Email;
            textBoxLogin.Text = c.Login;
            textBoxSenha.Text = c.Senha;

        }
    }
}
