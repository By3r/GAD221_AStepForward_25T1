using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public void CancelTaskPanel(GameObject taskpanel)
    {
        taskpanel.SetActive(false);
    }
}
