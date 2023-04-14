using djvu.models;
using DocumentFormat.OpenXml.Packaging;
using PdfiumViewer;
using QRCoder;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms.Integration;
using UglyToad.PdfPig.Geometry;
using PdfPig = UglyToad.PdfPig;


//using PdfiumViewer.Ext;




namespace djvu
{
    public partial class Srule : Form
    {
        private static PrintDocument interceptedDocument;
        private static IntPtr hHook = IntPtr.Zero;
        private PdfViewer pdfViewer;
        private ElementHost wpfHost;
        private System.Windows.Controls.RichTextBox wordPreview;
        private TransparentPanel transparentPanel;
        private OverlayPanel _overlayPanel;
        private object document;
        private string documentPath;
        private bool _isMouseDown = false;
        private Point _mouseDownLocation;
        private RectangleF _selectionRectangle;
        // private UglyToad.PdfPig.Geometry.PdfRectangle _selectionRectangle;

        DocumentModel documentModels = new DocumentModel();


        public Srule()
        {
            InitializeComponent();
            InitializeDocumentPreview();

            InitializeOverlayPanel();

            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.DrawItem += ListBox1_DrawItem;


        }

        private void InitializeOverlayPanel()
        {
            _overlayPanel = new OverlayPanel();
            _overlayPanel.Dock = DockStyle.Fill;
            _overlayPanel.MouseDown += OverlayPanel_MouseDown;
            _overlayPanel.MouseMove += OverlayPanel_MouseMove;
            _overlayPanel.MouseUp += OverlayPanel_MouseUp;

            // Add the _overlayPanel to the transparentPanel
            transparentPanel.Controls.Add(_overlayPanel);

            // Replace pdfViewer event handlers
            pdfViewer.MouseDown -= PdfViewer_MouseDown;
            pdfViewer.MouseMove -= PdfViewer_MouseMove;
            pdfViewer.MouseUp -= PdfViewer_MouseUp;

            // Add new event handlers for selection changed and zoom changed
            // pdfViewer.SelectionChange += PdfViewer_SelectionChanged;
            pdfViewer.Renderer.ZoomChanged += Renderer_ZoomChanged;
        }

        private void PdfViewer_SelectionChanged(object sender, EventArgs e)
        {
            // Extract and display words within the selection rectangle
            ExtractWordsInSelectedArea();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // ...
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // ...
        }

        private void printPreviewControl1_Click(object sender, EventArgs e)
        {
            // ...
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            // ...
        }

        protected override void OnClosing(CancelEventArgs e)
        {

        }


        private void button4_Click(object sender, EventArgs e)
        {
            // Intercept the print job, modify it as needed,
            // and then send it to the printer using SendDataToPrinter()
            documentPath = textBox5.Text;

            // Create the intercepted print document
            interceptedDocument = new PrintDocument();

            // Attach the PrintPageCallback to the PrintPage event
            interceptedDocument.PrintPage += new PrintPageEventHandler(PrintPageCallback);

            // Set the margins of the print document to make space for the QR code
            int qrCodeHeight = 100;
            interceptedDocument.DefaultPageSettings.Margins.Bottom = qrCodeHeight + 20;

            // Show the print preview dialog
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = interceptedDocument;

            // Open the document based on its file extension
            if (documentPath.EndsWith(".pdf"))
            {
                // Load and display the PDF document
                pdfViewer.Document = PdfiumViewer.PdfDocument.Load(documentPath);
                pdfViewer.Dock = DockStyle.Fill;
                printPreviewDialog.ShowDialog();
            }
            else if (documentPath.EndsWith(".docx"))
            {
                // Load and display the Word document
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(documentPath, false))
                {
                    var mainPart = wordDocument.MainDocumentPart;
                    using (Stream stream = mainPart.GetStream())
                    {
                        wordPreview.Selection.Load(stream, DataFormats.Rtf);
                    }
                }

                wpfHost.Child = wordPreview;
                printPreviewDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Unsupported file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /*private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_COMMAND)
            {
                int id = wParam.ToInt32() & 0xFFFF;
                if (id == IDOK)
                {
                    // Intercepted the print job, modify it as needed
                    // and then send it to the printer using SendDataToPrinter()
                    interceptedDocument = new PrintDocument();
                    interceptedDocument.PrintPage += new PrintPageEventHandler(PrintPageCallback);

                    // Display a message box
                    MessageBox.Show("Printing intercepted. Click OK to proceed with print preview.", "Printing Interception");

                    // Show the print preview dialog
                    PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                    printPreviewDialog.Document = interceptedDocument;
                    printPreviewDialog.ShowDialog();
                }
            }

            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }*/

        private static void InstallHook()
        {
            // Install a hook to intercept print jobs
            //hHook = SetWindowsHookEx(WH_CALLWNDPROC, HookCallback, IntPtr.Zero, 0);
        }

        private static void UninstallHook()
        {
            // Uninstall the hook
            UnhookWindowsHookEx(hHook);
        }

        private const int WH_CALLWNDPROC = 4;
        private const int WM_COMMAND = 0x0111;
        private const int IDOK = 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(IntPtr hHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);


        private void InitializeDocumentPreview()
        {
            pdfViewer = new PdfViewer();
            wpfHost = new ElementHost();
            wordPreview = new System.Windows.Controls.RichTextBox();

            wpfHost.Child = wordPreview;
            wpfHost.Dock = DockStyle.Fill;
            transparentPanel = new TransparentPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Supported Files|*.pdf;*.docx",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = openFileDialog.FileName;
                OpenDocument(openFileDialog.FileName);

            }



        }

