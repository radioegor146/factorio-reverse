namespace TempAApp.FactorioNet
{
    public interface IReadable<T>
    {
        T Load(System.IO.BinaryReader reader);
    }
}
