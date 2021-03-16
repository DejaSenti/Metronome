using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class View : MonoBehaviour
{
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
    private Controller controller;

    private MetronomeData metronomeData;

    private void Awake()
    {
        if (metronomeData == null)
        {
            metronomeData = new MetronomeData();
        }

        UpdateMetronomeData();
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

    public void UpdateTempoField()
    {
        tempoField.text = tempoSlider.value.ToString();
    }

    public void UpdateTempoSlider()
    {
        ushort value = ushort.Parse(tempoField.text);

        if (value > tempoSlider.maxValue || value < tempoSlider.minValue)
            return;

        tempoSlider.value = value;
    }

    public void UpdateMetronomeData()
    {
        var tempo = ushort.Parse(tempoField.text);
        var numer = ushort.Parse(numerator.text);
        var denom = ushort.Parse(denominator.options[denominator.value].text);
        List<ushort> subs = Enumerable.Repeat((ushort)1, numer).ToList();

        if (subdivisions.text.Length > 0)
        {
            var subsFromField = GetSubdivisionsFromTextField(subdivisions.text);
            if (subsFromField.Sum(x => x) == numer)
            {
                subs = subsFromField;
            }
        }

        metronomeData.UpdateData(tempo, numer, denom, subs);

        controller.UpdateModelData(metronomeData);
    }

    private List<ushort> GetSubdivisionsFromTextField(string text)
    {
        while (!char.IsDigit(text[text.Length - 1]))
        {
            text.Remove(text.Length - 1);
        }

        var length = text.Length - text.Replace("+", "").Length + 1;
        var result = new List<ushort>(length);

        int count = 0;
        string currentNum = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsDigit(text[i]))
            {
                currentNum += text[i];
            }
            else
            {
                result[count] = ushort.Parse(currentNum);
                count++;
                currentNum = "";
            }
        }

        return result;
    }
}