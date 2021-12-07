using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairWallDefect : MonoBehaviour
{
    TrashClean trashClean;
    LocationsToMove locationsToMove;

    [SerializeField] List<GameObject> wallDefects;
    [SerializeField] List<GameObject> newWalls;
    int wallDefectIndex = 0;
    [SerializeField] GameObject player;
    [SerializeField] GameObject newWall;
    [SerializeField] GameObject repairTool;

    Vector3 wallOffset = new Vector3(1, -0.4f, -1);
    Vector3 repairtToolOffset = new Vector3(-1, -0.4f, -1);

    int hitsRemain = 5;
    bool created = false;
    bool newWallPlaced = false;
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
            Destroy(newWalls[wallDefectIndex]);
            wallDefectIndex++;
            repairTool.SetActive(false);
            locationsToMove.PlayConfetti();
            locationsToMove.MoveToNextLocation(2f);
            isMoved = true;
            newWallPlaced = false;
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
                        if (hit.transform.gameObject.tag == "WallDefect" && locationsToMove.locations[locationsToMove.GetIndex()].CompareTag("WallDefectWaypoint"))
                        {
                            CreateObjectsToReparirWall();
                            isMoved = false;
                            hitsRemain = 5;
                        }
                        if (hit.transform.gameObject.tag == "NewWall" && newWallPlaced && created && hitsRemain > 0)
                        {
                            repairToolAnimation();
                        }
                    }
                }
        }
    }

    void CreateObjectsToReparirWall()
    {
        if (!created)
        {
            repairTool.SetActive(true);
            repairTool.transform.position = player.transform.position + repairtToolOffset;
            newWalls.Add(Instantiate(newWall, player.transform.position + wallOffset, player.transform.rotation));
            newWalls[wallDefectIndex].transform.SetParent(player.transform);
            repairTool.transform.SetParent(player.transform);
            created = true;
        } else
        {
            if (!newWallPlaced)
            {
                newWalls[wallDefectIndex].transform.position = wallDefects[wallDefectIndex].transform.position;
                newWalls[wallDefectIndex].transform.rotation = wallDefects[wallDefectIndex].transform.rotation;
                Destroy(wallDefects[wallDefectIndex]);
                newWallPlaced = true;
            }
        }
    }
    void repairToolAnimation()
    {
        hitsRemain--;
        repairTool.GetComponent<Animation>().Play();
    }
}
