using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class AddressToUsernameMapping : IReadable<AddressToUsernameMapping>
    {
        public string Address;
        public string Username;

        public AddressToUsernameMapping(BinaryReader reader)
        {
            Load(reader);
        }

        public AddressToUsernameMapping() { }

        public AddressToUsernameMapping Load(BinaryReader reader)
        {
            Address = reader.ReadFactorioString();
            Username = reader.ReadFactorioString();
            return this;
        }
    }
}