﻿using ClashOfClans.API.Model;
using ClashOfClans.API.ViewModels;

namespace ClashOfClans.API.Repositories;

public interface IClanRepository : IRepository<Clan>
{
    Task<Clan> ObterClanPorTag(string tag);
    Task<bool> VerificarSeExisteClan(string tag);
}
