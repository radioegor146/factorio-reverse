namespace FactorioNetParser.FactorioNet
{
    public interface IWritable<out T>
    {
        void Write(System.IO.BinaryWriter writer);
    }
}
