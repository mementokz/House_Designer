using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDPanel : MonoBehaviour
{



    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject openInfoPanel;
    [SerializeField] GameObject closeInfoPanel;
    // Start is called before the first frame update
    void Start()
    {
        closeInfoPanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void ClickOnClose()
    {
        closeInfoPanel.SetActive(false);
        openInfoPanel.SetActive(true);
        infoPanel.SetActive(false);
    }


    public void ClickOnOpen()
    {
        closeInfoPanel.SetActive(true);
        openInfoPanel.SetActive(false);
        infoPanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
