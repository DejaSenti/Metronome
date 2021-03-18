using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class View : MonoBehaviour
{
    private const int DEFAULT_DENOM_INDEX = 1;

    [SerializeField]
    private TMP_InputField tempoField;
    [SerializeField]
    private TMP_InputField numerator;
    [SerializeField]
    private TMP_Dropdown denominator;
    [SerializeField]
    private TMP_InputField subdivisions;
    [SerializeField]
    private Slider tempoSlider;
    [SerializeField]
    private TMP_Text playButton;
    [SerializeField]
    private GameObject subsHelpOverlay;
    [SerializeField]
    private Button subsHelpOverlayButton;

    [SerializeField]
    private Controller controller;

    public void OnTempoTextChange()
    {
        ushort tempo;

        try
        {
            tempo = ushort.Parse(tempoField.text);
        }
        catch
        {
            tempo = MetronomeData.DEFAULT_TEMPO;
        }

        UpdateTempoSlider(tempo);

        controller.UpdateMetronomeData(tempo, MetronomeProperties.Tempo);
    }

    public void OnTempoSliderChange()
    {
        ushort tempo = (ushort)tempoSlider.value;
        UpdateTempoField(tempo);

        controller.UpdateMetronomeData(tempo, MetronomeProperties.Tempo);
    }

    public void OnNumeratorChange()
    {
        ushort numer;

        try
        {
            numer = ushort.Parse(numerator.text);
        }
        catch
        {
            numer = MetronomeData.DEFAULT_NUMERATOR;
        }

        controller.UpdateMetronomeData(numer, MetronomeProperties.Numerator);
    }

    public void OnDenominatorChange()
    {
        ushort denom = ushort.Parse(denominator.options[denominator.value].text);

        controller.UpdateMetronomeData(denom, MetronomeProperties.Denominator);
    }

    public void OnSubdivisionChange()
    {
        List<ushort> subs = null;

        if (subdivisions.text.Length > 0)
        {
            subs = GetSubdivisionsFromTextField(subdivisions.text);
        }

        controller.UpdateMetronomeData(subs, MetronomeProperties.Subdivisions);
    }

    public void UpdatePlayButtonDisplay(bool status)
    {
        if (status)
        {
            playButton.text = "Stop";
        }
        else
        {
            playButton.text = "Play";
        }
    }

    public void UpdateTempoField(ushort value)
    {
        tempoField.text = value.ToString();
    }

    public void UpdateTempoSlider(ushort tempo)
    {
        if (tempo > tempoSlider.maxValue || tempo < tempoSlider.minValue)
            return;

        tempoSlider.value = tempo;
    }

    public void UpdateTempoView(ushort tempo)
    {
        UpdateTempoSlider(tempo);
        UpdateTempoField(tempo);
    }

    public void UpdateMetronomeView(MetronomeData data)
    {
        UpdateTempoView(data.Tempo);

        numerator.text = data.Numerator.ToString();

        var denomIndex = denominator.options.FindIndex(x => x.text == data.Denominator.ToString());
        denominator.value = denomIndex == -1 ? DEFAULT_DENOM_INDEX : denomIndex;

        subdivisions.text = GetTextFromSubdivisions(data.Subdivisions);
    }

    private string GetTextFromSubdivisions(List<ushort> subs)
    {
        string result = "";

        for (int i = 0; i < subs.Count; i++)
        {
            result += subs[i].ToString();

            if (i < subs.Count - 1)
            {
                result += '+';
            }
        }

        return result;
    }

    private List<ushort> GetSubdivisionsFromTextField(string text)
    {
        while (!char.IsDigit(text[text.Length - 1]))
        {
            text = text.Remove(text.Length - 1);
        }

        var length = text.Length - text.Replace("+", "").Length + 1;
        var result = new List<ushort>(length);

        string currentNum = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsDigit(text[i]))
            {
                currentNum += text[i];
            }
            else
            {
                result.Add(ushort.Parse(currentNum));
                currentNum = "";
            }
        }
        result.Add(ushort.Parse(currentNum));

        return result;
    }

    public void SetSubsHelpOverlay(bool status)
    {
        subsHelpOverlayButton.interactable = !status;
        subsHelpOverlay.SetActive(status);
    }
}