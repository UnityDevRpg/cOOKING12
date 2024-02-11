using UnityEngine;
using TMPro;
using System.Data.SqlTypes;
using UnityEngine.UI;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class OrdererController : MonoBehaviour
{
    private Score score;
    private Interact interact;
    public bool isDestroyed = false;
    private Spawner spawner;
    private Animator animator;
    private TextMeshProUGUI TotalMoneyCountText;

    public GameObject PoofAnimation;
    public int cookiesToServe;
    public int teasToServe;
    public int CakesToServe;
    public GameObject OrderMenu;
    private TextMeshProUGUI cookieText;
    private TextMeshProUGUI teaText;
    private TextMeshProUGUI CakeText;

    private void Start()
    {
        score = GameObject.Find("Script").GetComponent<Score>();
        interact = GameObject.Find("FirstPersonController").GetComponent<Interact>();
        animator = GetComponent<Animator>();
        OrderMenu.SetActive(false);
        CakeText = this.transform.Find("OrdererMenu").transform.Find("Menu").transform.Find("Cake").transform.Find("CakeText").GetComponent<TextMeshProUGUI>();
        spawner = GameObject.Find("OrdererSpawner").GetComponent<Spawner>();
        cookieText = this.transform.Find("OrdererMenu").transform.Find("Menu").transform.Find("Cookies").transform.Find("CookieText").GetComponent<TextMeshProUGUI>();
        teaText = this.transform.Find("OrdererMenu").transform.Find("Menu").transform.Find("Tea").transform.Find("TeaText").GetComponent<TextMeshProUGUI>();

    }

    private void Update() {
       
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Register"))
        {
            animator.SetBool("IsOrdering", true); 
            OrderMenu.SetActive(true);
            cookiesToServe = Random.Range(1, 3); // Randomly set cookies to serve between 1 and 3
            teasToServe = Random.Range(1, 4); // Randomly set cookies to serve between 1 and 3
            CakesToServe = Random.Range(1, 3); // Randomly set cookies to serve between 1 and 3
            cookieText.SetText("Cookies: " + cookiesToServe);
            teaText.SetText("Cups of Tea: " + teasToServe);
            CakeText.SetText("Slices of Cake: " + CakesToServe);
        }
    }

    public void eatCookie()
    {
        cookiesToServe -= 1;
        cookieText.SetText("Cookies: " + cookiesToServe); // Update displayed text
        EndGame();
    }

    public void eatTea()
    {
        teasToServe -= 1;
        teaText.SetText("Cups of Tea: " + teasToServe); // Update displayed text
        EndGame();
    }

    public void eatCake()
    {
        CakesToServe -= 1;
        Debug.Log(CakesToServe);
        CakeText.SetText("Slices of Cake: " + CakesToServe); // Update displayed text
        EndGame();
    }

    public void EndGame () {
        if (cookiesToServe <= 0 && teasToServe <= 0 && CakesToServe <= 0)
        {
            score.score1++;
            interact.Timer.fillAmount = 1f;
            Instantiate(PoofAnimation, this.gameObject.transform);
            isDestroyed = true;
            Destroy(this.gameObject, 1f);
            spawner.CurrentPlayerCount--;
        }
    }

}
