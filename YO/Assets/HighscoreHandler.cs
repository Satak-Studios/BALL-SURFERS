using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreHandler : MonoBehaviour
{
    public Text scoreText;
    public GameManager gm;
    public Score se;
    public Restart rs;
    public Rigidbody rb;
    public Text Score;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Score.text;
    }
}
