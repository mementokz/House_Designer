using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairFloorDefect : MonoBehaviour
{
    TrashClean trashClean;
    [SerializeField] List<GameObject> listOfWaypointsOfFloorDefects;
    [SerializeField] List<GameObject> floorDefects;
    int floorDefectIndex = 0;
    [SerializeField] GameObject player;
    [SerializeField] GameObject newFloor;
    [SerializeField] GameObject repairTool;

    [SerializeField] TMPro.TextMeshPro hitsRemainText;
    Vector3 floorOffset = new Vector3(1, -0.4f, 0.5f);
    Vector3 repairtToolOffset = new Vector3(1, -0.4f, -1f);

    int WayPointIndex = 0;
    int hitsRemain = 5;
    bool created = false;
    bool newFloorPlaced = false;
    void Start()
    {
        hitsRemainText.enabled = false;
        trashClean = FindObjectOfType<TrashClean>();
    }

    // Update is called once per frame
    void Update()
    {
        hitsRemainText.text = hitsRemain.ToString() + " hits remain";
        if (hitsRemain <= 0)
        {
            hitsRemainText.enabled = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.transform != null)
                {
                    Debug.Log(hit.transform.gameObject.tag);
                    if (hit.transform.gameObject.tag == "FloorDefect")
                    {
                        Debug.Log(listOfWaypointsOfFloorDefects[WayPointIndex].transform.position);
                        Debug.Log(listOfWaypointsOfFloorDefects[WayPointIndex].transform.rotation);
                        if (trashClean.trashesAndFloorDefet[0].tag == "FloorDefect")
                        {
                            MoveToFloorDefect();
                        }
                    }
                    if (hit.transform.gameObject.tag == "NewFloor" && newFloorPlaced && created && hitsRemain > 0)
                    { 
                            repairToolAnimation();
                    }
                }
        }
    }

    void MoveToFloorDefect()
    {
        player.transform.position = listOfWaypointsOfFloorDefects[WayPointIndex].transform.position;
        player.transform.rotation = listOfWaypointsOfFloorDefects[WayPointIndex].transform.rotation;
        if (!created)
        {
            newFloor = Instantiate(newFloor, player.transform.position + floorOffset, newFloor.transform.rotation) as GameObject;
            repairTool = Instantiate(repairTool, player.transform.position + repairtToolOffset, repairTool.transform.rotation) as GameObject;
            created = true;
        } else
        {
            if (!newFloorPlaced)
            {
                newFloor.transform.position = floorDefects[floorDefectIndex].transform.position;
                newFloor.transform.rotation = floorDefects[floorDefectIndex].transform.rotation;
                Destroy(floorDefects[floorDefectIndex]);
                floorDefects.RemoveAt(floorDefectIndex);
                newFloorPlaced = true;
                hitsRemainText.enabled = true;
            }
        }
    }
    void repairToolAnimation()
    {
        hitsRemain--;
        Debug.Log("ANIMATION");
        repairTool.GetComponent<Animation>().Play();
    }
}
