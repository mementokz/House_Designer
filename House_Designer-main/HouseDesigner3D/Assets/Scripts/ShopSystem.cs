using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    LocationsToMove locationsToMove;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject positionToPlaceSofa1;
    [SerializeField] GameObject positionToPlaceSofa2;
    [SerializeField] GameObject sofa1;
    [SerializeField] GameObject sofa2;

    [SerializeField] GameObject applyButton;

    GameObject newCard;

    bool called = false;
    // Start is called before the first frame update
    void Start()
    {
        applyButton.SetActive(false);
        shopPanel.SetActive(false);
        locationsToMove = FindObjectOfType<LocationsToMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (locationsToMove.GetIndex() > 0)
            if (locationsToMove.locations[locationsToMove.GetIndex()].CompareTag("ShopWaypoint") && !called)
            {
                Invoke("ActivateShopSystem", 2f);
                called = true;
            }
    }
    public void OnClickApply()
    {
        shopPanel.SetActive(false);
        locationsToMove.PlayConfetti();
        locationsToMove.MoveToNextLocation(2f);
    }

    public void ActivateShopSystem()
    {
        shopPanel.SetActive(true);
    }

    public void OnClickTypeSofa1()
    {
        applyButton.SetActive(true);
        Destroy(newCard);
        newCard = Instantiate(sofa1, positionToPlaceSofa1.transform.position, sofa1.transform.rotation) as GameObject;
    }
    public void OnClickTypeSofa2()
    {
        applyButton.SetActive(true);
        Destroy(newCard);
        newCard = Instantiate(sofa2, positionToPlaceSofa2.transform.position, sofa2.transform.rotation) as GameObject;
    }
}
