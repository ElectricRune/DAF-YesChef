using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
    PlayerController player;
    public Transform door;
    public GameObject fog;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Select(true);
            FridgeDoor(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Select(false);
            FridgeDoor(false);
        }
    }

    private void FridgeDoor(bool _open)
    {
        fog.SetActive(_open);
        if(!_open)
        {
            door.eulerAngles = Vector3.zero;
        }
        else
        {
            door.eulerAngles = new Vector3(0f, -145f, 0f);
        }
    }


}
