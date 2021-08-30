namespace Anshan.Core
{
    public interface IServiceLocator
    {
        T GetInstance<T>();
    }
}