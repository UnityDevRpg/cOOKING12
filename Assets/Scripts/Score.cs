using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
        private GameObject GameOverScreen;
        private TextMeshProUGUI ScoreText;
         public int score1; 
    // Start is called before the first frame update
    void Start()
    {
        GameOverScreen = GameObject.Find("UI").transform.Find("EndGameScreen").transform.Find("Button").transform.Find("ScoreText").gameObject;
        ScoreText = GameOverScreen.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(score1);
        ScoreText.text = ("Score: " + score1);
    }
}
