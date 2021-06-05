using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoFinalWesleyOtto
{
    class Cadastro
    {   
        //Atributos
        private string nome;
        private int idade;
        private string cpf;
        private string email;
        private string login;
        private string senha;
        private int id;


        //Construtor sem parâmetros
        public Cadastro() { }

        //Construtor com parâmetros 
        public Cadastro(string nome, int idade, string cpf, string email, string login, string senha, int id ) {

            this.Nome = nome;
            this.Idade = idade;
            this.Cpf = cpf;
            this.Email = email;
            this.Login = login;
            this.Senha = senha;
            this.Id = id;
        }

        //Get ans Setters Atalho: Ctrl+r Ctrl+e
        public string Nome { get => nome; set => nome = value; }
        public int Idade { get => idade; set => idade = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Email { get => email; set => email = value; }
        public string Login { get => login; set => login = value; }
        public string Senha { get => senha; set => senha = value; }
        public int Id { get => id; set => id = value; }


        //Métodos

        public override string ToString()
        {
            return (String.Format("-nome: {0} \n -idade: {1} \n -cpf: {2} \n -email: {3} \n -login: {4} \n -senha: {5} \n -id: {6} \n \n ****************************************************************** \n ", nome, idade, cpf, email, login, senha, id));
        }
    }

}
