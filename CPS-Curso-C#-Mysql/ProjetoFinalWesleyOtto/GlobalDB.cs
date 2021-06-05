using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace ProjetoFinalWesleyOtto
{

    class GlobalDB
    {

        // Responsável pela coneção com o banco de dados
        public static MySqlConnection conexao;

        //Responável pelas instruções à serem executadas
        public static MySqlCommand comando;

        //Responsável por inserir dados em uma DataTable
       // public static MySqlDataAdapter adaptador;

        //Responsavel por ligar o Banco e controles com a propriedade DataSource
        //public static DataTable dataTabela;

        //Variável Global
        public static MySqlDataReader resultado;

        //Método de Conexão do BD e Criação de tabeças
        public static void Conectar()
        {

            //Estabelece os parâmetros para conexão com o BD
            conexao = new MySqlConnection("server=localhost; uid=root; pwd=root");

            //Abre a conexão com o BD
            conexao.Open();

            //Informa a Instrução/Comando SQL
            comando = new MySqlCommand("CREATE DATABASE IF NOT EXISTS cpscurso; use cpscurso;", conexao);

            //Executa a Instrução no Mysql
            comando.ExecuteNonQuery();

            //Informa nova instrução SQL
            comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS cadastro " +
                                        "(id integer auto_increment primary key, " +
                                        "nome varchar(50), " +
                                        "idade int, " +
                                        "cpf varchar(20), " +
                                        "email varchar(40), " +
                                        "login varchar(20), " +
                                        "senha varchar(20)); ", conexao);



            comando.ExecuteNonQuery();

            //Fecha o Banco de Dados
            conexao.Close();

        }

        //Metodo de Insert dos dados
        public static void Cadastrar(string nome, int idade, string cpf, string email, string login, string senha)
        {

            try
            {

                conexao.Open();

                //Comando SQL
                comando = new MySqlCommand("INSERT INTO cadastro (nome, idade, cpf, email, login, senha) VALUES ('"
                                           + nome + "','" + idade + "','" + cpf + "','" + email + "','" + login + "','" + senha + "');", conexao);

                // Executa o Comando SQL
                comando.ExecuteNonQuery();

                //Mensagem que foi executado o cadastro com sucesso
                MessageBox.Show("Cadastro realizado com sucesso!", "Cadastro OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(Exception erro)
            {
                MessageBox.Show("ERRO ==>" + erro.Message, "Falha no Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Fecha o Banco de Dados
                conexao.Close();
            }

        }

        //Método que seleciona o Último ID Cadastrado
        public static string SelectUltimoId()
        {
            string id = "0";
            
            try
            {
                conexao.Open();

                comando = new MySqlCommand("SELECT MAX(id) AS maior FROM cadastro;", conexao);

                resultado = comando.ExecuteReader();

                if (resultado.HasRows)
                {
                    resultado.Read();
                    id = resultado["maior"].ToString();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha na recuperação do último ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!resultado.IsClosed)
                    resultado.Close();

                conexao.Close();
            }
            return id;
        }

        //Método que lista os Usuários Cadastrados
        public static string Listar()
        {
            string listagem = "Lista vazia!";
            try
            {
                conexao.Open();

                comando = new MySqlCommand("SELECT * FROM cadastro ORDER BY id;", conexao);

                MySqlDataReader resultado = comando.ExecuteReader();

                Cadastro c = new Cadastro();

                if (resultado.HasRows)
                {
                    listagem = "Listagem de Cadastros\n\n";
                    while (resultado.Read())
                    {
                        // Faço INT ==> String
                        c.Id = Convert.ToInt32(resultado["id"].ToString());
                        c.Nome = resultado["nome"].ToString();
                        c.Idade = Convert.ToInt32(resultado["idade"].ToString());
                        c.Cpf = resultado["cpf"].ToString();
                        c.Email = resultado["email"].ToString();
                        c.Login = resultado["login"].ToString();
                        c.Senha = resultado["senha"].ToString();
                        listagem += c.ToString() + '\n';
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha na listagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!resultado.IsClosed)
                    resultado.Close();

                conexao.Close();
            }

            return listagem;

        }

        //Método para Alterar os dados dos Usuários
        public static void Alterar(Cadastro c)
        {
            
            try
            {
                conexao.Open();

                comando = new MySqlCommand("SELECT * FROM cadastro WHERE id=" + c.Id + ";", conexao);

                resultado = comando.ExecuteReader();
                               

                if (resultado.HasRows)
                {
                    resultado.Read();
                    string instrucao = "UPDATE cadastro set ";
                    instrucao += "nome = '" + c.Nome + "',";
                    instrucao += "idade = '" + c.Idade + "',";
                    instrucao += "cpf = '" + c.Cpf + "',";
                    instrucao += "email = '" + c.Email + "',";
                    instrucao += "login = '" + c.Login + "',";
                    instrucao += "senha = '" + c.Senha + "' WHERE id=" + c.Id + ";";

                    if (!resultado.IsClosed)
                        resultado.Close();

                    comando = new MySqlCommand(instrucao, conexao);

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Alteração realizada com sucesso!", "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Registro não encontrado", "Falha na Alteração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha na alteração", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!resultado.IsClosed)
                    resultado.Close();

                conexao.Close();
            }
        }

        //Método de exclusão dos dados
        public static void Excluir(string id)
        {
            try
            {
                conexao.Open();

                comando = new MySqlCommand("SELECT * FROM cadastro WHERE id=" + id + ";", conexao);

                MySqlDataReader resultado = comando.ExecuteReader();

                if (resultado.HasRows)
                {
                    resultado.Read();
                    string instrucao = "DELETE FROM cadastro WHERE id=" + id + ";";

                    if (!resultado.IsClosed)
                        resultado.Close();

                    comando = new MySqlCommand(instrucao, conexao);

                    comando.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Registro não encontrado! Tente novamente!", "Falha na exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha na exclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!resultado.IsClosed)
                    resultado.Close();

                conexao.Close();
            }
        }

        //Metodo Retorna objeto Cadastro
        public static Cadastro RetonarCadastro(string id)
        {
            Cadastro c = new Cadastro();
            //Instancio meu Objeto como Null
            //c = null;

            try
            {
                conexao.Open();

                comando = new MySqlCommand("SELECT * FROM cadastro WHERE id=" + id + ";", conexao);

                MySqlDataReader resultado = comando.ExecuteReader();

                if (resultado.HasRows)
                {

                    resultado.Read();
                    //c.Id = Convert.ToInt32(resultado["id"].ToString());
                    c.Nome = resultado["nome"].ToString();
                    c.Idade = Convert.ToInt32(resultado["idade"].ToString());
                    c.Cpf = resultado["cpf"].ToString();
                    c.Email = resultado["email"].ToString();
                    c.Login = resultado["login"].ToString();
                    c.Senha = resultado["senha"].ToString();
                    
                }

                return c;
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha no retorno Retorno Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return c;
            }
            finally
            {
                if (!resultado.IsClosed)
                    resultado.Close();

                conexao.Close();
            }

        }
    }


}
