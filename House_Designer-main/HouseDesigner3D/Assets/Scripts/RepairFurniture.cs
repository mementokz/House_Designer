using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairFurniture : MonoBehaviour
{
    LocationsToMove locationsToMove;
    [SerializeField] GameObject chair;
    // Start is called before the first frame update
    void Start()
    {
        locationsToMove = FindObjectOfType<LocationsToMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.transform != null)
                {
                    if (locationsToMove.GetIndex() > 0)
                    {
                        if (hit.transform.gameObject.tag == "RepairFurniture" && locationsToMove.locations[locationsToMove.GetIndex()].CompareTag("RepairFurnitureWaypoint"))
                        {
                            chair.transform.Rotate(-10, 0, 0);
                            check();
                        }
                    }
                }
        }
    }
    void check()
    {
        if (chair.transform.rotation.x > -10 && chair.transform.rotation.x < 10)
        {
            chair.transform.rotation = new Quaternion(0, 0, 0, 0);
            locationsToMove.PlayConfetti();
            locationsToMove.MoveToNextLocation(2f);
        }
    }
}
