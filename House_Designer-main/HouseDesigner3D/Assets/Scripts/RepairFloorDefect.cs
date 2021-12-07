using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairFloorDefect : MonoBehaviour
{
    TrashClean trashClean;
    LocationsToMove locationsToMove;

    [SerializeField] List<GameObject> floorDefects;
    [SerializeField] List<GameObject> newFloors;
    int floorDefectIndex = 0;
    [SerializeField] GameObject player;
    [SerializeField] GameObject newFloor;
    [SerializeField] GameObject repairTool;

    Vector3 floorOffset = new Vector3(1, -0.4f, -1);
    Vector3 repairtToolOffset = new Vector3(-1, -0.4f, -1);

    int hitsRemain = 5;
    bool created = false;
    bool newFloorPlaced = false;
    bool isMoved = false;
    void Start()
    {
        repairTool = Instantiate(repairTool, player.transform.position + repairtToolOffset, player.transform.rotation) as GameObject;
        repairTool.SetActive(false);
        trashClean = FindObjectOfType<TrashClean>();
        locationsToMove = FindObjectOfType<LocationsToMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitsRemain <= 0 && !isMoved)
        {
            Destroy(newFloors[floorDefectIndex]);
            floorDefectIndex++;
            repairTool.SetActive(false);
            locationsToMove.PlayConfetti();
            locationsToMove.MoveToNextLocation(2f);
            isMoved = true;
            newFloorPlaced = false;
            created = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.transform != null)
                {
                    if (locationsToMove.GetIndex() > 0)
                    {
                        if (hit.transform.gameObject.tag == "FloorDefect" && locationsToMove.locations[locationsToMove.GetIndex()].CompareTag("FloorDefectWaypoint"))
                        {
                            CreateObjectsToReparirFloor();
                            isMoved = false;
                            hitsRemain = 5;
                        }
                        if (hit.transform.gameObject.tag == "NewFloor" && newFloorPlaced && created && hitsRemain > 0)
                        {
                            repairToolAnimation();
                        }
                    }
                }
        }
    }

    void CreateObjectsToReparirFloor()
    {
        if (!created)
        {
            repairTool.SetActive(true);
            repairTool.transform.position = player.transform.position + repairtToolOffset;
            newFloors.Add(Instantiate(newFloor, player.transform.position + floorOffset, player.transform.rotation));
            newFloors[floorDefectIndex].transform.SetParent(player.transform);
            repairTool.transform.SetParent(player.transform);
            created = true;
        } else
        {
            if (!newFloorPlaced)
            {
                newFloors[floorDefectIndex].transform.position = floorDefects[floorDefectIndex].transform.position;
                newFloors[floorDefectIndex].transform.rotation = floorDefects[floorDefectIndex].transform.rotation;
                Destroy(floorDefects[floorDefectIndex]);
                newFloorPlaced = true;
            }
        }
    }
    void repairToolAnimation()
    {
        hitsRemain--;
        repairTool.GetComponent<Animation>().Play();
    }
}
