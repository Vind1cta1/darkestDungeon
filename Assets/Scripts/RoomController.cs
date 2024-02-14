using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomController : MonoBehaviour
{
    public List<RoomController> neighbours = new List<RoomController>();
    public event Action<RoomController> OnRoomClicked;
    public Vector3 position;
    public bool isVisited = false;
    public bool isAccessible = false;

    private void Update()
    {
        if (isVisited)
        {
            foreach (var room in neighbours)
            {
                room.isAccessible = true;
            }
        }
    }
    public void OnRoomClickedEvent(BaseEventData eventData)
    { 
        RoomController clickedRoom = eventData.selectedObject.GetComponent<RoomController>();

        if (clickedRoom != null)
        {
            OnRoomClicked?.Invoke(clickedRoom);
        }
    }

    public void MarkAsVisited()
    {
        isVisited = true;
    }

    public void SetPosition(Vector3 pos)
    {
        position = pos;
    }

    public void AddNeighbour(RoomController neighbour)
    {
        neighbours.Add(neighbour);
    }
}
