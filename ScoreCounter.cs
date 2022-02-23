using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private Text scoreText;
    public static int score;

    void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
