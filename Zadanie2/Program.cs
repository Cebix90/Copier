using Zadanie1;

namespace Zadanie2;

class Program
{
    static void Main(string[] args)
    {
        var xerox = new MultifunctionalDevice();
        xerox.PowerOn();
        IDocument doc1 = new PDFDocument("aaa.pdf");
        xerox.Print(in doc1);

        IDocument doc2;
        xerox.Scan(out doc2);

        xerox.SendFax(doc1);
        xerox.ReceiveFax(doc1);
        
        xerox.ScanAndPrint();
        Console.WriteLine($"Counter for PowerOn: {xerox.Counter}");
        Console.WriteLine($"Counter for PrintCounter: {xerox.PrintCounter}");
        Console.WriteLine($"Counter for ScanCounter: {xerox.ScanCounter}");
        Console.WriteLine($"Counter for FaxCounter: {xerox.FaxCounter}");
    }
}