using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationsToMove : MonoBehaviour
{
    [SerializeField] public List<GameObject> locations;
    [SerializeField]  private Transform player;
    [SerializeField] private Transform camera;
    [SerializeField] public ParticleSystem confetti;

    public Vector3 offset = new Vector3(0, -4.5f, -3);

    int index = -1;
    // Start is called before the first frame update
    void Start()
    {
        confetti.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayConfetti()
    {
        confetti.transform.position = camera.transform.position + offset;
        confetti.Play();
    }

    public void MoveToNextLocation(float time)
    {
        index++;
        Invoke("Mover", time);
    }

    public void MoveToNextLocation()
    {
        if (index == 10)
        {
            Mover();
            PlayConfetti();
        }
        else
        {
            index++;
            Mover();
        }
    }

    private void Mover()
    {
        confetti.Stop();
        player.position = locations[index].transform.position;
        player.rotation = locations[index].transform.rotation;
    }

    public int GetIndex()
    {
        return index;
    }
}
