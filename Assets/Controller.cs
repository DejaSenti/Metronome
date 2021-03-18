using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class Controller : MonoBehaviour
{
    private const float TAP_TIMEOUT_PERIOD = 2;
    private const int MAX_TEMPO_TAPS = 3;

    [SerializeField]
    private Metronome metronome;
    [SerializeField]
    private View view;

    private List<float> tapTimes;
    private Timer tapTimeout;

    private void Awake()
    {
        tapTimes = new List<float>();
        tapTimeout = GetComponent<Timer>();
        tapTimeout.ElapsedEvent.AddListener(OnTapTimeout);
    }

    private void Start()
    {
        view.UpdateMetronomeView(metronome.Data);
        metronome.CreateMetronome();
    }

    public void UpdateMetronomeData(object value, MetronomeProperties property)
    {
        if (metronome.enabled)
        {
            metronome.enabled = false;
            view.UpdatePlayButtonDisplay(metronome.enabled);
        }

        switch (property)
        {
            case MetronomeProperties.Tempo:
                metronome.Data.Tempo = (ushort)value;
                break;

            case MetronomeProperties.Numerator:
                metronome.Data.Numerator = (ushort)value;
                break;

            case MetronomeProperties.Denominator:
                metronome.Data.Denominator = (ushort)value;
                break;

            case MetronomeProperties.Subdivisions:
                metronome.Data.Subdivisions = (List<ushort>)value;
                break;

            default:
                return;
        }

        metronome.CreateMetronome();
    }

    public void OnPlayClick()
    {
        metronome.enabled = !metronome.enabled;
        view.UpdatePlayButtonDisplay(metronome.enabled);
    }

    public void OnTapClick()
    {
        tapTimes.Add(Time.time);

        if (tapTimes.Count > MAX_TEMPO_TAPS)
        {
            tapTimes.RemoveAt(0);
        }

        tapTimeout.ResetTimer();
        tapTimeout.StartTimer(TAP_TIMEOUT_PERIOD);

        if (tapTimes.Count > 1)
        {
            var tempo = CalculateMeanTempo();
            UpdateMetronomeData(tempo, MetronomeProperties.Tempo);
            view.UpdateTempoView(tempo);
        }
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    private ushort CalculateMeanTempo()
    {
        int numDiffs = tapTimes.Count - 1;
        float[] timeDiffs = new float[numDiffs];

        for (int i = 0; i < numDiffs; i++)
        {
            timeDiffs[i] = tapTimes[i + 1] - tapTimes[i];
        }

        var meanDiff = timeDiffs.Average();
        var frequency = Metronome.NUM_SECONDS_IN_MINUTE / meanDiff;
        var result = (ushort)frequency;

        return result;
    }

    private void OnTapTimeout()
    {
        tapTimes.Clear();
    }
}