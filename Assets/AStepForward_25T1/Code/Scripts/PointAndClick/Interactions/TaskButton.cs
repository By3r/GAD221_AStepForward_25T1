using UnityEngine;

public class TaskButton : MonoBehaviour
{
    [SerializeField] private SentenceTriggerer sentenceTriggerer;

    private void OnMouseDown()
    {
        if (sentenceTriggerer != null)
        {
            sentenceTriggerer.LoadSentences();
            Debug.Log("Task started manually!");
        }
    }
}