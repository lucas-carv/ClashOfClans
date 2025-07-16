using ClashOfClans.Api;
using ClashOfClans.Models;
using ClashOfClans.Services;

namespace ClashOfClans
{
    public partial class Principal : Form
    {
        private readonly ClanService _clanService;
        public Principal()
        {
            _clanService = new();
            InitializeComponent();
        }

        private void BuscarClanButton_Click(object sender, EventArgs e)
        {
            Task TaskBuscarClan = BuscarClan();
            Task.WhenAll(TaskBuscarClan);
        }

        private async Task BuscarClan()
        {
            Clan clan = await _clanService.BuscarClan("#2LOUC9R8P");
            ClanViewModel clanViewModel = new()
            {
                MemberList = clan.MemberList,
                Name = clan.Name,
                Tag = clan.Tag
            };
            ClashOfClansApiService clashOfClansApiService = new();
            await clashOfClansApiService.EnviarClan(clanViewModel);
        }

        private void ListarMembrosButton_Click(object sender, EventArgs e)
        {
            Task TaskObterMembros = ObterMembros();
            Task.WhenAll(TaskObterMembros);
        }
        private async Task ObterMembros()
        {
            Membros membros = await _clanService.BuscarMembros("#2LOUC9R8P");
            MembrosDataGridView.DataSource = membros.items;
        }
    }
}
