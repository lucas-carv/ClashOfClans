using ClashOfClans.Models;
using ClashOfClans.Views;

namespace ClashOfClans;

public interface IPrincipalView : IViewBase
{
    event EventHandler BuscarMembroClickEvent;
    void PopularGrid(List<MembroViewModel> membros);
}
