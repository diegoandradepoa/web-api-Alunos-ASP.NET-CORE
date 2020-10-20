using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class Aluno
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public string data { get; set; }
        public int ra { get; set; }

        public List<Aluno> ListarAluno()
        {

            try
            {
                var alunoBD = new AlunoDAO();
                return alunoBD.ListarAlunosDB();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao listar Alunos: Erro => {ex.Message}");
            }
        }

        public bool RescreverArquivo(List<Aluno> listaAlunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Start\Base.Json");

            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public Aluno Inserir(Aluno Aluno)
        {
            var listaAlunos = this.ListarAluno();

            var maxId = listaAlunos.Max(aluno => aluno.id);
            Aluno.id = maxId + 1;
            listaAlunos.Add(Aluno);

            RescreverArquivo(listaAlunos);
            return Aluno;
        }

        public Aluno Atualizar(int id, Aluno Aluno)
        {
            var listaAluno = this.ListarAluno();

            var itemIndex = listaAluno.FindIndex(p => p.id == id);
            if (itemIndex >= 0)
            {
                Aluno.id = id;
                listaAluno[itemIndex] = Aluno;
            }
            else
            {

                return null;
            }

            RescreverArquivo(listaAluno);
            return Aluno;
        }

        public bool Deletar(int id)
        {
            var listaAlunos = this.ListarAluno();

            var itemIndex = listaAlunos.FindIndex(p => p.id == id);
            if (itemIndex >= 0)
            {
                listaAlunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            RescreverArquivo(listaAlunos);
            return true;
        }

    }
}