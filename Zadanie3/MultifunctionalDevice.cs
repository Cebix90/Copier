namespace Zadanie3;

public class MultifunctionalDevice : Copier, IFax
{
    public int FaxCounter { get; private set; } = 0;

    private Fax fax = new Fax();
    
    public override void PowerOn()
    {
        var offPrinter = IPrinter.State.Off;
        var offScanner = IScanner.State.Off;
        var offFax = IFax.State.Off;
        
        if (fax.GetState() == IFax.State.Off && printer.GetState() == IPrinter.State.Off && scanner.GetState() == IScanner.State.Off)
        {
            state = IDevice.State.On;
            
            Console.Write($"Device is on... (");
            printer.PowerOn();
            Console.Write(", ");
            scanner.PowerOn();
            Console.Write(", ");
            fax.PowerOn();
            Console.WriteLine(")");

            Counter++;
        }
    }

    public override void PowerOff()
    {
        if (fax.GetState() == IFax.State.On && printer.GetState() == IPrinter.State.On && scanner.GetState() == IScanner.State.On)
        {
            state = IDevice.State.Off;
            
            Console.Write($"Device is off... (");
            printer.PowerOff();
            Console.Write(", ");
            scanner.PowerOff();
            Console.Write(", ");
            fax.PowerOff();
            Console.WriteLine(")");
        }
    }

    public void SendFax(in IDocument document)
    {
        if (fax.GetState() == IFax.State.On)
        {
            fax.SendFax(document);
            FaxCounter++;
        }
    }

    public void ReceiveFax(in IDocument document)
    {
        if (fax.GetState() == IFax.State.On)
        {
            fax.ReceiveFax(in document);
            FaxCounter++;
        }
    }
}

#region classFax
public class Fax : BaseDevice, IFax
{ 
    public override void PowerOn()
    {
        if (GetState() == IFax.State.Off)
        {
            state = IFax.State.On;
            Console.Write("Fax active");
        }
    }

    public override void PowerOff()
    {
        if (GetState() == IFax.State.On)
        {
            state = IFax.State.Off;
            Console.Write("Fax inactive");
        }
    }
    
    public void SendFax(in IDocument document)
    {
        string dateTimeString = DateTimeHelper.GetCurrentDateTimeString();
        
        Console.WriteLine($"{dateTimeString} Fax sent: {document.GetFileName()}");
    }
    
    public void ReceiveFax(in IDocument document)
    {
        string dateTimeString = DateTimeHelper.GetCurrentDateTimeString();
        
        Console.WriteLine($"{dateTimeString} Fax received: {document.GetFileName()}");
    }
}
#endregion