namespace Zadanie5;

public class Copier
{
    #region properties=
    public int PrintCounter { get; private set; } = 0;
    public int ScanCounter { get; private set; } = 0;

    public IDevice.State state { get; private set; } = IDevice.State.Off;

    public int Counter { get; private set; } = 0;
    
    Printer printer = new Printer();
    Scanner scanner = new Scanner();

    #endregion


    #region PowerStates

    public void SetState(IDevice.State state)
    {
        this.state = state;
    }

    public IDevice.State GetState()
    {
        return state;
    }

    public IDevice.State GetPrinterState()
    {
        return printer.printerState;
    }

    public IDevice.State GetScannerState()
    {
        return scanner.scannerState;
    }

    public void PowerOff()
    {
        this.SetState(IDevice.State.Off);
        printer.printerState = IDevice.State.Off;
        scanner.scannerState = IDevice.State.Off;

        Console.WriteLine("... Device is off !");
    }

    public void PowerOn()
    {
        this.SetState(IDevice.State.On);
        printer.printerState = IDevice.State.On;
        scanner.scannerState = IDevice.State.On;
        Counter++;


        Console.WriteLine("Device is on ...");
    }

    public void StandbyOn()
    {
        SetState(IDevice.State.Standby);
        printer.printerState = IDevice.State.Standby;
        scanner.scannerState = IDevice.State.Standby;
    }

    public void StandbyOff()
    {
        this.SetState(IDevice.State.On);
        printer.printerState = IDevice.State.On;
        scanner.scannerState = IDevice.State.On;
    }

    #endregion
    
    public void Print(in IDocument document)
    {
        if (GetState() == IDevice.State.On)
        {
            scanner.scannerState = IDevice.State.Standby;
            
            printer.Print(document);
            PrintCounter++;
            
            if (PrintCounter % 3 == 0 && PrintCounter > 0)
            {
                printer.printerState = IDevice.State.Standby;
                Console.WriteLine("Printer in standby mode for a moment...");
            }
            else
            {
               printer.printerState = IDevice.State.On;
            }
        }
    }

    public void Scan(out IDocument document, IDocument.FormatType? formatType = null)
    {
        if (GetState() == IDevice.State.On)
        {
            printer.printerState = IDevice.State.Standby;
            
            scanner.Scan(out document, formatType);
            ScanCounter++;
            
            if (ScanCounter % 2 == 0 && ScanCounter > 0)
            {
                Console.WriteLine("Scanner in standby mode for a moment...");
                scanner.scannerState = IDevice.State.Standby;
            }
            else
            {
                scanner.scannerState = IDevice.State.On;
            }
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

#region classPrinter
sealed class Printer : IPrinter
{
    public IDevice.State printerState { get; set; } = IDevice.State.Off;
    public void SetState(IDevice.State state)
    {
        printerState = state;
    }
    public IDevice.State GetState()
    {
        return printerState;
    }

    public void PowerOn()
    {
        if (GetState() == IPrinter.State.Off)
        {
            printerState = IPrinter.State.On;
            Console.Write("Printer active");
        }
    }

    public void PowerOff()
    {
        if (GetState() == IPrinter.State.On)
        {
            printerState = IPrinter.State.Off;
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
sealed class Scanner : IScanner
{
    public IDevice.State scannerState { get; set; } = IDevice.State.Off;
    public void SetState(IDevice.State state)
    {
        scannerState = state;
    }
    public IDevice.State GetState()
    {
        return scannerState;
    }

    public void PowerOn()
    {
        if (GetState() == IScanner.State.Off)
        {
            scannerState = IScanner.State.On;
            Console.Write("Scanner active");
        }
    }

    public void PowerOff()
    {
        if (GetState() == IScanner.State.On)
        {
            scannerState = IScanner.State.Off;
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