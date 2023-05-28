namespace Zadanie4;

public class Copier : IPrinter, IScanner
{
    #region properties
    public int PrintCounter { get; private set; } = 0;
    public int ScanCounter { get; private set; } = 0;
    
    public IDevice.State state { get; private set; } = IDevice.State.Off;
    public IDevice.State printerState { get; private set; } = IDevice.State.Off;
    public IDevice.State scannerState { get; private set; } = IDevice.State.Off;

    public int Counter { get; private set; } = 0;
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
        return printerState;
    }
    public IDevice.State GetScannerState()
    {
        return scannerState;
    }

    public void PowerOff()
    {
        this.SetState(IDevice.State.Off);
        printerState = IDevice.State.Off;
        scannerState = IDevice.State.Off;

        Console.WriteLine("... Device is off !");
    }

    public void PowerOn()
    {
        this.SetState(IDevice.State.On);
        printerState = IDevice.State.On;
        scannerState = IDevice.State.On;

        Counter++;
        Console.WriteLine("Device is on ...");
    }

    public void StandbyOn()
    {
        SetState(IDevice.State.Standby);
        printerState = IDevice.State.Standby;
        scannerState = IDevice.State.Standby;
    }

    public void StandbyOff()
    {
        SetState(IDevice.State.On);
        printerState = IDevice.State.On;
        scannerState = IDevice.State.On;
    }
    #endregion
    
    private static DateTime thisDay = DateTime.Now;
    protected string data = $"{thisDay:d}";
    protected string time = $"{thisDay:T}";

    #region Printer
    public void Print(in IDocument document)
    {
        if (state == IDevice.State.On)
        {
            scannerState = IDevice.State.Standby;
           
            Console.WriteLine($"{data} {time} Print: {document.GetFileName()}");
            PrintCounter++;
            
            if (PrintCounter % 3 == 0 && PrintCounter > 0)
            {
                printerState = IDevice.State.Standby;
                Console.WriteLine("Printer in standby mode for a moment...");
            }
            else
            {
                printerState = IDevice.State.On;
            }
        }
    }
    #endregion

    #region Scanner
    public void Scan(out IDocument document, IDocument.FormatType? formatType = null)
    {
        if (state == IDevice.State.On)
        {
            printerState = IDevice.State.Standby;
            
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
            
            if (ScanCounter % 2 == 0 && ScanCounter > 0)
            {
                scannerState = IDevice.State.Standby;
                Console.WriteLine("Scanner in standby mode for a moment...");
            }
            else
            {
                scannerState = IDevice.State.On;
            }
        }
        else
        {
            document = null;
        }
    }
    #endregion


    public void ScanAndPrint()
    {
        scannerState = IDevice.State.On;
        printerState = IDevice.State.On;
        
        IDocument document;

        Scan(out document);

        Print(document);
    }
}