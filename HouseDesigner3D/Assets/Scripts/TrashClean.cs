using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashClean : MonoBehaviour
{
    [SerializeField] public List<GameObject> trashesAndFloorDefet;
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
                    Debug.Log(hit.transform.gameObject.tag);
                    if (hit.transform.gameObject.tag == "Trash")
                    {
                        trashesAndFloorDefet.Remove(hit.transform.gameObject);
                        Destroy(hit.transform.gameObject);
                    }
                }
        }
    }
}
