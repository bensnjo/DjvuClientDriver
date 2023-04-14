using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace djvu
{
    public class OverlayPanel : Panel
    {
        public RectangleF SelectionRectangle { get; set; }

        public OverlayPanel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!SelectionRectangle.IsEmpty)
            {
                using (var pen = new Pen(Color.Blue, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                {
                    e.Graphics.DrawRectangleF(pen, SelectionRectangle);
                }

                using (var brush = new SolidBrush(Color.FromArgb(128, Color.LightBlue)))
                {
                    e.Graphics.FillRectangleF(brush, SelectionRectangle);
                }
            }
        }






        // end of a class
    }


    public static class GraphicsExtensions
    {
        public static void DrawRectangleF(this Graphics graphics, Pen pen, RectangleF rect)
        {
            graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static void FillRectangleF(this Graphics graphics, Brush brush, RectangleF rect)
        {
            graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }
    }

}
