using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] List<GameObject> listOfWaypoints;

    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
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
                    Debug.Log(hit.transform.gameObject.name);
                    if (hit.transform.gameObject.name == "MainDoor")
                    {
                        transform.position = new Vector3(0, transform.position.y, 4.20f);
                        Destroy(hit.transform.gameObject.GetComponent<Collider>());
                        Invoke("MovePlayerToWaypoints", 4f);
                  
                        
                    }
                }
        }
    }

    public void MovePlayerToWaypoints()
    {
        transform.position = listOfWaypoints[index].transform.position;
        transform.rotation = listOfWaypoints[index].transform.rotation;
        index++;
    }
}
