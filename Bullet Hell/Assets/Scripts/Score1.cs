using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public TMP_Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString() + " PTS";
    }
}