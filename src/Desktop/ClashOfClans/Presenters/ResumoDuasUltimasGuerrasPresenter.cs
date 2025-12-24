using ClashOfClans.Models;
using ClashOfClans.Services.API;
using ClashOfClans.Views;

namespace ClashOfClans.Presenters;

public class ResumoDuasUltimasGuerrasPresenter : IPresenter
{
    public string Titulo { get => _view.Text; set => _view.Text = value; }
    public IViewBase View => _view;

    public event EventHandler? BuscarMembroClickEvent;
    private readonly IResumoDuasUltimasGuerrasView _view;

    public ResumoDuasUltimasGuerrasPresenter(IResumoDuasUltimasGuerrasView view)
    {
        _view = view;
        _view.BuscarMembroClickEvent += AtualizarClickEvent;
    }

    private void AtualizarClickEvent(object? sender, System.EventArgs e)
    {
        CarregarMembrosDaApi();
    }

    private async void CarregarMembrosDaApi()
    {
        IntegrationService integrationService = new();
        List<MembroViewModel> membros = await integrationService.ObterMembros("#2L0UC9R8P");
        if (membros != null)
        {
            _view.PopularGrid(membros);
        }
    }

    public void Show()
    {
    }
}
