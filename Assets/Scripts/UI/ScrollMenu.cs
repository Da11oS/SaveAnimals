using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMenu : MonoBehaviour
{
    [SerializeField] public GameObject[] _prefabs;
    [SerializeField] private GameObject[] _panels;

    [SerializeField] private float _positionOffset;
    [SerializeField] private float _scaleOffset;
    [SerializeField] private float _snapSpeed;
    [SerializeField] private float _scaleCorrectSpeed;

    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector2[] _panelPositions;

    [SerializeField] private ScrollRect _scrollRect;

    private Vector2 _contectVector;
    private Vector2 _panelSize;

    private RectTransform _rectTransform;
    private int _currentPanelId = 0;
    private bool _isScroll;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _scrollRect = FindObjectOfType<ScrollRect>();
        _scrollRect.inertia = true;
    }
    void FixedUpdate()
    {
        FixCurrentPanel();
    }
    public void ClearPanels()
    {
        foreach(var child in _panels)
        {
#if UNITY_EDITOR
            DestroyImmediate(child.gameObject);
#else
            Destroy(child.gameObject);
#endif
        }
    }
    public void OnScrolling(bool value)
    {
        _isScroll = value;
        if(_isScroll)
        _scrollRect.inertia = true;
        
    }
    private void FixCurrentPanel()
    {
        if (_rectTransform.anchoredPosition.x >= _panelPositions[0].x 
            || _rectTransform.anchoredPosition.x <= _panelPositions[_panelPositions.Length - 1].x)
        { 
            _scrollRect.inertia = false;
        }
        float nearestPos = float.MaxValue;

        for (int i = 0; i < _panels.Length; i++)
        {
            float distance = GetPanelDistance(_panelPositions[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                _currentPanelId = i;
            }
            float scale = Mathf.Clamp(1 / (distance/ _positionOffset) * _scaleOffset, 0.5f , 1f);
            CorrectPanelsSize(_panels[i].transform, scale);
        }
        float velocity = Mathf.Abs(_scrollRect.velocity.x);
        if (velocity < 400 )
            _scrollRect.inertia = false;
        if (_isScroll || velocity > 400)
            return;
        CorrectPanelPosition();
    }

    private void CorrectPanelsSize(Transform panel, float scale)
    {
        float sizeX = Mathf.SmoothStep(panel.localScale.x, scale, _scaleCorrectSpeed * Time.fixedDeltaTime);
        float sizeY = Mathf.SmoothStep(panel.localScale.y, scale, _scaleCorrectSpeed * Time.fixedDeltaTime);
        panel.localScale = new Vector2(sizeX, sizeY);
    }
    private void CorrectPanelPosition()
    {
        _contectVector.x = Mathf.SmoothStep(_rectTransform.anchoredPosition.x, _panelPositions[_currentPanelId].x, _snapSpeed * Time.fixedDeltaTime);
        _rectTransform.anchoredPosition = _contectVector;
    }
    private float GetPanelDistance(float target)
    {
        return Mathf.Abs(_rectTransform.anchoredPosition.x - target);
    }
    public void SetPanels()
    {
        _panels = new GameObject[_prefabs.Length];
        _panelPositions = new Vector2[_prefabs.Length];
        SetPanel(out _panels[0], _prefabs[0]);
        _panels[0].transform.position = _startPosition;
        for (int i = 1; i < _panels.Length; i++)
        {
            SetPanel(out _panels[i], _prefabs[i]);
            print(_panelSize);
            SetPanelPosition(_panels[i - 1], _panels[i]);
            _panelPositions[i] = -_panels[i].transform.localPosition;
            
        }
    }
    private void SetPanelPosition(GameObject lastPanel, GameObject panel)
    {
        float x = lastPanel.transform.localPosition.x + panel.GetComponent<RectTransform>().sizeDelta.x + _positionOffset;
        panel.transform.localPosition = new Vector2(x, lastPanel.transform.localPosition.y);   
    }
    private void SetPanel(out GameObject panel, GameObject prefab)
    {
        panel = Instantiate(prefab, Vector2.zero, prefab.transform.rotation, transform);
    }
}
