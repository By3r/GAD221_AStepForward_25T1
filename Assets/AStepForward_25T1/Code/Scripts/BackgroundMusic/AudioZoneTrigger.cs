using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZoneTrigger : MonoBehaviour
{
    public AudioZoneData zoneData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayNewMusic(
                zoneData.zoneMusic,
                zoneData.volume
            );
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && zoneData.revertOnExit)
        {
            AudioManager.Instance.PlayDefaultMusic();
        }
    }
}
