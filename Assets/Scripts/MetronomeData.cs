using System.Collections.Generic;

public class MetronomeData
{
    public const ushort DEFAULT_TEMPO = 100;
    public const ushort DEFAULT_NUMERATOR = 4;
    private const ushort DEFAULT_DENOMINATOR = 4;
    private readonly List<ushort> DEFAULT_SUBDIVISIONS = new List<ushort> { DEFAULT_NUMERATOR };

    public ushort Tempo;
    public ushort Numerator;
    public ushort Denominator;
    public List<ushort> Subdivisions;

    public MetronomeData()
    {
        Tempo = DEFAULT_TEMPO;
        Numerator = DEFAULT_NUMERATOR;
        Denominator = DEFAULT_DENOMINATOR;
        Subdivisions = DEFAULT_SUBDIVISIONS;
    }
}
