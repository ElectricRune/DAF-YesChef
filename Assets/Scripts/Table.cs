using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    PlayerController player;
    public List<GameObject> uncut = new List<GameObject>();
    public List<GameObject> cut = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(player.IsCarrying(2))
            {
                StartCoroutine(Slice());
            }
        }
    }

    public IEnumerator Slice()
    {
        player.Chop(true);
        float start = Time.time;
        foreach(GameObject go in cut)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in uncut)
        {
            go.SetActive(true);
        }
        int count = 0;
        while(Time.time < start + 2f)
        {
            uncut[count].SetActive(false);
            cut[count].SetActive(true);
            count++;
            yield return new WaitForSeconds(.2f);
        }
        player.Chop(false);
        foreach (GameObject go in cut)
        {
            go.SetActive(false);
        }
    }

}
