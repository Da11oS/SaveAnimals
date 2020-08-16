
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 RunDirection;
    public Vector2 JumpDirection;
    private Player _player;
    public static bool IsClick;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        IsClick = true;
        _player.Lerp(RunDirection);
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _player.Jump(JumpDirection);
        IsClick = false;
    }

}
