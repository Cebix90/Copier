namespace Zadanie1;

public interface IDevice
{
    enum State {On, Off};

    void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
    void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
    State GetState(); // zwraca aktualny stan urządzenia

    int Counter {get;}  // zwraca liczbę charakteryzującą eksploatację urządzenia,
    // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
}

public abstract class BaseDevice : IDevice
{
    protected IDevice.State state = IDevice.State.Off;
    public IDevice.State GetState() => state;

    public virtual void PowerOff()
    {
        state = IDevice.State.Off;
        Console.WriteLine("... Device is off !");
    }

    public virtual void PowerOn()
    {
        state = IDevice.State.On;
        Console.WriteLine("Device is on ...");  
    }

    public virtual int Counter { get; protected set; } = 0;
}


public interface IPrinter : IDevice
{
    /// <summary>
    /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
    /// </summary>
    /// <param name="document">obiekt typu IDocument, różny od `null`</param>
    void Print(in IDocument document);
}

public interface IScanner : IDevice
{
    // dokument jest skanowany, jeśli urządzenie włączone
    // w przeciwnym przypadku nic się dzieje
    void Scan(out IDocument document, IDocument.FormatType? formatType = null);
}