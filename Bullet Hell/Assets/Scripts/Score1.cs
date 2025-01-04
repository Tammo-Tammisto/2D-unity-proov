using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float score = 0;
    public static float totalScore = 0;
    public TMP_Text scoreText;

    void Start()
    {
        score = totalScore;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString() + " PTS";
    }
}
