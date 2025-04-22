using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Variables
    private bool isCursorVisible = true;
    public static UIManager Instance { get; private set; }

    [SerializeField] private List<GameObject> uiPanels = new List<GameObject>();
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

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

    public bool IsAnyUIOpen
    {
        get
        {
            foreach (var panel in uiPanels)
            {
                if (panel != null && panel.activeInHierarchy)
                    return true;
            }
            return false;
        }
    }
}