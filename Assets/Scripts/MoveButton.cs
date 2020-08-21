
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
        _player.LerpRunDirection(RunDirection);
        _player.LerpRotation(new Quaternion(0, 0, 0, 0));
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
      
        _player.Jump(JumpDirection);
        IsClick = false;
    }

}
