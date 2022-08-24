using AgendaContatos.Data.Configurations;
using AgendaContatos.Data.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Repositories
{
    public class ContatoRepository
    {
        public void Create(Contato contato)
        {
            var sql = $@"
                            INSERT INTO CONTATO
                            (
                                IDCONTATO,
                                NOME,
                                TELEFONE,
                                DATANASCIMENTO,
                                EMAIL,
                                IDUSUARIO
                            )
                            VALUES
                            (
                                @IdContato, 
                                @Nome, 
                                @Telefone, 
                                @DataNascimento, 
                                @Email, 
                                @IdUsuario 
                            )
                        ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                connection
                    .Execute(sql, contato);
            }
        }

        public void Update(Contato contato)
        {
            var sql = $@"
                            UPDATE CONTATO
                            SET
                                NOME = @Nome,
                                TELEFONE = @Telefone,
                                DATANASCIMENTO = @DataNascimento,
                                EMAIL = @Email
                            WHERE
                                IDCONTATO = @IdContato
                       ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                connection
                    .Execute(sql, contato);
            }
        }

        public void Delete(Contato contato)
        {
            var sql = $@"
                            DELETE  
                            FROM CONTATO
                            WHERE
                                IDCONTATO = @IdContato
                       ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                connection
                    .Execute(sql, contato);
            }
        }

        public List<Contato> GetByUsuario(Guid idUsuario)
        {
            var sql = $@"
                            SELECT * 
                            FROM CONTATO
                            WHERE IDUSUARIO = @idusuario
                            ORDER BY NOME
                       ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                return connection
                    .Query<Contato>(sql, new { idUsuario })
                    .ToList();
            }
        }

        public Contato GetById(Guid idContato, Guid idUsuario)
        {
            var sql = $@"
                            SELECT * FROM CONTATO
                            WHERE IDCONTATO = @idContato
                            AND IDUSUARIO = @idUsuario
                        ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                return connection
                    .Query<Contato>(sql, new { idContato, idUsuario })
                    .FirstOrDefault();
            }
        }
    }
}
