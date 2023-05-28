namespace Zadanie5;

class Program
{
    static void Main()
    {
        Copier copier = new Copier();
        
        Console.WriteLine("Copier stan: " + copier.GetState());
        Console.WriteLine("Printer stan: " + copier.GetPrinterState());
        Console.WriteLine("Scanner stan: " + copier.GetScannerState());

        copier.PowerOn();

        Console.WriteLine("Copier stan: " + copier.GetState());
        Console.WriteLine("Printer stan: " + copier.GetPrinterState());
        Console.WriteLine("Scanner stan: " + copier.GetScannerState());

        IDocument doc1 = new PDFDocument("aaa");
        
        copier.Print(doc1);
        copier.Print(doc1);
        copier.Print(doc1);
        Console.WriteLine("Copier stan: " + copier.GetState());
        Console.WriteLine("Printer stan: " + copier.GetPrinterState());
        Console.WriteLine("Scanner stan: " + copier.GetScannerState());
        copier.Print(doc1);
        copier.Print(doc1);

        Console.WriteLine("Copier stan: " + copier.GetState());
        Console.WriteLine("Printer stan: " + copier.GetPrinterState());
        Console.WriteLine("Scanner stan: " + copier.GetScannerState());

        IDocument file = new PDFDocument("aaa");

        copier.Scan(out file, IDocument.FormatType.JPG);
        copier.Scan(out file, IDocument.FormatType.JPG);
        copier.Scan(out file, IDocument.FormatType.JPG);
        copier.Scan(out file, IDocument.FormatType.JPG);

        Console.WriteLine("Copier stan: " + copier.GetState());
        Console.WriteLine("Printer stan: " + copier.GetPrinterState());
        Console.WriteLine("Scanner stan: " + copier.GetScannerState());

        copier.StandbyOn();
        copier.StandbyOff();

        Console.WriteLine("Copier stan: " + copier.GetState());
        Console.WriteLine("Printer stan: " + copier.GetPrinterState());
        Console.WriteLine("Scanner stan: " + copier.GetScannerState());
        
        Console.WriteLine($"Counter for PowerOn: {copier.Counter}");
        Console.WriteLine($"Counter for PrintCounter: {copier.PrintCounter}");
        Console.WriteLine($"Counter for ScanCounter: {copier.ScanCounter}");
    }
}