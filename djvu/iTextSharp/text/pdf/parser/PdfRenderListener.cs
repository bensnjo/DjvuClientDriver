namespace djvu.iTextSharp.text.pdf.parser
{
    internal class PdfRenderListener
    {
        private object width;
        private object height;
        private Graphics graphics;

        public PdfRenderListener(object width, object height, Graphics graphics)
        {
            this.width = width;
            this.height = height;
            this.graphics = graphics;
        }
    }
}