using UnityEngine;

/// <summary>
/// Controls how the cursor should look like based on the layer the cursor icon is related to.
/// </summary>

#region Scriptable Object
[CreateAssetMenu (fileName = "CursorIconType", menuName = "CustomCursor/CursorIcon")]
public class CursorSpriteChangeLayers : ScriptableObject
{
    [Tooltip("What layer should have change the cursor look to the one you have assigned.")]
    public int layer;
    [Tooltip("Place the sprite the cursor should turn into when interacting with the layer.")]
    public Sprite cursorIcon;
}
#endregion