using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioZone", menuName = "Audio/Audio Zone")]
public class AudioZoneData : ScriptableObject 
{
    [Header("Music Settings")]
    public AudioClip zoneMusic;       // Music to play in this zone
    [Range(0, 1)] public float volume = 0.7f;

    [Header("Zone Behavior")]
    public bool revertOnExit = true; // Return to default music when leaving
}
