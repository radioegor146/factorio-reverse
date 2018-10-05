using System;
using System.Collections.Generic;
using System.Linq;

namespace FactorioNetParser.FactorioNet
{
    internal class FactorioNetMessageBundle
    {
        private readonly SortedList<short, FactorioNetMessage> bundleMessages =
            new SortedList<short, FactorioNetMessage>();

        private int needCount = -1;

        public bool HandleBundleMessage(FactorioNetMessage message)
        {
            if (needCount == bundleMessages.Count)
                return true;
            if (message.IsLastFragment)
                needCount = message.FragmentId + 1;
            if (!bundleMessages.ContainsKey(message.FragmentId))
                bundleMessages.Add(message.FragmentId, message);
            return needCount == bundleMessages.Count;
        }

        public FactorioNetMessage GetOverallMessage()
        {
            if (needCount != bundleMessages.Count)
                return null;
            var overallsize = bundleMessages.Values.Sum(netmsg => netmsg.PacketBytes.Length);
            var bytes = new byte[overallsize];
            var arrayptr = 0;
            foreach (var netmsg in bundleMessages.Values)
            {
                Array.Copy(netmsg.PacketBytes, 0, bytes, arrayptr, netmsg.PacketBytes.Length);
                arrayptr += netmsg.PacketBytes.Length;
            }

            return new FactorioNetMessage(bundleMessages.First().Value.Type, bytes);
        }
    }
}