using UnityEngine;

public class UmlautCharConverter : MonoBehaviour
{
    public char? GetUmlautCharacter()
    {
        if (!Input.GetKey(KeyCode.Tab))
            return null;

        if (Input.GetKeyDown(KeyCode.A)) return '�';
        if (Input.GetKeyDown(KeyCode.O)) return '�';
        if (Input.GetKeyDown(KeyCode.U)) return '�';
        if (Input.GetKeyDown(KeyCode.S)) return '�';

        if (Input.GetKey(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.A)) return '�';
            if (Input.GetKeyDown(KeyCode.O)) return '�';
            if (Input.GetKeyDown(KeyCode.U)) return '�';
        }

        return null;
    }
}