using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    PlayerController player;

    public List<GameObject> burners = new List<GameObject>();
    public List<GameObject> meats = new List<GameObject>();
    public List<GameObject> burgers = new List<GameObject>();
    int[] burnerStates = { 0, 0 };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.IsCarrying(3))
            {
                if(burnerStates[0] == 0)
                {
                    StartCoroutine(Cook(0));
                }
                else if(burnerStates[1] == 0)
                {
                    StartCoroutine(Cook(1));
                }
            }
            else if(player.IsCarrying(0))
            {
                if(burnerStates[0] == 2)
                {
                    burnerStates[0] = 0;
                    burgers[0].SetActive(false);
                    player.GetIngredient(4);
                    player.carrying = 5;
                }
                else if(burnerStates[1] == 2)
                {
                    burnerStates[1] = 0;
                    burgers[1].SetActive(true);
                    player.GetIngredient(4);
                    player.carrying = 5;
                }
            }
        }
    }


    IEnumerator Cook(int _burner)
    {
        player.DropIngredient();
        burners[_burner].SetActive(true);
        meats[_burner].SetActive(true);
        burnerStates[_burner] = 1;
        yield return new WaitForSeconds(6f);
        burnerStates[_burner] = 2;
        meats[_burner].SetActive(false);
        burgers[_burner].SetActive(true);
    }



}
