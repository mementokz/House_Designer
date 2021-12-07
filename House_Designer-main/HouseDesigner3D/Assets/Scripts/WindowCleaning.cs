using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowCleaning : MonoBehaviour
{
    LocationsToMove locationsToMove;
    [SerializeField] GameObject window;
    [SerializeField] GameObject progressBar;
    // Start is called before the first frame update

    bool setted = false;
    void Start()
    {
        progressBar.SetActive(false);
        locationsToMove = FindObjectOfType<LocationsToMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (progressBar.transform.GetChild(0).GetComponent<Image>().fillAmount == 1f && setted)
        {
            window.GetComponent<MeshCollider>().enabled = false;
            progressBar.SetActive(false);
            locationsToMove.PlayConfetti();
            locationsToMove.MoveToNextLocation(2f);
            setted = false;
        }
        if (locationsToMove.GetIndex() > 0)
        {
            if (locationsToMove.locations[locationsToMove.GetIndex()].CompareTag("WindowWaypoint") && !setted)
            {
                window.GetComponent<MeshCollider>().enabled = true;
                progressBar.SetActive(true);
                setted = true;
            }
        }
    }
}
