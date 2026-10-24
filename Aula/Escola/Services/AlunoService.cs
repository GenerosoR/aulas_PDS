using Escola.Contexts;
using Escola.Models;
using Npgsql;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace Escola.Services
{
    public class AlunoService
    {
        private EscolaDbContext context;
        public AlunoService(EscolaDbContext context)
        {
            this.context = context;
        }

        public AlunoService() : this(new EscolaDbContext(ConnectionString))
        {

        }

        private const string ConnectionString = 
            "Server=172.16.103.22;" +
            "Port=5432;" +
            "Database=escola_brainrot;" +
            "User Id=postgres;" +
            "Password=postgres;"; 
        private List<Aluno> lista = new List<Aluno>
        {
            new Aluno { Nome = "João da Silva", Codigo = "0001", DataNascimento = new DateTime(2005, 5, 15), Email = "joao@email.com" },
            new Aluno { Nome = "Maria Oliveira", Codigo = "0002", DataNascimento = new DateTime(2006, 8, 22), Email = null },
            new Aluno { Nome = "Pedro Santos", Codigo = "0003", DataNascimento = new DateTime(2005, 12, 5), Email = "pedro@gmail.com" },
        };

        public List<Aluno> Consultar()
        {
            //var conexao = new NpgsqlConnection(ConnectionString);
            //conexao.Open();

            //var sql = """
            //    SELECT codigo, nome, email, datanascimento
            //    FROM alunos
            //""";

            //var comando = new NpgsqlCommand(sql, conexao);
            //var leitor = comando.ExecuteReader();
            //var resultado = new List<Aluno>();

            //while (leitor.Read())
            //{
            //    var aluno = new Aluno()
            //    {
            //        Codigo = leitor.GetString(0),
            //        Nome = leitor.GetString(1),
            //        Email = leitor.GetString(2),
            //        DataNascimento = leitor.GetDateTime(3)
            //    };
            //    resultado.Add(aluno);
            //}

            var resultado = context.Alunos.OrderBy(a => a.Nome).ToList();
            return resultado;
        }

        public bool Criar(Aluno aluno)
        {
            if (!Validar(aluno))
            {
                return false;
            }
            //var conexao = new NpgsqlConnection(ConnectionString);
            //conexao.Open();

            //var sql = $"""
            //    INSERT INTO alunos(codigo, nome, email, datanascimento)
            //    VALUES (@codigo, @p1, @p2, @p3)
            //""";

            //var comando = new NpgsqlCommand(sql, conexao);
            //comando.Parameters.Add("codigo", NpgsqlDbType.Text).Value = aluno.Codigo;
            //comando.Parameters.Add("p1", NpgsqlDbType.Text).Value = aluno.Nome;
            //comando.Parameters.Add("p2", NpgsqlDbType.Text).Value = aluno.Email;
            //comando.Parameters.Add("p3", NpgsqlDbType.Timestamp).Value = aluno.DataNascimento;

            //var linhas = comando.ExecuteNonQuery();
            //return linhas == 1;

            context.Add(aluno); //insert
            context.SaveChanges(); //commit
            return true;
        }

        public bool Validar(Aluno aluno)
        {
            var context = new ValidationContext(aluno);
            var erros = new List<ValidationResult>();
            return Validator.TryValidateObject(aluno, context, erros);
        }

        public bool Excluir(string codigo)
        {
            var aluno = FindByCodigo(codigo);
            if (aluno == null)
            {
                return false;
            }
            return lista.Remove(aluno);
        }

        private Aluno? FindByCodigo(string codigo)
        {
            return lista.FirstOrDefault(a => a.Codigo == codigo);
        }
    }
}
