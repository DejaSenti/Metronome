using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public const float NUM_SECONDS_IN_MINUTE = 60;
    private const float NUM_BEATS_IN_MEASURE = 4;

    public MetronomeData Data { get; private set; }

    [SerializeField]
    private AudioSource[] tickTock;

    private bool[] metronomeArray;
    private float periodTime;
    private double nextTick;

    private int tickTockIndex;

    private void Awake()
    {
        Data = new MetronomeData();
    }

    public void CreateMetronome()
    {
        periodTime = (NUM_BEATS_IN_MEASURE * NUM_SECONDS_IN_MINUTE) / (Data.Denominator * Data.Tempo);

        metronomeArray = new bool[Data.Numerator];

        int index = 0;
        metronomeArray[index] = true;

        if (Data.Subdivisions == null || Data.Subdivisions.Sum(d => d) != Data.Numerator)
            return;

        for (int i = 0; i < Data.Subdivisions.Count - 1; i++)
        {
            index += Data.Subdivisions[i];
            metronomeArray[index] = true;
        }

        tickTockIndex = 0;
    }

    private void OnEnable()
    {
        var audioIndex = Convert.ToInt32(metronomeArray[tickTockIndex]);
        tickTock[audioIndex].Play();

        ScheduleTickTock();
    }

    private void Update()
    {
        if (AudioSettings.dspTime >= nextTick)
        {
            ScheduleTickTock();
        }
    }

    private void OnDisable()
    {
        foreach (var source in tickTock)
        {
            source.Stop();
        }

        tickTockIndex = 0;
    }

    private void ScheduleTickTock()
    {
        nextTick = AudioSettings.dspTime + periodTime;

        tickTockIndex++;
        if (tickTockIndex >= metronomeArray.Length)
            tickTockIndex = 0;

        var audioIndex = Convert.ToInt32(metronomeArray[tickTockIndex]);

        tickTock[audioIndex].PlayScheduled(nextTick);
    }
}
