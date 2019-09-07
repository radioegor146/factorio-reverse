﻿using System;
using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class SignalId : IReadable<SignalId>
    {
        public byte ContainedType;
        public ushort FluidId;
        public ushort ItemId;
        public ushort VirtualSignalId;

        public SignalId(BinaryReader reader)
        {
            Load(reader);
        }

        public SignalId() { }

        public SignalId Load(BinaryReader reader)
        {
            ContainedType = reader.ReadByte();
            switch (ContainedType)
            {
                case 0:
                    ItemId = reader.ReadUInt16();
                    break;
                case 1:
                    FluidId = reader.ReadUInt16();
                    break;
                case 2:
                    VirtualSignalId = reader.ReadUInt16();
                    break;
                default:
                    throw new Exception($"Invalid SignalId type: {ContainedType}");
            }

            return this;
        }
    }
}