using ClashOfClans.Views;

namespace ClashOfClans.Presenters;

public interface IPresenter
{
    string Titulo { get; set; }
    IViewBase View { get; }
}
