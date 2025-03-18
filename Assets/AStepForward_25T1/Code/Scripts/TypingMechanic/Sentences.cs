using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class stores sentences to write per NPC/Task interaction. 
/// </summary>

#region Scriptable Object
[CreateAssetMenu(fileName = "WhatTriggeredTheTask?", menuName = "Sentences/SentencesToWrite")]
public class Sentences : ScriptableObject
{
    [Tooltip("Write all sentences you want the player to copy letter by letter.")]
    public List<string> sentencesToType = new List<string>();
    [Tooltip("Should the background change or stay the same? Set whatever you want the background at a specific sentence display.")]
    public List<Image> sentenceBackground = new List<Image>();
}
#endregion