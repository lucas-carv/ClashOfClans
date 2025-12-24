using ClashOfClans.Models;

namespace ClashOfClans.Views;

public interface IResumoDuasUltimasGuerrasView : IViewBase
{
    event EventHandler BuscarMembroClickEvent;
    void PopularGrid(List<MembroViewModel> membros);
}
