using ClashOfClans.Models;
using ClashOfClans.Presenters;

namespace ClashOfClans;

public partial class Principal : Form, IPrincipalView
{
    public event EventHandler? BuscarMembroClickEvent;

    public Principal()
    {
        InitializeComponent();

        BuscarClanButton.Click += (s, e) => { BuscarMembroClickEvent?.Invoke(s, e); };
    }

    public void PopularGrid(List<MembroViewModel> membros)
    {
        MembrosDataGridView.DataSource = membros;
    }
}
