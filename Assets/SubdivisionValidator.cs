using UnityEngine;

[CreateAssetMenu(fileName = "Input Field Validator", menuName = "Input Field Validator")]
public class SubdivisionValidator : TMPro.TMP_InputValidator
{
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (char.IsNumber(ch) || ch == '+' && pos > 0)
        {
#if UNITY_EDITOR
            text = text.Insert(pos, ch.ToString());
#endif
            pos++;

            return ch;
        }
        else
        {
            return '\0';
        }
    }
}