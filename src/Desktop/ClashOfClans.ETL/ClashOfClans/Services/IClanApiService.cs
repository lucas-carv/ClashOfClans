using ClashOfClans.ETL.ClashOfClans.Model;

namespace ClashOfClans.ETL.ClashOfClans.Services;

public interface IClanApiService
{
    Task<Guerras> ObterGuerras();
}
