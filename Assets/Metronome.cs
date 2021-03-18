using System;
using System.Linq;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public const float NUM_SECONDS_IN_MINUTE = 60;
    private const float NUM_BEATS_IN_MEASURE = 4;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] tickTock;

    private bool[] metronomeArray;
    private float periodTime;

    private int index;

    public MetronomeData Data { get; private set; }

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
        index = 0;
        InvokeRepeating("TickTock", 0, periodTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void TickTock()
    {
        var audioIndex = Convert.ToInt32(metronomeArray[index]);
        source.PlayOneShot(tickTock[audioIndex]);

        index++;

        if (index >= metronomeArray.Length)
            index = 0;
    }
}
