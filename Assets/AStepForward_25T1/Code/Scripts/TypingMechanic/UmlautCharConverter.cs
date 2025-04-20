using UnityEngine;

public class UmlautCharConverter : MonoBehaviour
{
    public char? GetUmlautCharacter()
    {
        if (!Input.GetKey(KeyCode.Tab))
            return null;

        if (Input.GetKeyDown(KeyCode.A)) return 'ä';
        if (Input.GetKeyDown(KeyCode.O)) return 'ö';
        if (Input.GetKeyDown(KeyCode.U)) return 'ü';
        if (Input.GetKeyDown(KeyCode.S)) return 'ß';

        if (Input.GetKey(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.A)) return 'Ä';
            if (Input.GetKeyDown(KeyCode.O)) return 'Ö';
            if (Input.GetKeyDown(KeyCode.U)) return 'Ü';
        }

        return null;
    }
}