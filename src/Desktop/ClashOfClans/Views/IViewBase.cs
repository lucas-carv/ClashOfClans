namespace ClashOfClans.Views;

public interface IViewBase
{
    string Text { get; set; }
    string Name { get; set; }
    void Show();
}
