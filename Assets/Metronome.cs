using UnityEngine;

public class Metronome : MonoBehaviour
{
    private int NUM_SECONDS_IN_MINUTE = 60;
    private int NUM_BEATS_IN_MEASURE = 4;

    private bool[] metronomeArray;
    private float periodTime;

    private int index;

    public void CreateMetronome(MetronomeData data)
    {
        periodTime = (NUM_BEATS_IN_MEASURE * NUM_SECONDS_IN_MINUTE) / (data.Denominator * data.Tempo);

        metronomeArray = new bool[data.Numerator];

        int index = 0;
        metronomeArray[index] = true;
        for (int i = 0; i < data.Subdivisions.Count - 1; i++)
        {
            index += data.Subdivisions[i];
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
        if (metronomeArray[index])
        {
            Debug.Log("Tick");
        }
        else
        {
            Debug.Log("Tock");
        }

        index++;

        if (index >= metronomeArray.Length)
            index = 0;
    }
}
