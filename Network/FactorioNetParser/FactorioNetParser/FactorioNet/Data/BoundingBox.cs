using System.IO;

namespace FactorioNetParser.FactorioNet.Data
{
    internal class BoundingBox : IReadable<BoundingBox>
    {
        public PixelPosition LeftTop;
        public PixelPosition RightBottom;
        public RealOrientation Orientation;

        public BoundingBox(BinaryReader reader)
        {
            Load(reader);
        }

        public BoundingBox() { }

        public BoundingBox Load(BinaryReader reader)
        {
            LeftTop = new PixelPosition(reader);
            RightBottom = new PixelPosition(reader);
            Orientation = new RealOrientation(reader);
            return this;
        }
    }
}