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

    private static DateTime thisDay = DateTime.Now;
    protected string data = $"{thisDay:d}";
    protected string time = $"{thisDay:T}";

    
    public override void PowerOn()
    {
        var off = IDevice.State.Off;
        
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
        if (GetState() == IDevice.State.On)
        {
            ScanCounter++;
            string fileName = "";
            IDocument.FormatType selectedFormatType = formatType ?? IDocument.FormatType.JPG;

            if (selectedFormatType == IDocument.FormatType.PDF)
            {
                fileName = $"PDFScan{ScanCounter}.{selectedFormatType.ToString().ToLower()}";
                document = new PDFDocument(fileName);
            }
            else if (selectedFormatType == IDocument.FormatType.TXT)
            {
                fileName = $"TextScan{ScanCounter}.{selectedFormatType.ToString().ToLower()}";
                document = new TextDocument(fileName);
            }
            else
            {
                fileName = $"ImageScan{ScanCounter}.{selectedFormatType.ToString().ToLower()}";
                document = new ImageDocument(fileName);
            }

            Console.WriteLine($"{data} {time} Scan: {fileName}");
        }
        else
        {
            document = null;
        }
    }


    public void ScanAndPrint()
    {
        IDocument document;

        Scan(out document);

        Print(document);
    }
}