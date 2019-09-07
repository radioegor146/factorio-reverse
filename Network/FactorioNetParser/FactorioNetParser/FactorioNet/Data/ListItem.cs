using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class ListItem : IReadable<ListItem>
    {
        public string Address;
        public string Reason;
        public string Username;

        public ListItem(BinaryReader reader)
        {
            Load(reader);
        }

        public ListItem() { }

        public ListItem Load(BinaryReader reader)
        {
            Username = reader.ReadSimpleString();
            Reason = reader.ReadSimpleString();
            Address = reader.ReadSimpleString();
            return this;
        }
    }
}