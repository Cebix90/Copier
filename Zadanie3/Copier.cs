namespace Zadanie3;

public class Copier : BaseDevice, IPrinter, IScanner
{
    public int ScanCounter { get; private set; } = 0;
    public int PrintCounter { get; private set; } = 0;

    private Printer printer = new Printer();
    private Scanner scanner = new Scanner();

    public override int Counter { get; protected set; }

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
            printer.Print(document);
            PrintCounter++;
        }
    }

    public void Scan(out IDocument document, IDocument.FormatType? formatType = null)
    {
        if (GetState() == IScanner.State.On)
        {
            scanner.Scan(out document, formatType);
            ScanCounter++;
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

public class Printer : BaseDevice, IPrinter
{ 
    public void Print(in IDocument document)
    {
        DateTime thisDay = DateTime.Now;
        string data = $"{thisDay:d}";
        string time = $"{thisDay:T}";
        
        Console.WriteLine(
        $"{data} {time} Print: {document.GetFileName().Substring(0, document.GetFileName().Length - 4)}.{document.GetFormatType()}");
    }
}

public class Scanner : BaseDevice, IScanner
{
    public int ScanCounter { get; private set; } = 0;
    
    public void Scan(out IDocument document, IDocument.FormatType? formatType = null)
    {
        DateTime thisDay = DateTime.Now;
        string data = $"{thisDay:d}";
        string time = $"{thisDay:T}";
        
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
}