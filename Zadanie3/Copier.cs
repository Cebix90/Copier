namespace Zadanie3;

#region classCopier
public class Copier : BaseDevice, IPrinter, IScanner
{
    public int ScanCounter { get; private set; } = 0;
    public int PrintCounter { get; private set; } = 0;

    protected Printer printer = new Printer();
    protected Scanner scanner = new Scanner();

    public override int Counter { get; protected set; }

    public override void PowerOn()
    {
        var offPrinter = IPrinter.State.Off;
        var offScanner = IScanner.State.Off;
        
        if (printer.GetState() == IPrinter.State.Off && scanner.GetState() == IScanner.State.Off)
        {
            state = IDevice.State.On;
            
            Console.Write($"Device is on... (");
            printer.PowerOn();
            Console.Write(", ");
            scanner.PowerOn();
            Console.WriteLine(")");
            
            Counter++;
        }
    }

    public override void PowerOff()
    {
        if (printer.GetState() == IPrinter.State.On && scanner.GetState() == IScanner.State.On)
        {
            state = IDevice.State.Off;
            
            Console.Write($"Device is off... (");
            printer.PowerOff();
            Console.Write(", ");
            scanner.PowerOff();
            Console.WriteLine(")");
        }
    }
    
    public void Print(in IDocument document)
    {
        if (printer.GetState() == IPrinter.State.On)
        {
            printer.Print(document);
            PrintCounter++;
        }
    }

    public void Scan(out IDocument document, IDocument.FormatType? formatType = null)
    {
        if (scanner.GetState() == IScanner.State.On)
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
#endregion

#region classPrinter
public class Printer : BaseDevice, IPrinter
{ 
    public override void PowerOn()
    {
        if (GetState() == IPrinter.State.Off)
        {
            state = IPrinter.State.On;
            Console.Write("Printer active");
        }
    }

    public override void PowerOff()
    {
        if (GetState() == IPrinter.State.On)
        {
            state = IPrinter.State.Off;
            Console.Write("Printer inactive");
        }
    }
    public void Print(in IDocument document)
    {
        string dateTimeString = DateTimeHelper.GetCurrentDateTimeString();
        
        Console.WriteLine(
        $"{dateTimeString} Print: {document.GetFileName()}");
    }
}
#endregion

#region classScanner
public class Scanner : BaseDevice, IScanner
{
    public override void PowerOn()
    {
        if (GetState() == IScanner.State.Off)
        {
            state = IScanner.State.On;
            Console.Write("Scanner active");
        }
    }

    public override void PowerOff()
    {
        if (GetState() == IScanner.State.On)
        {
            state = IScanner.State.Off;
            Console.Write("Scanner inactive");
        }
    }
    public int ScanCounter { get; private set; } = 0;
    
    public void Scan(out IDocument document, IDocument.FormatType? formatType = null)
    {
        string dateTimeString = DateTimeHelper.GetCurrentDateTimeString();
        
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

        Console.WriteLine($"{dateTimeString} Scan: {fileName}");
    }
}
#endregion

#region DateTimeHelper
public static class DateTimeHelper
{
    public static string GetCurrentDateTimeString()
    {
        DateTime thisDay = DateTime.Now;
        string data = $"{thisDay:d}";
        string time = $"{thisDay:T}";
        return $"{data} {time}";
    }
}
#endregion