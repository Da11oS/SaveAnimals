using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketWarning : MonoBehaviour
{
    private Canvas _canvas;
    private GameObject _father;
    private RectTransform _rectTransform;
    private RectTransform _canvasRect;
    private CustomCamera _camera;
    private GameObject _rocket;
    public void SetPosition()
    {

        Vector2 relativePosition = GetRocketPositionRelativeCamera();
        Vector2 direction = relativePosition / relativePosition.magnitude;
        Vector3 position = Vector3.one;
        float offset = 60;
        position.x = Mathf.Clamp(direction.x * _canvasRect.sizeDelta.x, -_canvasRect.sizeDelta.x / 2 + offset, _canvasRect.sizeDelta.x / 2 - offset);
        position.y = Mathf.Clamp(direction.y * _canvasRect.sizeDelta.y, -_canvasRect.sizeDelta.y / 2 + offset, _canvasRect.sizeDelta.y / 2 - offset);
        _rectTransform.localPosition = position;

    }
    public void SetParent(RectTransform parent)
    {
        transform.parent = parent;
    }
    public void SetFather(GameObject father)
    {
        _father = father;
    }
    public Vector2 GetRocketPositionRelativeCamera()
    {
        return _father.transform.position - _camera.transform.position;
    }
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _camera = FindObjectOfType<CustomCamera>();
        _canvas = FindObjectOfType<Canvas>();
        _rectTransform.anchorMax = Vector2.one / 2;
        _rectTransform.anchorMin = Vector2.one / 2;
        _canvasRect = _canvas.GetComponent<RectTransform>();
        SetParent(_canvasRect);

    }
}
