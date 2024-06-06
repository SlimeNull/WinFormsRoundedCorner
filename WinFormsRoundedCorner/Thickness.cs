using System.ComponentModel;

namespace WinFormsTest
{
    [Serializable]
    [TypeConverter(typeof(ThicknessConverter))]
    public struct Thickness
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public bool IsEmpty =>
            Left == 0 &&
            Top == 0 &&
            Right == 0 &&
            Bottom == 0;

        public Thickness() { }

        public Thickness(int uniform)
        {
            Left = Top = Right = Bottom = uniform;
        }

        public Thickness(int horizontal, int vertical)
        {
            Left = Right = horizontal;
            Top = Bottom = vertical;
        }

        public Thickness(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

}
