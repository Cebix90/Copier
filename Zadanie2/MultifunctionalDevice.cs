using Zadanie1;

namespace Zadanie2;

public class MultifunctionalDevice : Copier
{
    public int FaxCounter { get; private set; } = 0;
    
    public void SendFax(in IDocument document)
    {
        if (GetState() == IDevice.State.On)
        {
            Console.WriteLine($"{data} {time} Fax sent: {document.GetFileName()}");
            FaxCounter++;
        }
    }
    
    public void ReceiveFax(in IDocument document)
    {
        if (GetState() == IDevice.State.On)
        {
            Console.WriteLine($"{data} {time} Fax received: {document.GetFileName()}");
            FaxCounter++;
        }
    }
}