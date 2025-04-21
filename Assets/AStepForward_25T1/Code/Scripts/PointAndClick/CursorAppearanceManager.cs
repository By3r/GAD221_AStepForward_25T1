using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorAppearanceManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Camera mainSceneCamera;
    [SerializeField] private Image cursorImage;
    [SerializeField] private List<CursorSpriteChangeLayers> cursorSpriteChangeLayers;

    private Dictionary<int, Sprite> _cursorDictionary;
    private Sprite _defaultCursor;
    private int _currentLayer = -1;
    #endregion

    private void Start()
    {
        InitialiseTheCursorsDictionary();
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateCursorAppearance();
    }

    #region Private Functions
    private void InitialiseTheCursorsDictionary()
    {
        _cursorDictionary = new Dictionary<int, Sprite>();
        foreach (CursorSpriteChangeLayers cursorLayer in cursorSpriteChangeLayers)
        {
            _cursorDictionary[cursorLayer.layer] = cursorLayer.cursorIcon;
        }

        _defaultCursor = cursorImage.sprite;
    }

    private void UpdateCursorAppearance()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            if (_currentLayer != -2) 
            {
                cursorImage.sprite = _defaultCursor;
                _currentLayer = -2;
            }

            cursorImage.transform.position = Input.mousePosition;
            return;
        }

        Ray ray = mainSceneCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer != _currentLayer)
            {
                _currentLayer = hitLayer;

                if (_cursorDictionary.ContainsKey(hitLayer))
                {
                    cursorImage.sprite = _cursorDictionary[hitLayer];
                }
                else
                {
                    cursorImage.sprite = _defaultCursor;
                }
            }
        }
        else
        {
            if (_currentLayer != -1)
            {
                cursorImage.sprite = _defaultCursor;
                _currentLayer = -1;
            }
        }

        cursorImage.transform.position = Input.mousePosition;
    }
    #endregion
}