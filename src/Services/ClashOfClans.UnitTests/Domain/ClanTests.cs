using ClashOfClans.API.BackgroundServices;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model;
using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Quartz;

namespace ClashOfClans.UnitTests.Domain;

public class ClanTests
{
    //[Fact]
    //public void Deve_criar_um_clan_corretamente()
    //{
    //    // Arrange
    //    string tag = "#ABC123";
    //    string nome = "Meu Clan";

    //    // Act
    //    var clan = new Clan(tag, nome);

    //    // Assert
    //    Assert.Equal(tag, clan.Tag);
    //    Assert.Equal(nome, clan.Nome);
    //}

    //[Theory]
    //[InlineData("")]
    //[InlineData("   ")]
    //[InlineData(null)]
    //public void Deve_falhar_quando_tag_for_vazia(string tagInvalida)
    //{
    //    Assert.Throws<ArgumentException>(() => new Clan(tagInvalida!, "Nome"));
    //}

    //[Fact]
    //public void Deve_falhar_quando_tag_nao_comeca_com_cardinal()
    //{
    //    Assert.Throws<ArgumentException>(() => new Clan("ABC123", "Nome"));
    //}
}

public class AnaliseAtaquesTests
{
    private ClashOfClansContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<ClashOfClansContext>()
            .UseInMemoryDatabase(databaseName: "ClashTestDB")
            .Options;

        return new ClashOfClansContext(options);
    }

    [Fact]
    public async Task Job_Deve_salvar_resumo_de_ataques_corretamente()
    {  // Arrange
        var context = CriarContexto();
        var logger = Mock.Of<ILogger<AnalisarGuerrasJob>>();
        var job = new AnalisarGuerrasJob(context, logger);

        // Guerra 1
        ClanEmGuerra clanEmGuerra = new("#CLAN");
        List<MembroEmGuerra> membros =
            [new MembroEmGuerra("#A", "Alice") {
                Ataques =
                [
                    new GuerraMembroAtaque("#A", "#defensor1"){Estrelas = 1},
                    new GuerraMembroAtaque("#A", "#defensor2"){Estrelas = 3}
                ]},
            new MembroEmGuerra("#B", "Bruno"){
                Ataques =
                [
                    new GuerraMembroAtaque("#B", "#defensor1"){Estrelas = 3},
                    new GuerraMembroAtaque("#B", "#defensor2"){Estrelas = 3}
                ]}
            ];
        foreach (var membro in membros)
        {
            var membroCriado = clanEmGuerra.AdicionarMembro(membro.Tag, membro.Nome);
            foreach (var ataque in membro.Ataques)
            {
                membro.AtualizarAtaque(ataque.AtacanteTag, ataque.DefensorTag, ataque.Estrelas);
            }
        }
        var g1 = new Guerra("WarEnded", DateTime.Now, DateTime.Now, clanEmGuerra);

        // Guerra 2
        var g2 = new Guerra("WarEnded", DateTime.Now, DateTime.Now, clanEmGuerra);

        context.Guerras.AddRange(g1, g2);
        await context.SaveChangesAsync();

        var quartzContext = Mock.Of<IJobExecutionContext>();

        // Act
        await job.Execute(quartzContext);

        // Assert
        var resumos = context.MembrosGuerrasResumo.ToList();

        Assert.Equal(2, resumos.Count);

        var alice = resumos.First(x => x.Tag == "#A");
        Assert.Equal(0, alice.QuantidadeAtaques);
        Assert.Equal(2, alice.GuerrasParticipadasSeq);

        var bruno = resumos.First(x => x.Tag == "#B");
        Assert.Equal(2, bruno.QuantidadeAtaques);
        Assert.Equal(2, bruno.GuerrasParticipadasSeq);
    }
}