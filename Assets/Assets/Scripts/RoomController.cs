using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<RoomController> neighbours = new List<RoomController>();
    public event Action<RoomController> OnRoomClicked;
    public bool isVisited = false;
    public bool isAccessible = false;
    public Vector3 position;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Room");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                RoomController clickedRoom = hit.collider.GetComponent<RoomController>();
                Debug.Log(clickedRoom);
                if (clickedRoom != null)
                {
                    Debug.Log("Room Clicked!");
                    OnRoomClicked?.Invoke(clickedRoom);
                }
            }
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
        if(isVisited)
        {
            neighbour.isAccessible = true;
        }
        neighbours.Add(neighbour);
    }
}
