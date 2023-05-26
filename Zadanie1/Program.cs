namespace Zadanie1;

class Program
{
    static void Main()
    {
        var xerox = new Copier();
        xerox.PowerOn(); // 1
        IDocument doc1 = new PDFDocument("aaa.pdf");
        xerox.Print(in doc1);

        IDocument doc2;
        xerox.Scan(out doc2, IDocument.FormatType.TXT);

        xerox.PowerOff();
        
        xerox.ScanAndPrint();
        
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
    }
}