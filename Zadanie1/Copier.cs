namespace Zadanie1;

public class Copier : BaseDevice, IPrinter, IScanner
{
    private int _scanCounter = 0;
    public int PrintCounter { get; private set; } = 0;

    public int ScanCounter
    {
        get => _scanCounter;
        private set => _scanCounter = value;

    }

    public override int Counter { get; protected set; }

    public static DateTime thisDay = DateTime.Now;
    public string data = $"{thisDay.ToString("d")}";
    public string time = $"{thisDay.ToString("T")}";

    
    public override void PowerOn()
    {
        var off = GetState();
        off = IDevice.State.Off;
        
        if (GetState() == IDevice.State.Off)
        {
            base.PowerOn();
            Counter++;
        }
    }

    public override void PowerOff()
    {
        if (GetState() == IDevice.State.On)
        {
            base.PowerOff();
        }
    }

    public void Print(in IDocument document)
    {
        if (GetState() == IDevice.State.On)
        {
            Console.WriteLine(
                $"{data} {time} Print: {document.GetFileName().Substring(0, document.GetFileName().Length - 4)}.{document.GetFormatType()}");
            PrintCounter++;
        }
    }

    public void Scan(out IDocument document, IDocument.FormatType? formatType = null)
    {
        document = null;
        
        if (GetState() == IDevice.State.On)
        {
            string fileName = "";

            if (formatType == IDocument.FormatType.PDF)
            {
                fileName = $"PDFScan{ScanCounter}.{formatType.ToString().ToLower()}";
                document = new PDFDocument(fileName);

            }
            else if (formatType == IDocument.FormatType.JPG)
            {
                fileName = $"ImageScan{ScanCounter}.{formatType.ToString().ToLower()}";
                document = new ImageDocument(fileName);
            }
            else if (formatType == IDocument.FormatType.TXT)
            {
                fileName = $"TextScan{ScanCounter}.{formatType.ToString().ToLower()}";
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