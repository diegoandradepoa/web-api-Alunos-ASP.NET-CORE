using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AlunoDAO
    {

        private string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<Aluno> ListarAlunosDB()
        {
            var listaAlunos = new List<Aluno>();

            IDbCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from Alunos";

            IDataReader resultado = selectCmd.ExecuteReader();
            while (resultado.Read())
            {
                var alu = new Aluno();

                alu.id = Convert.ToInt32(resultado["Id"]);
                alu.nome = Convert.ToString(resultado["nome"]);
                alu.sobrenome = Convert.ToString(resultado["sobrenome"]);
                alu.telefone = Convert.ToString(resultado["telefone"]);
                alu.ra = Convert.ToInt32(resultado["ra"]);

                listaAlunos.Add(alu);
            }
            conexao.Close();

            return listaAlunos;
        }
    }
}