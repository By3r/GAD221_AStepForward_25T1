using System.Collections;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject tooltipCanvas;

    [Header("Tooltip Settings")]
    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private float displayTime = 2f;

    private Coroutine currentRoutine;
    #endregion

    private void Awake()
    {
        tooltipCanvas?.SetActive(false);
    }

    public void ShowTooltip(string message)
    {
        if (tooltipText != null)
            tooltipText.text = message;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowAndHideCanvas());
    }

    private IEnumerator ShowAndHideCanvas()
    {
        tooltipCanvas?.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        tooltipCanvas?.SetActive(false);
    }
}