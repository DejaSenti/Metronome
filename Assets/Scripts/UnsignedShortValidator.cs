using UnityEngine;

[CreateAssetMenu(fileName = "Unsigned Short Validator", menuName = "Unsigned Short Validator")]
public class UnsignedShortValidator : TMPro.TMP_InputValidator
{
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (char.IsNumber(ch) && pos < 3)
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