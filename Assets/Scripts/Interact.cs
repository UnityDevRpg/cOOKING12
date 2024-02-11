using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    private Animator fridgeAnimator;
    public AudioSource audioSource;
    public GameObject GameOverScreen;
    public GameObject BakedCookie;
    private Animator ovenAnimator;
    public bool hasBeenInOven = false;
    public Image Timer;
    private float currentTimer = 1f;
    public float timerMultiplier = 1f / 160f;

    private Animator animator;
    private bool HasACookieOnPlate;
    private bool HasATeaOnPlate;
    public GameObject RaycastingControll;
    private OrdererController ordererController;
    private GameObject currentCookie;
    public GameObject cookie;
    private GameObject currentCake;
    public GameObject cake;
    private GameObject currentTea;
    public GameObject tea;
    private GameObject currentPlate;
    private GameObject currentCup;
    public float handItemCount = 0;
    public GameObject hand;
    public GameObject Plate1;
    public GameObject Cup1;
    public int MaxDistance = 10;
    public GameObject cam;
    public int MaxCookies = 1;
    public int currentCookies = 0;
    public int MaxCake = 1;
    public int CurrentCake = 0;

    private void Start() {
        Timer = GameObject.Find("UI").transform.Find("Timer").GetComponent<Image>();
        Timer.fillAmount = 1f;
        timerMultiplier = 1f / 160f; 

    }

    private void FixedUpdate() {
        currentTimer -= Time.deltaTime * timerMultiplier;
        currentTimer = Mathf.Clamp(currentTimer, 0f, 1f);
        Timer.fillAmount = currentTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
        currentTimer -= Time.deltaTime * timerMultiplier; // Decrease the timer based on the current timerMultiplier
        currentTimer = Mathf.Clamp01(currentTimer); // Ensure currentTimer stays within 0 and 1
        Timer.fillAmount = currentTimer;
        death();
        RaycastHit hitinfo;

        if(Physics.Raycast(RaycastingControll.transform.position, RaycastingControll.transform.forward, out hitinfo, MaxDistance))
        {
            if(hitinfo.collider.CompareTag("Orderer"))
            {
                ordererController = hitinfo.collider.gameObject.GetComponent<OrdererController>();
                if(ordererController.isDestroyed == true)
                {
                    currentTimer = 1f;
                    timerMultiplier -= 1.5f; // Decrease timerMultiplier by 0.5
                    Timer.fillAmount = 1f;
                    timerMultiplier = Mathf.Max(timerMultiplier, 1f / 160f); // Ensure timerMultiplier doesn't go below the minimum value
                    Timer.fillAmount = 1f;
                    ordererController.isDestroyed = false;
                } 

            }
            
        }

        
        if(Input.GetKeyDown(KeyCode.E))
            Interact1();
        
    }

    public void Interact1()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, MaxDistance))
        {
            if(hitInfo.collider.CompareTag("SoftDrinks"))
                SoftDrinks();
            if(hitInfo.collider.CompareTag("Cookies"))
                Cookies();
            if(hitInfo.collider.CompareTag("Cup"))
                Cup();
            if(hitInfo.collider.CompareTag("Plate"))
                Plate();
            if(hitInfo.collider.CompareTag("Register"))
                decreaseMenuCount();
            if(hitInfo.collider.CompareTag("Cake"))
                Cake();
            if(hitInfo.collider.CompareTag("Oven"))
            {
                ovenAnimator = hitInfo.collider.gameObject.GetComponent<Animator>();
                useOven();
            }
        }
    }



    public void SoftDrinks () {
        if(currentCup != null)
        {
            currentTea = Instantiate(tea, hand.transform);
        }
    }

    public void Cookies () {
        Destroy(currentCake);
         Debug.Log("Current Plate: " + currentPlate); // Add this line
        if(currentPlate != null && currentCookies < MaxCookies)
        {   
            if(currentCookie == null)
            {
                currentCookies++;
                currentCookie = Instantiate(cookie, hand.transform);
            }
        }
    }

    public void Cake() {
        Destroy(currentCookie);
        Debug.Log("Current Plate: " + currentPlate); // Add this line
        if(currentPlate != null && CurrentCake < MaxCake)
        {   
            CurrentCake++;
            currentCake = Instantiate(cake, hand.transform);
        }
    }

    public void Cup () {
        if (handItemCount == 1)
        {
            Destroy(currentCup);
            Destroy(currentPlate);
            Destroy(currentCookie);
            Destroy(currentTea);
            Destroy(currentCake);
            currentCup = Instantiate(Cup1, hand.transform);
        } else {
            currentCup = Instantiate(Cup1, hand.transform);
            handItemCount++;
        }
    }

     public void Plate () {
        if (handItemCount == 1)
        {
            Destroy(currentCup);
            Destroy(currentPlate);
            Destroy(currentCookie);
            Destroy(currentTea);
            Destroy(currentCake);
            currentPlate = Instantiate(Plate1, hand.transform);
        } else {
            currentPlate = Instantiate(Plate1, hand.transform);
            handItemCount++;
        }
    }

    public void decreaseMenuCount()
    {
        audioSource.Play();
        if(currentCookie != null && hasBeenInOven)
        {
            ordererController.eatCookie();
            hasBeenInOven = false;
            Destroy(currentCookie);
            currentCookies--;
        }
        if(currentTea != null)
            ordererController.eatTea();
        Destroy(currentTea);
        if(currentCake != null)
        {
            CurrentCake--;
            ordererController.eatCake();
        }
        Destroy(currentCake);
    }

    public void death()
    {
        if(Timer.fillAmount <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
            GameOverScreen.SetActive(true);
        }
    }
    
public void useOven()
{
    if (currentCookie != null)
    {
        ovenAnimator.SetTrigger("IsOpening");
        audioSource.Play();
        Destroy(currentCookie);
        currentCookies--;
        Invoke("AnimationOven", 3f);
        Invoke("FinishOven", 3.5f);
    }
}

public void FinishOven()
{
    hasBeenInOven = true;
    if (currentPlate != null && MaxCookies > currentCookies)
    {
        currentCookie = Instantiate(BakedCookie, hand.transform);
        currentCookies++;
    }
}



    public void  AnimationOven () {
        audioSource.Play();
        ovenAnimator.SetTrigger("IsOpening");
    }
    

}
