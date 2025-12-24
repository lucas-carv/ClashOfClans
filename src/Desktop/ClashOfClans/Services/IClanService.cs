using ClashOfClans.Models;

namespace ClashOfClans.Services;

public interface IClanService
{
    Task<Clan> BuscarClan(string tag);
    Task<List<MembroViewModel>> BuscarMembros(string tag);
}