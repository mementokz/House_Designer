using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        transform.rotation = player.transform.rotation;
    }
}
