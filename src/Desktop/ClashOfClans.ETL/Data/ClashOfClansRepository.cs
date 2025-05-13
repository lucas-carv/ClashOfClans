using Dapper;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace ClashOfClans.ETL.Data;

public class ClashOfClansRepository : IClashOfClansRepository
{
    private readonly SincronizadorConfiguration _configuration;
    private readonly string _connectionString;
    public ClashOfClansRepository(SincronizadorConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.DefaultConnection;

        if (string.IsNullOrEmpty(_connectionString))
            throw new ApplicationException("Os dados de configuração do banco de dados não foram identificados!");
    }

    public async Task<IEnumerable<Historico>> ObterHistorico()
    {
        using var connection = new MySqlConnection(_connectionString);

        string query = $@"SELECT * FROM historico_integracao";
        var result = await connection.QueryAsync<Historico>(query);
        return result;
    }
}

public interface IClashOfClansRepository
{
}

public class SincronizadorConfiguration
{
    public string DefaultConnection { get; set; } = string.Empty;
}

public class Historico
{
    public string Recurso { get; set; } = string.Empty;
    public string ReferenciaId { get; set; } = string.Empty;
    public DateTime DataUltimaAlteracao { get; set; }
}
public abstract class BaseDAL
{
    protected static string Conexao => "Host=localhost;Port=3307;Database=clash_of_clans;Username=root;Password=GH#@Mn47spW!HH$yvv76;";
}
