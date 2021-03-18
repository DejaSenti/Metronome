using UnityEngine;

[CreateAssetMenu(fileName = "Subdivision Validator", menuName = "Subdivision Validator")]
public class SubdivisionValidator : TMPro.TMP_InputValidator
{
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (char.IsNumber(ch) || ch == '+' && pos > 0 && text[pos - 1] != '+')
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