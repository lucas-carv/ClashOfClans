using ClashOfClans.Models;
using ClashOfClans.Views;

namespace ClashOfClans;

public partial class ResumoDuasUltimasGuerrasView : Form, IResumoDuasUltimasGuerrasView
{
    public event EventHandler? BuscarMembroClickEvent;

    public ResumoDuasUltimasGuerrasView()
    {
        InitializeComponent();

        BuscarClanButton.Click += (s, e) => { BuscarMembroClickEvent?.Invoke(s, e); };
    }

    public void PopularGrid(List<MembroViewModel> membros)
    {
        MembrosDataGridView.DataSource = membros;
    }
}
