using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ChestController chestController;
    public GameObject roomUI;
    public GameObject BGCorridor;
    public GameObject chest;
    public GameObject roomPrefab;
    public GameObject pathPrefab;
    public List<GameObject> roomBackGround;
    public List<Vector3> predefinedPoints;
    public Transform map;
    public int numberOfRooms;
    public float minDistanceBetweenRooms;
    public float minRoomTime;
    public float maxRoomTime;

    private RoomController currentRoom;
    private List<RoomController> createdRooms = new List<RoomController>();
    private Coroutine roomCoroutine;
    private int selectBackGround;
    private bool isTransitioning = false;
    private bool isRoomBackgroundRandomized = false;

    private void Start()
    {
        chestController = FindObjectOfType<ChestController>();
        chest = chestController.gameObject;
        selectBackGround = UnityEngine.Random.Range(0, roomBackGround.Count);
        foreach (var backGround in roomBackGround)
        {
            backGround.SetActive(false);
        }
        roomBackGround[selectBackGround].SetActive(true);
        if (map != null)
        {
            GenerateMap();
        }
        foreach (var room in createdRooms)
        {
            room.OnRoomClicked += HandleRoomClicked;
        }
    }

    private void OnDisable()
    {
        foreach (var room in createdRooms)
        {
            room.OnRoomClicked -= HandleRoomClicked;
        }
    }

    private void Update()
    {
        if (!chestController.isInRoom && !chestController.isOpen && !isTransitioning)
        {
            if (IsChestOutOfCamera())
            {
                isTransitioning = true;
                StartCoroutine(SpawnRoomAfterDelay());
            }
        }
    }

    public void IsRoomButtonPressed()
    {
        if (roomCoroutine != null)
        {
            StopCoroutine(roomCoroutine);
        }
        //if (currentRoom != null && currentRoom.isAccessible)
        //{
            chestController.isInRoom = false;
            BGCorridor.SetActive(true);
            roomUI.SetActive(false);
            isTransitioning = true;
            isRoomBackgroundRandomized = false;
            roomCoroutine = StartCoroutine(SpawnRoomAfterDelay());
        //}
    }

    private IEnumerator SpawnRoomAfterDelay()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minRoomTime, maxRoomTime));

        if (!chestController.isOpen && IsChestOutOfCamera())
        {
            if (!isRoomBackgroundRandomized)
            {
                selectBackGround = UnityEngine.Random.Range(0, roomBackGround.Count);
                foreach (var backGround in roomBackGround)
                {
                    backGround.SetActive(false);
                }
                roomBackGround[selectBackGround].SetActive(true);
                isRoomBackgroundRandomized = true;
            }
            chest.transform.position = new Vector3(-10f, -2f, -1f);
            chestController.isInRoom = true;
            roomUI.SetActive(true);
            BGCorridor.SetActive(false);
        }

        isTransitioning = false;

        if (!chestController.isOpen && IsChestOutOfCamera())
        {
            StartCoroutine(SpawnRoomAfterDelay());
        }
    }

    private bool IsChestOutOfCamera()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(chest.transform.position);
        return viewPos.x < 0f;
    }

    void GenerateMap()
    {
        List<Vector3> availablePoints = new List<Vector3>(predefinedPoints);

        for (int i = 0; i < numberOfRooms; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availablePoints.Count);
            Vector3 randomPredefinedPosition = availablePoints[randomIndex];
            Vector3 randomWorldPosition = map.transform.TransformPoint(randomPredefinedPosition) + new Vector3(280f, -110f, 0f);
            GameObject newRoom = Instantiate(roomPrefab, randomWorldPosition, Quaternion.identity, map.transform);
            RoomController newRoomController = newRoom.GetComponent<RoomController>();
            newRoomController.SetPosition(randomWorldPosition);
            createdRooms.Add(newRoomController);
            RemoveUsedPoint(randomIndex, ref availablePoints);
        }

        createdRooms.Sort((a, b) => a.position.x.CompareTo(b.position.x));
        currentRoom = createdRooms[0];
        currentRoom.MarkAsVisited();

        for (int i = 0; i < createdRooms.Count; i++)
        {
            for (int j = i + 1; j < createdRooms.Count; j++)
            {
                float distanceX = Mathf.Abs(createdRooms[i].transform.position.x - createdRooms[j].transform.position.x);
                float distanceY = Mathf.Abs(createdRooms[i].transform.position.y - createdRooms[j].transform.position.y);
                if ((distanceX < 200f && Mathf.Approximately(distanceY, 0f)) || (distanceY < 200f && Mathf.Approximately(distanceX, 0f)))
                {

                    if (distanceX > 0 && distanceY > 0)
                    {
                        continue;
                    }
                    createdRooms[i].GetComponent<RoomController>().AddNeighbour(createdRooms[j].GetComponent<RoomController>());
                    createdRooms[j].GetComponent<RoomController>().AddNeighbour(createdRooms[i].GetComponent<RoomController>());

                    Vector3 midpoint = (createdRooms[i].transform.position + createdRooms[j].transform.position) / 2f - new Vector3(3f, 0f, 0f);

                    if (distanceX > 0)
                    {
                        CreatePath(createdRooms[i].transform.position, createdRooms[j].transform.position, midpoint, true);
                    }
                    else if (distanceY > 0)
                    {
                        CreatePath(createdRooms[i].transform.position, createdRooms[j].transform.position, midpoint, false);
                    }
                }
            }
        }
    }

    void CreatePath(Vector3 room1Position, Vector3 room2Position, Vector3 midpoint, bool isHorizontal)
    {
        GameObject path = Instantiate(pathPrefab, midpoint, Quaternion.identity, map.transform);

        float distanceX = Mathf.Abs(room1Position.x - room2Position.x);
        float distanceY = Mathf.Abs(room1Position.y - room2Position.y);

        if (isHorizontal)
        {
            path.transform.localScale = new Vector3(distanceX / 130f, 1f, 1f);
        }
        else
        {
            path.transform.localScale = new Vector3(0.5f, distanceY / 100f, 1f);
        }
    }

    void RemoveUsedPoint(int index, ref List<Vector3> pointList)
    {
        pointList.RemoveAt(index);
    }

    private void HandleRoomClicked(RoomController room)
    {
        currentRoom = room;
    }
}