        private void OpenDocument(string filePath)
        {
            panelDocumentPreview.Controls.Clear();

            string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();
            if (fileExtension == ".pdf")
            {
                pdfViewer.Document = PdfiumViewer.PdfDocument.Load(filePath);
                pdfViewer.Dock = DockStyle.Fill;
                panelDocumentPreview.Controls.Add(pdfViewer);
                panelDocumentPreview.Controls.Add(transparentPanel);

                document = pdfViewer.Document;
                // Bring the transparentPanel to the front
                transparentPanel.BringToFront();
            }
            else if (fileExtension == ".docx")
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filePath, false))
                {
                    var mainPart = wordDocument.MainDocumentPart;
                    using (Stream stream = mainPart.GetStream())
                    {
                        wordPreview.Selection.Load(stream, DataFormats.Rtf);
                    }
                }

                panelDocumentPreview.Controls.Add(wpfHost);
                panelDocumentPreview.Controls.Add(transparentPanel);

                // Bring the transparentPanel to the front
                transparentPanel.BringToFront();
            }
            else
            {
                MessageBox.Show("Unsupported file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OverlayPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = true;
                _mouseDownLocation = e.Location;
                _selectionRectangle = new RectangleF();
            }
        }


        private void OverlayPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                _selectionRectangle = new RectangleF(
                    Math.Min(_mouseDownLocation.X, e.X),
                    Math.Min(_mouseDownLocation.Y, e.Y),
                    Math.Abs(_mouseDownLocation.X - e.X),
                    Math.Abs(_mouseDownLocation.Y - e.Y)
                );

                textBox2.Text = _selectionRectangle.ToString();

                _overlayPanel.SelectionRectangle = _selectionRectangle;
                _overlayPanel.Invalidate(); // Force the control to redraw
            }
        }




        private void OverlayPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                _isMouseDown = false;

                // Extract and display words within the selection rectangle
                ExtractWordsInSelectedArea();
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PdfViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = true;
                _mouseDownLocation = e.Location;
                _selectionRectangle = new RectangleF();
            }
        }


        private void PdfViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                _selectionRectangle = new RectangleF(
                    Math.Min(_mouseDownLocation.X, e.X),
                    Math.Min(_mouseDownLocation.Y, e.Y),
                    Math.Abs(_mouseDownLocation.X - e.X),
                    Math.Abs(_mouseDownLocation.Y - e.Y)
                );

                textBox2.Text = _selectionRectangle.ToString();

                pdfViewer.Refresh(); // Force the control to redraw immediately
            }
        }



        private void PdfViewer_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                _isMouseDown = false;

                // Extract and display words within the selection rectangle
                ExtractWordsInSelectedArea();
            }
        }


        /*  private List<PointF> FindStringInPdf(string searchText, string filePath)
          {
              List<PointF> coordinates = new List<PointF>();

              try
              {
                  using (PdfiumViewer.PdfDocument pdfDoc = PdfiumViewer.PdfDocument.Load(filePath))
                  {
                      using (PdfiumViewer.PdfRenderer renderer = new PdfiumViewer.PdfRenderer(pdfDoc))
                      {
                          for (int pageIndex = 0; pageIndex < pdfDoc.PageCount; pageIndex++)
                          {
                              using (Bitmap pageImage = renderer.RenderPageToBitmap(pageIndex, 72, 72))
                              {
                                  double width = pageImage.Width;
                                  double height = pageImage.Height;

                                  int schResultCount = FPDFText_CountOccurrences(pdfDoc.NativeMethods.Handle, searchText);
                                  if (schResultCount > 0)
                                  {
                                      for (int i = 0; i < schResultCount; i++)
                                      {
                                          double left, top, right, bottom;
                                          FPDFText_GetSearchResult(pdfDoc.NativeMethods.Handle, i, out left, out top, out right, out bottom);
                                          coordinates.Add(new PointF((float)left, (float)top));
                                      }
                                  }
                              }
                          }
                      }
                  }
              }
              catch (PdfiumViewerException ex)
              {
                  // Handle any exceptions that may be thrown
              }

              return coordinates;
          } */





        // Add this class-level variable
        // private Tuple<float, float, float, float> _selectedAreaCoordinates;

        private void ExtractWordsInSelectedArea()
        {
            int currentPageIndex = pdfViewer.Renderer.Page;

            double zoomFactor = pdfViewer.Renderer.Zoom;
            var pageSize = pdfViewer.Document.PageSizes[currentPageIndex];
            double dpiX, dpiY;
            using (Graphics graphics = CreateGraphics())
            {
                dpiX = graphics.DpiX;
                dpiY = graphics.DpiY;
            }
            float scaleX = (float)(pageSize.Width * dpiX * zoomFactor / 72.0);
            float scaleY = (float)(pageSize.Height * dpiY * zoomFactor / 72.0);

            RectangleF selectionInPoints = new RectangleF(
                (float)(_selectionRectangle.X * 72.0 / dpiX / zoomFactor),
                (float)((pageSize.Height - (_selectionRectangle.Bottom * 72.0 / dpiY / zoomFactor))),
                (float)(_selectionRectangle.Width * 72.0 / dpiX / zoomFactor),
                (float)(_selectionRectangle.Height * 72.0 / dpiY / zoomFactor)
            );

            string extractedText = ExtractTextFromArea(currentPageIndex + 1, selectionInPoints);

            textBox3.Text = extractedText;
        }


        private string ExtractTextFromArea(int pageNumber, RectangleF area)
        {
            using (var document = PdfPig.PdfDocument.Open(textBox5.Text))
            {
                var page = document.GetPage(pageNumber);
                var contentArea = new PdfPig.Core.PdfRectangle(area.X, area.Y, area.Width, area.Height);

                var extractedText = new StringBuilder();
                foreach (var letter in page.Letters)
                {
                    if (contentArea.Contains(letter.GlyphRectangle))
                    {
                        extractedText.Append(letter.Value);
                    }
                }

                return extractedText.ToString();
            }
        }


        private void PdfViewer_DocumentLoaded(object sender, EventArgs e)
        {
            UpdatePanelsSize();
        }

        private void UpdatePanelsSize()
        {
            int currentPageIndex = pdfViewer.Renderer.Page;

            double zoomFactor = pdfViewer.Renderer.Zoom;
            var pageSize = pdfViewer.Document.PageSizes[currentPageIndex];
            double dpiX, dpiY;
            using (Graphics graphics = this.CreateGraphics())
            {
                dpiX = graphics.DpiX;
                dpiY = graphics.DpiY;
            }
            float width = (float)(pageSize.Width * dpiX * zoomFactor / 72.0);
            float height = (float)(pageSize.Height * dpiY * zoomFactor / 72.0);

            _overlayPanel.Size = new Size((int)width, (int)height);
            transparentPanel.Size = new Size((int)width, (int)height);
        }

        private void Renderer_ZoomChanged(object sender, EventArgs e)
        {
            UpdatePanelsSize();
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();

        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Check if the current item is selected
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Check if the current item was already selected
            bool wasSelected = listBox1.SelectedIndices.Contains(e.Index);

            // Set the background color based on the selection state
            if (isSelected)
            {
                e.Graphics.FillRectangle(Brushes.Blue, e.Bounds);
            }
            else if (wasSelected)
            {
                e.Graphics.FillRectangle(Brushes.LightGreen, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }

            // Draw the item text
            string itemText = listBox1.Items[e.Index].ToString();
            e.Graphics.DrawString(itemText, e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);

            // Draw a focus rectangle if necessary
            if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
            {
                e.DrawFocusRectangle();
            }
        }

        private void TransparentPanel_Paint(object sender, PaintEventArgs e)
        {
            if (_isMouseDown && !_selectionRectangle.IsEmpty)
            {
                using (var pen = new Pen(Color.Blue, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                {
                    e.Graphics.DrawRectangle(pen, Rectangle.Round(_selectionRectangle));
                }

                // Add this code to fill the selection rectangle with a semi-transparent color
                using (var brush = new SolidBrush(Color.FromArgb(128, Color.LightBlue)))
                {
                    e.Graphics.FillRectangle(brush, _selectionRectangle);
                }
            }
        }

        public static Bitmap GenerateQRCode(string text, int size)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(size);
            return qrCodeImage;
        }

        private void PrintPageCallback(object sender, PrintPageEventArgs e)
        {
            if (document is PdfiumViewer.PdfDocument pdfDocument)
            {
                // Draw the PDF document
                if (pdfDocument != null)
                {
                    int pageNumber = pdfViewer.Renderer.Page;
                    double zoomFactor = pdfViewer.Renderer.Zoom;
                    float dpiX = e.Graphics.DpiX;
                    float dpiY = e.Graphics.DpiY;

                    Rectangle bounds = new Rectangle(
                        (int)e.MarginBounds.X,
                        (int)e.MarginBounds.Y,
                        (int)e.MarginBounds.Width,
                        (int)e.MarginBounds.Height
                    );

                    pdfDocument.Render(pageNumber, e.Graphics, dpiX, dpiY, bounds, PdfRenderFlags.None);
                }
            }
            else if (document is WordprocessingDocument wordDocument)
            {
                // Draw the Word document
                using (Stream stream = wordDocument.MainDocumentPart.GetStream())
                {
                    wordPreview.Selection.Load(stream, DataFormats.Rtf);
                }
            }

            // Draw the QR code image
            Image qrCodeImage = GenerateQRCode("Hello world!", 100);
            int x = (e.PageBounds.Width - qrCodeImage.Width) / 2;
            int y = e.PageBounds.Bottom - qrCodeImage.Height - 20;
            e.Graphics.DrawImage(qrCodeImage, x, y);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Template saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        //
        [DllImport("pdfium.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int FPDF_GetPageCount(IntPtr document);

        [DllImport("pdfium.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr FPDF_LoadPage(IntPtr document, int page_index);

        [DllImport("pdfium.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double FPDF_GetPageWidth(IntPtr page);

        [DllImport("pdfium.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double FPDF_GetPageHeight(IntPtr page);

        [DllImport("pdfium.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int FPDFText_CountOccurrences(IntPtr page, [MarshalAs(UnmanagedType.LPWStr)] string needle);

        [DllImport("pdfium.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void FPDFText_GetSearchResult(IntPtr page, int index, out double left, out double top, out double right, out double bottom);

        [DllImport("pdfium.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void FPDF_ClosePage(IntPtr page);

        private void button6_Click(object sender, EventArgs e)
        {
            Test test = new Test();
            this.Hide();
            test.Show();
        }
    }
}
