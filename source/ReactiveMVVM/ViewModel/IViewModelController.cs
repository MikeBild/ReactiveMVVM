namespace ReactiveMVVM.ViewModel
{
    public interface IViewModelController<out T> where T : IViewModel, new()
    {
        T ViewModel { get; }
    }
}