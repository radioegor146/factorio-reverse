using System;
using System.Collections.Generic;
using System.Linq;

namespace FactorioNetParser.FactorioNet
{
    internal class NetworkMessageBundle
    {
        private readonly SortedList<ushort, NetworkMessage> bundleMessages =
            new SortedList<ushort, NetworkMessage>();

        private int needCount = -1;

        public bool HandleBundleMessage(NetworkMessage message)
        {
            if (needCount == bundleMessages.Count)
                return true;
            if (message.IsLastFragment)
                needCount = message.FragmentId + 1;
            if (!bundleMessages.ContainsKey(message.FragmentId))
                bundleMessages.Add(message.FragmentId, message);
            return needCount == bundleMessages.Count;
        }

        public NetworkMessage GetOverallMessage()
        {
            if (needCount != bundleMessages.Count)
                return null;
            var overallsize = bundleMessages.Values.Sum(netmsg => netmsg.MessageDataBytes.Length);
            var bytes = new byte[overallsize];
            var arrayptr = 0;
            foreach (var netmsg in bundleMessages.Values)
            {
                Array.Copy(netmsg.MessageDataBytes, 0, bytes, arrayptr, netmsg.MessageDataBytes.Length);
                arrayptr += netmsg.MessageDataBytes.Length;
            }

            return new NetworkMessage(bundleMessages.First().Value.Type, bytes);
        }
    }
}