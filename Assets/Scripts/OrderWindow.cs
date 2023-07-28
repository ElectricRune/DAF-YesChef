using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderWindow : MonoBehaviour
{

    Order order;
    public Text timeText;
    public Text scoreText;
    public Text scorePanelText;
    public List<GameObject> ingredients = new List<GameObject>();
    PlayerController player;
    GameManager manager;
    public CanvasGroup mainPanel;
    public CanvasGroup scorePanel;

    public void BeginOrder()
    {
        order = new Order();
    }

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        BeginOrder();
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        float timeVal = (Time.time - order.startTime);
        scoreText.text = (order.score - (int)timeVal).ToString();
        timeText.text = (Time.time - order.startTime).ToString("N2");
        foreach(GameObject item in ingredients)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < order.ingredients.Count; i++)
        {
            ingredients[(i * 3) + order.ingredients[i]].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!player.IsCarrying(0))
            {
                CheckOrder();
            }
        }
    }

    public void CheckOrder()
    {
        int carriedItem = 0;
        if (player.IsCarrying(1))
        {
            carriedItem = 0;
        }
        if (player.IsCarrying(4))
        {
            carriedItem = 1;
        }
        if (player.IsCarrying(5))
        {
            carriedItem = 2;
        }
        int count = order.ingredients.Count;
        for (int i = 0; i < count ; i++)
        {
            if(order.ingredients[i] == carriedItem)
            {
                order.ingredients.RemoveAt(i);
                UpdateUI();
                player.DropIngredient();
                if(order.ingredients.Count == 0)
                {
                    StartCoroutine(CompleteOrder());
                }
                break;
            }
        }
    }


    public IEnumerator CompleteOrder()
    {
        mainPanel.alpha = 0f;
        scorePanel.alpha = 1f;
        int points = (order.score - (int)(Time.time - order.startTime));
        manager.AddScore(points);
        if (points > 0)
        {
            scorePanelText.text = "+" + points.ToString();
        }
        else
        {
            scorePanelText.text = points.ToString();
        }
        yield return new WaitForSeconds(3f);
        float start = Time.time;
        while (Time.time < (start + 2f))
        {
            float current = 1f - ((Time.time - start) / 2f);
            scorePanel.alpha = current;
            yield return new WaitForEndOfFrame();
        }
        order = new Order();
        mainPanel.alpha = 1f;
    }

}

public class Order
{
    public float startTime;
    // 0 cheese, 1 veg, 2 meat
    public List<int> ingredients = new List<int>();
    public int score = 0;

    public Order()
    {
        startTime = Time.time;
        int loopcount = 2;
        if(Random.Range(0f,1f) < .5f)
        {
            loopcount = 3;
        }
        for (int i = 0; i < loopcount; i++)
        {
            ingredients.Add(Random.Range(0, 3));
        }
        foreach(int i in ingredients)
        {
            if(i==0)
            {
                score += 10;
            }
            if (i == 1)
            {
                score += 20;
            }
            if (i == 2)
            {
                score += 30;
            }
        }
    }
}
