using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public const float NUM_SECONDS_IN_MINUTE = 60;
    private const float NUM_BEATS_IN_MEASURE = 4;

    public MetronomeData Data { get; private set; }

    private IEnumerator tickTockCoroutine;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] tickTock;

    private bool[] metronomeArray;
    private float periodTime;

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
    }

    private void OnEnable()
    {
        tickTockCoroutine = WaitAndTickTock(periodTime);
        StartCoroutine(tickTockCoroutine);
    }

    private void OnDisable()
    {
        StopCoroutine(tickTockCoroutine);
        tickTockIndex = 0;
    }

    private IEnumerator WaitAndTickTock(float periodTime)
    {
        while (true)
        {
            TickTock();
            yield return new WaitForSecondsRealtime(periodTime);
        }
    }

    private void TickTock()
    {
        var audioIndex = Convert.ToInt32(metronomeArray[tickTockIndex]);
        source.PlayOneShot(tickTock[audioIndex]);

        tickTockIndex++;

        if (tickTockIndex >= metronomeArray.Length)
            tickTockIndex = 0;
    }
}
