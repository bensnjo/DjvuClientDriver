using Leadtools;
using Leadtools.Document.Writer;
using Leadtools.Printer;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Leadtools.Controls;
using Leadtools.WinForms;
using System.IO;
using System;
using Leadtools.Codecs;
using Leadtools.Drawing;
using Leadtools.Windows.Controls;

namespace djvu
{
    public partial class Test : Form
    {
        Printer LeadPrinter;
        //DocumentWriter DocumentWriter;
        PrinterInfo PrinterInfo;
        string OutputFile;
        private static Leadtools.Controls.ImageViewer imageViewer = new Leadtools.Controls.ImageViewer();
        private static DocumentWriter DocumentWriter = new DocumentWriter();


        private static Leadtools.Controls.ImageViewer _imageViewer;
        private static DocumentWriter _documentWriter;

        PictureBox pictureBox;



        public Test()
        {
            InitializeComponent();
            SetLicense();
            SetupPrinter();

            Console.WriteLine("Printer set up and ready to receive jobs. Print to the LEADTOOLS Printer to see it working. Press enter to exit.");
            Console.ReadLine();

            // Printer.UnInstall(PrinterInfo);
          /*  imageViewer = new Leadtools.Controls.ImageViewer();
            imageViewer.Dock = DockStyle.Fill;
            this.Controls.Add(imageViewer);*/
        }

        void SetLicense()
        {
            string license = @"C:\LEADTOOLS22\Support\Common\License\LEADTOOLS.LIC";
            string key = File.ReadAllText(@"C:\LEADTOOLS22\Support\Common\License\LEADTOOLS.LIC.KEY");
            RasterSupport.SetLicense(license, key);
            if (RasterSupport.KernelExpired)
                Console.WriteLine("License file invalid or expired.");
            else
                Console.WriteLine("License file set successfully");
        }

        void SetupPrinter()
        {
            List<string> installedPrinters = new List<string>();

            foreach (string printer in PrinterSettings.InstalledPrinters)
                installedPrinters.Add(printer);

            string printerName = "LEADTOOLS Printer007";
            string printerPassword = "";
            string documentPrinterRegPath = @"SOFTWARE\LEAD Technologies, Inc.\21\Printer\";
            PrinterInfo = new PrinterInfo
            {
                MonitorName = printerName,
                PortName = printerName,
                ProductName = printerName,
                PrinterName = printerName,
                Password = printerPassword,
                RegistryKey = documentPrinterRegPath + printerName,
                RootDir = @"C:\LEADTOOLS22\Bin\Common\PrinterDriver\",
                Url = "https://www.leadtools.com",
                PrinterExe = AppDomain.CurrentDomain.BaseDirectory
            };

            if (!installedPrinters.Contains(printerName))
                Printer.Install(PrinterInfo);

            LeadPrinter = new Printer(printerName);
            LeadPrinter.EmfEvent += (sender, e) => LeadPrinter_EmfEvent(sender, e);
            LeadPrinter.JobEvent += (sender, e) => LeadPrinter_JobEvent(sender, e, this);

            DocumentWriter = new DocumentWriter();
        }

        static void LeadPrinter_EmfEvent(object sender, EmfEventArgs e)
        {
            try
            {
                Metafile metaFile = new Metafile(e.Stream);
                using (var rasterImage = RasterImageConverter.FromEmf(metaFile.GetHenhmetafile(), Color.White.ToArgb(), 0))
                {
                    imageViewer.Image = rasterImage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LeadPrinter_EmfEvent: {ex.Message}");
            }
        }


        static void LeadPrinter_JobEvent(object sender, JobEventArgs e, Test formInstance)
        {
            string printerName = e.PrinterName;
            int jobID = e.JobID;
            string outputFileName = $"doc_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";

            try
            {
                if (e.JobEventState == EventState.JobStart)
                {
                    _documentWriter = new DocumentWriter();
                    _documentWriter.BeginDocument(outputFileName, Leadtools.Document.Writer.DocumentFormat.Pdf);

                    Console.WriteLine($"Job {jobID} for {printerName} was started");
                }
                else if (e.JobEventState == EventState.JobEnd)
                {
                    _documentWriter.EndDocument();

                    RasterCodecs codecs = new RasterCodecs();
                    using (FileStream fs = new FileStream(outputFileName, FileMode.Open, FileAccess.Read))
                    {
                        RasterImage image = codecs.Load(fs);
                        formInstance.pictureBox1.Image = RasterImageConverter.ConvertToImage(image, ConvertToImageOptions.None);
                        Console.WriteLine($"Output file: {outputFileName}");
                    }

                    Console.WriteLine($"Job {jobID} for {printerName} was ended. PDF displayed in the image viewer.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in LeadPrinter_JobEvent: {ex.Message}");
            }
        }


    }
}
