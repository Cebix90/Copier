using Zadanie1;

namespace Zadanie2;

class Program
{
    static void Main(string[] args)
    {
        var xerox = new MultifunctionalDevice();
        xerox.PowerOn(); // 1
        IDocument doc1 = new PDFDocument("aaa.pdf");
        xerox.Print(in doc1);

        IDocument doc2;
        xerox.Scan(out doc2, IDocument.FormatType.TXT);

        xerox.SendFax(doc1);
        xerox.ReceiveFax(doc1);
        
        xerox.PowerOff();
        
        xerox.ScanAndPrint();
        xerox.SendFax(doc1);
        xerox.SendFax(doc2);
        
        xerox.PowerOff();
        
        xerox.PowerOn(); // 2
        xerox.PowerOff();
        xerox.PowerOn(); // 3
        
        xerox.ScanAndPrint();
        xerox.ScanAndPrint();
        xerox.Print(in doc1);
        
        xerox.PowerOn();
        
        Console.WriteLine($"Counter for PowerOn: {xerox.Counter}");
        Console.WriteLine($"Counter for PrintCounter: {xerox.PrintCounter}");
        Console.WriteLine($"Counter for ScanCounter: {xerox.ScanCounter}");
        Console.WriteLine($"Counter for FaxCounter: {xerox.FaxCounter}");
    }
}