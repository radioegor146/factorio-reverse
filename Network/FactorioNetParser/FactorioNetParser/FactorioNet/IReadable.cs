namespace FactorioNetParser.FactorioNet
{
    public interface IReadable<T>
    {
        T Load(System.IO.BinaryReader reader);
    }
}
