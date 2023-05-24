using Zadanie1;

namespace Zadanie2;

public interface IFax : IDevice
{
    void SendFax(in IDocument document);
    
    void ReceiveFax(in IDocument document);
}