using System.Collections.Generic;

public class MetronomeData
{
    public ushort Tempo;
    public ushort Numerator;
    public ushort Denominator;
    public List<ushort> Subdivisions;

    public void UpdateData(ushort tempo, ushort numer, ushort denom, List<ushort> subs)
    {
        Tempo = tempo;
        Numerator = numer;
        Denominator = denom;
        Subdivisions = subs;
    }
}
