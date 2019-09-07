namespace FactorioNetParser.FactorioNet
{
    public interface IMessage<out T> : IWritable<T>, IReadable<T>
    {
        MessageType GetMessageType();
    }
}