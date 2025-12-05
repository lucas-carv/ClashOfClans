using Autofac;
using ClashOfClans.Presenters;
using ClashOfClans.Views;

namespace ClashOfClans
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var builder = new ContainerBuilder();
            builder.RegisterType<ResumoDuasUltimasGuerrasView>().As<IResumoDuasUltimasGuerrasView>().AsSelf();
            builder.RegisterType<ResumoDuasUltimasGuerrasPresenter>().AsSelf();

            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                //  Resolve o presenter (isso já cria a view e assina o evento)
                var presenter = scope.Resolve<ResumoDuasUltimasGuerrasPresenter>();

                // Recupera a view para rodar o form
                var view = (Form)presenter.View; // já que View => _view, que é um Form

                Application.Run(view);
            }
        }
    }
}