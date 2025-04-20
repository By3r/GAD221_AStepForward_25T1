using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogueLines")]
public class NPCDialogueLines : ScriptableObject
{
    public List<string> lines = new List<string>();

    public List<string> translatedLines = new List<string>();

    public List<AudioClip> audioClips = new List<AudioClip>();
}