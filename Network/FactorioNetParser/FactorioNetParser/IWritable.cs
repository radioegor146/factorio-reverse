using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioNetParser
{
    public interface IWritable<T>
    {
        void Write(System.IO.BinaryWriter writer);
    }
}
