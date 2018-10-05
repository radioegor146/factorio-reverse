using System.IO;

namespace FactorioNetParser.FactorioNet
{
    public interface IReadable<out T>
    {
        T Load(BinaryReader reader);
    }
}