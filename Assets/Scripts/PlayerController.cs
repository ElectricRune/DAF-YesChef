using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public List<GameObject> ingredients = new List<GameObject>();

    public float speed = 10f;
    public float rotationSpeed = .1f;
    Vector3 moveVector;
    Rigidbody rb;
    bool selecting = false;
    bool chopping = false;
    public int carrying = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!chopping)
        {
            float vMove = Input.GetAxis("Vertical");
            moveVector = transform.forward * speed * vMove;
            float hMove = Input.GetAxis("Horizontal");
            transform.localRotation = transform.localRotation * Quaternion.Euler(new Vector3(0f, hMove * rotationSpeed, 0f));

            if (selecting)
            {
                if (Input.GetKeyDown(KeyCode.Space) && carrying < 4)
                {
                    carrying++;
                    if (carrying == 4)
                    {
                        carrying = 1;
                    }
                    GetIngredient(carrying - 1);
                }
            }
        }
        else
        {
            moveVector = Vector3.zero;
        }
    }

    public void GetIngredient(int _ingredient)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if(i==_ingredient)
            {
                ingredients[i].SetActive(true);
            }
            else
            {
                ingredients[i].SetActive(false);
            }
        }
    }

    public void DropIngredient()
    {
        carrying = 0;
        for (int i = 0; i < ingredients.Count; i++)
        {
            ingredients[i].SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector;
    }

    public void Select(bool _on)
    {
        selecting = _on;
    }

    public void Chop(bool _on)
    {
        chopping = _on;
        if(chopping)
        {
            DropIngredient();
        }
        else
        {
            GetIngredient(3);
            carrying = 4;
        }
    }

    public bool IsCarrying(int _item)
    {
        if(carrying == _item)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
