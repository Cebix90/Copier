using Zadanie1;
using Zadanie2;
using Zadanie1UnitTests;

namespace Zadanie2UnitTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void MultifunctionalDevice_GetState_StateOff()
    {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOff();

        Assert.AreEqual(IDevice.State.Off, multifunctionalDevice.GetState()); 
    }

    [TestMethod]
    public void MultifunctionalDevice_GetState_StateOn()
    {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOn();

        Assert.AreEqual(IDevice.State.On, multifunctionalDevice.GetState());
    }
    
    // weryfikacja, czy po wywołaniu metody `SendFax` i włączonym urzadzeniu wielofunkcyjnym w napisie pojawia się słowo `Fax sent`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultifunctionalDevice_SendFax_DeviceOn()
    {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
        {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.SendFax(in doc1);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax sent"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);   
    }
    
    // weryfikacja, czy po wywołaniu metody `SendFax` i wyłączonym urzadzeniu wielofunkcyjnym w napisie NIE pojawia się słowo `Fax sent`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultifunctionalDevice_SendFax_DeviceOff()
    {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.SendFax(in doc1);
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax sent"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    // weryfikacja, czy po wywołaniu metody `ReceiveFax` i włączonym urzadzeniu wielofunkcyjnym w napisie pojawia się słowo `Fax received`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultifunctionalDevice_ReceiveFax_DeviceOn()
    {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
        {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.ReceiveFax(in doc1);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax received"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);   
    }
    
    // weryfikacja, czy po wywołaniu metody `ReceiveFax` i wyłączonym urzadzeniu wielofunkcyjnym w napisie NIE pojawia się słowo `Fax received`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
    public void MultifunctionalDevice_ReceiveFax_DeviceOff()
    {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter())
        {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.ReceiveFax(in doc1);
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax received"));
        }
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    [TestMethod]
    public void MultifunctionalDevice_FaxCounter()
    {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOn();

        IDocument doc1 = new TextDocument("aaa.txt");;
        multifunctionalDevice.SendFax(in doc1);
        IDocument doc2 = new PDFDocument("bbb.pdf");
        multifunctionalDevice.SendFax(in doc2);

        IDocument doc3 = new ImageDocument("aaa.jpg");
        multifunctionalDevice.SendFax(in doc3);

        multifunctionalDevice.PowerOff();
        multifunctionalDevice.ReceiveFax(in doc3);
        multifunctionalDevice.ReceiveFax(in doc1);
        multifunctionalDevice.PowerOn();

        multifunctionalDevice.ReceiveFax(doc3);
        multifunctionalDevice.ReceiveFax(doc1);

        // 4 faxy, gdy urządzenie włączone
        Assert.AreEqual(5, multifunctionalDevice.FaxCounter);
    }
}