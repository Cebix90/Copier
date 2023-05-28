namespace Zadanie5;

public interface IDevice
{
    enum State {On, Off, Standby};

    abstract protected void SetState(State state);
    State GetState(); // zwraca aktualny stan urządzenia
    void PowerOn() // uruchamia urządzenie, zmienia stan na `on`
    {
        SetState(State.On);
    }

    void PowerOff() // wyłącza urządzenie, zmienia stan na `off
    {
        SetState(State.Off);
    }

    void StandbyOn() // wprowadza urządzenie w stan "standby"
    {
        SetState(State.Standby);
    }

    void StandbyOff() // zmienia spowrotem stan urzadzenia ze "standby" na "on"
    {
        SetState(State.On);
    }
    // int Counter {get;}  // zwraca liczbę charakteryzującą eksploatację urządzenia,
    // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
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