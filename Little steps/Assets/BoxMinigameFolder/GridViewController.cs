using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridViewController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static DraggedDirection? LastDraggedDirection { get; private set; } = null;

    public enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        LastDraggedDirection = GetDragDirection(dragVectorDirection);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Required by the interface but not used.
    }

    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        if (positiveX > positiveY)
        {
            return dragVector.x > 0 ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            return dragVector.y > 0 ? DraggedDirection.Up : DraggedDirection.Down;
        }
    }

    public static void ResetDragDirection()
    {
        LastDraggedDirection = null;
    }
}