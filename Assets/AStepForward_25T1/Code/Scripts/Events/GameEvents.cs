using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<Sentences> OnTaskStarted;
    public static Action<Sentences> OnTimerExpired;
    public static Action<Sentences> OnTaskCompleted;
    public static Action<Sentences> OnTaskFailed;
    public static Action<Sentences> OnTimerStarted;
    public static Action<Sentences> OnTaskRetryRequested;

    public static Action<int> OnDayChanged;
    public static Action<int> OnNewDayStarted;
    public static Action<int> OnTaskProgressUpdated;

    public static Action<bool> OnTogglePlayerMovement;
}