using ClashOfClans.Models;

public interface IClanService
{
    Task<Clan> BuscarClan(string tag);
    Task<Membros> BuscarMembros(string tag);
}