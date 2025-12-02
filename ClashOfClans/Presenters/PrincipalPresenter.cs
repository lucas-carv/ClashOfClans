using ClashOfClans.Models;
using ClashOfClans.Services.API;
using ClashOfClans.Views;

namespace ClashOfClans.Presenters;

public class PrincipalPresenter : IPresenter
{
    public string Titulo { get => _view.Text; set => _view.Text = value; }
    public IViewBase View => _view;

    public event EventHandler BuscarMembroClickEvent;
    private readonly IPrincipalView _view;

    public PrincipalPresenter(IPrincipalView view)
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
        List<MembroViewModel> membros = await integrationService.ObterMembros("#2LOUC9R8P");
        if (membros != null)
        {
            _view.PopularGrid(membros);
        }
    }

    public void Show()
    {
        throw new NotImplementedException();
    }
}
