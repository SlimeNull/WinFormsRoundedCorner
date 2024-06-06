using System.ComponentModel;

namespace WinFormsTest
{
    [Serializable]
    [TypeConverter(typeof(CornerRadiusConverter))]
    public struct CornerRadius
    {
        public int TopLeft { get; set; }
        public int TopRight { get; set; }
        public int BottomRight { get; set; }
        public int BottomLeft { get; set; }

        public bool IsEmpty =>
            TopLeft == 0 &&
            TopRight == 0 &&
            BottomRight == 0 &&
            BottomLeft == 0;

        public CornerRadius() { }

        public CornerRadius(int uniform)
        {
            TopLeft = TopRight = BottomRight = BottomLeft = uniform;
        }

        public CornerRadius(int topLeft, int topRight, int bottomRight, int bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
        }
    }
}
