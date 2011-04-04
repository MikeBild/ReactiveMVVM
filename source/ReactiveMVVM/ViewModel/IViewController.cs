namespace ReactiveMVVM.ViewModel
{
    public interface IViewController<out T> where T : IViewModel, new()
    {
        T ViewModel { get; }
    }
}