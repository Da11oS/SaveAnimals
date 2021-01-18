using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Vector3 LeftBottom { get => FindObjectOfType<LeftBottomBorder>().transform.position; }
    public Vector3 RightUpper { get => FindObjectOfType<RightUpperBorder>().transform.position; }
    public Vector3 RightBottom { get => new Vector3(RightUpper.x, LeftBottom.y, RightUpper.z); }
    public Vector3 LeftUpper { get => new Vector3(LeftBottom.x, RightUpper.y, RightUpper.z); }
    private EdgeCollider2D _collider;
    private void OnDrawGizmos()
    {
        if (LeftBottom != null && RightUpper != null)
            DrawBorders();
        
    }
    private void DrawBorders()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(LeftBottom, RightBottom);
        Gizmos.DrawLine(RightBottom, RightUpper);
        Gizmos.DrawLine(RightUpper, LeftUpper);
        Gizmos.DrawLine(LeftUpper, LeftBottom);
    }
    private void Start()
    {
        GenerateCollider();
    }
    private void GenerateCollider()
    {
        _collider = GetComponent<EdgeCollider2D>();
        var pos = FindObjectOfType<LeftBottomBorder>().transform.localPosition;
        var height = LeftUpper - LeftBottom;
        var width = RightBottom - LeftBottom;
        _collider.points = new Vector2[]
        {
            pos,
            pos + height,
            pos + width + height,
            pos + width,
            pos
        };

    }
}
