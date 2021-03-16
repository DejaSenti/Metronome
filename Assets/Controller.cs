using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private Metronome metronome;
    [SerializeField]
    private View view;

    public void UpdateModelData(MetronomeData data)
    {
        if (metronome.enabled)
        {
            metronome.enabled = false;
            view.UpdatePlayButtonDisplay(metronome.enabled);
        }

        metronome.CreateMetronome(data);
    }

    public void OnPlayClick()
    {
        metronome.enabled = !metronome.enabled;
        view.UpdatePlayButtonDisplay(metronome.enabled);
    }
}
