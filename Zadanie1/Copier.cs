namespace Zadanie1;

public class Copier : BaseDevice, IPrinter, IScanner
{
    private int scanCounter = 0;
    public int PrintCounter { get; private set; } = 0;

    public int ScanCounter
    {
        get => scanCounter;
        private set => scanCounter = value;

    }

    public new int Counter { get; set; }

    static DateTime thisDay = DateTime.Now;
    string data = $"{thisDay.ToString("d")}";
    string time = $"{thisDay.ToString("T")}";

    
    public new void PowerOn()
    {
        var off = GetState();
        off = IDevice.State.off;
        
        if (GetState() == IDevice.State.off)
        {
            base.PowerOn();
            Counter++;
        }
    }

    public override void PowerOff()
    {
        if (GetState() == IDevice.State.on)
        {
            base.PowerOff();
        }
    }

    public void Print(in IDocument document)
    {
        if (GetState() == IDevice.State.on)
        {
            Console.WriteLine(
                $"{data} {time} Print: {document.GetFileName().Substring(0, document.GetFileName().Length - 4)}.{document.GetFormatType()}");
            PrintCounter++;
        }
    }

    public void Scan(out IDocument document, IDocument.FormatType? formatType = null)
    {
        document = null;
        
        if (GetState() == IDevice.State.on)
        {
            string fileName = "";

            if (formatType == IDocument.FormatType.PDF)
            {
                fileName = $"PDFScan{ScanCounter}.pdf";
                document = new PDFDocument(fileName);

            }
            else if (formatType == IDocument.FormatType.JPG)
            {
                fileName = $"ImageScan{ScanCounter}.jpg";
                document = new ImageDocument(fileName);
            }
            else if (formatType == IDocument.FormatType.TXT)
            {
                fileName = $"TextScan{ScanCounter}.txt";
                document = new TextDocument(fileName);
            }

            Console.WriteLine($"{data} {time} Scan: {fileName}");
            ScanCounter++;
        }
    }

    public void ScanAndPrint()
    {
        IDocument document;
        IDocument.FormatType formatType = IDocument.FormatType.JPG;

        Scan(out document, formatType);

        if (document != null)
        {
            Print(document);
        }
    }
}