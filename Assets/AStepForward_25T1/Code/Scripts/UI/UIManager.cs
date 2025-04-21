using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool isCursorVisible = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ToggleCursorPresence(!isCursorVisible);
        }
    }

    private void ToggleCursorPresence(bool cursorVisibility)
    {
        Cursor.visible = cursorVisibility;
        isCursorVisible = cursorVisibility;
    }
}