using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsTest
{

    public class RoundedRectangle : Control
    {
        private CornerRadius _cornerRadius;
        private Thickness _borderThickness = new Thickness(1);
        private Color _borderColor = Color.Gray;

        [Category("Appearance")]
        public CornerRadius CornerRadius
        {
            get => _cornerRadius; 
            set
            {
                _cornerRadius = value;
                Invalidate();
            }
        }


        [Category("Appearance")]
        public Thickness BorderThickness
        {
            get => _borderThickness; 
            set
            {
                _borderThickness = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get => _borderColor; 
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }
        /// <summary>
        /// 校正圆角 (防止它过大)
        /// </summary>
        /// <param name="cornerRadius"></param>
        /// <param name="areaWidth"></param>
        /// <param name="areaHeight"></param>
        /// <returns></returns>
        private CornerRadius CorrectCornerRadius(CornerRadius cornerRadius, int areaWidth, int areaHeight)
        {
            var widthHeightMin = Math.Min(areaWidth, areaHeight);
            var halfWidthHeightMin = widthHeightMin / 2;

            return new CornerRadius(
                Math.Min(cornerRadius.TopLeft, halfWidthHeightMin),
                Math.Min(cornerRadius.TopRight, halfWidthHeightMin),
                Math.Min(cornerRadius.BottomRight, halfWidthHeightMin),
                Math.Min(cornerRadius.BottomLeft, halfWidthHeightMin));
        }

        private Thickness CorrectBorderThickness(Thickness borderThickness, int areaWidth, int areaHeight)
        {
            var widthHeightMin = Math.Min(areaWidth, areaHeight);
            var halfWidthHeightMin = widthHeightMin / 2;

            return new Thickness(
                Math.Min(borderThickness.Left, halfWidthHeightMin),
                Math.Min(borderThickness.Top, halfWidthHeightMin),
                Math.Min(borderThickness.Right, halfWidthHeightMin),
                Math.Min(borderThickness.Bottom, halfWidthHeightMin));
        }

        /// <summary>
        /// 获取边框的内部圆角
        /// </summary>
        /// <param name="cornerRadius"></param>
        /// <param name="borderThickness"></param>
        /// <returns></returns>
        private CornerRadius GetBorderInnerCornerRadius(CornerRadius cornerRadius, Thickness borderThickness)
        {
            return new CornerRadius(
                Math.Max(cornerRadius.TopLeft - Math.Max(borderThickness.Left, borderThickness.Top), 0),
                Math.Max(cornerRadius.TopRight - Math.Max(borderThickness.Right, borderThickness.Top), 0),
                Math.Max(cornerRadius.BottomRight - Math.Max(borderThickness.Right, borderThickness.Bottom), 0),
                Math.Max(cornerRadius.BottomLeft - Math.Max(borderThickness.Left, borderThickness.Bottom), 0));
        }

        /// <summary>
        /// 构建一个圆角矩形
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="cornerRadius"></param>
        /// <returns></returns>
        private GraphicsPath BuildRoundedRectangle(Rectangle rectangle, CornerRadius cornerRadius)
        {
            var path = new GraphicsPath();

            var topLeftCornerDiameter = cornerRadius.TopLeft * 2;

            if (topLeftCornerDiameter > 0)
            {
                path.AddArc(new Rectangle(rectangle.X, rectangle.Y, topLeftCornerDiameter, topLeftCornerDiameter), 180, 90);
            }
            path.AddLine(new Point(cornerRadius.TopLeft + rectangle.X, rectangle.Y), new Point(rectangle.Width - cornerRadius.TopRight + rectangle.X, rectangle.Y));

            var topRightCornerDiameter = cornerRadius.TopRight * 2;
            if (topRightCornerDiameter > 0)
            {
                path.AddArc(new Rectangle(rectangle.Width - topRightCornerDiameter + rectangle.X, rectangle.Y, topRightCornerDiameter, topRightCornerDiameter), 270, 90);
            }
            path.AddLine(new Point(rectangle.Width + rectangle.X, cornerRadius.TopRight + rectangle.Y), new Point(rectangle.Width + rectangle.X, rectangle.Height - cornerRadius.BottomRight + rectangle.Y));

            var bottomRightCornerDiamter = cornerRadius.BottomRight * 2;
            if (bottomRightCornerDiamter > 0)
            {
                path.AddArc(new Rectangle(rectangle.Width - bottomRightCornerDiamter + rectangle.X, rectangle.Height - bottomRightCornerDiamter + rectangle.Y, bottomRightCornerDiamter, bottomRightCornerDiamter), 0, 90);
            }
            path.AddLine(new Point(rectangle.Width - cornerRadius.BottomRight + rectangle.X, rectangle.Height + rectangle.Y), new Point(cornerRadius.BottomLeft + rectangle.X, rectangle.Height + rectangle.Y));

            var bottomLeftCornerDiameter = cornerRadius.BottomLeft * 2;
            if (bottomLeftCornerDiameter > 0)
            {
                path.AddArc(new Rectangle(rectangle.X, rectangle.Height - bottomLeftCornerDiameter + rectangle.Y, bottomLeftCornerDiameter, bottomLeftCornerDiameter), 90, 90);
            }
            path.AddLine(new Point(rectangle.X, rectangle.Height - cornerRadius.BottomLeft + rectangle.Y), new Point(rectangle.X, cornerRadius.TopLeft + rectangle.Y));


            return path;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            var borderThickness = CorrectBorderThickness(BorderThickness, Width, Height);
            var cornerRadius = CorrectCornerRadius(CornerRadius, Width, Height);
            var innerCornerRadius = GetBorderInnerCornerRadius(cornerRadius, borderThickness);

            using var borderBrush = new SolidBrush(BorderColor);
            using var backgroundBrush = new SolidBrush(BackColor);


            using var path = BuildRoundedRectangle(
                new Rectangle(0, 0, Width, Height), cornerRadius);
            using var innerPath = BuildRoundedRectangle(
                new Rectangle(borderThickness.Left, borderThickness.Top, Width - borderThickness.Left - borderThickness.Right, Height - borderThickness.Top - borderThickness.Bottom), innerCornerRadius);

            //g.SmoothingMode = SmoothingMode.AntiAlias;

            if (Parent is Control parent)
            {
                g.Clear(parent.BackColor);
            }
            else
            {
                g.Clear(BackColor);
            }

            g.FillPath(borderBrush, path);
            g.FillPath(backgroundBrush, innerPath);
        }
    }
}
