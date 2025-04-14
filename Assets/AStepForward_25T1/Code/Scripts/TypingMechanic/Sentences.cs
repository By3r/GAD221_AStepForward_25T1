using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class stores sentences to write per NPC/Task interaction. 
/// </summary>

[CreateAssetMenu(fileName = "WhatTriggeredTheTask?", menuName = "Sentences/SentencesToWrite")]
public class Sentences : ScriptableObject
{
    public float timeLimitInMinutes = 1f;
    [Tooltip("Write all sentences you want the player to copy letter by letter.")]
    public List<string> sentencesToType = new List<string>();

    [Tooltip("Translations of those sentences (same order).")]
    public List<string> translatedSentences = new List<string>();
}
