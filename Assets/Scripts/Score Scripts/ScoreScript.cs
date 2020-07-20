using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public static int score;
    public int scoreCurrent;

    public Text scoreText;
    public Text winText;

    void Start()
    {
       
            

    }

    void Update()
    {

        if (score >= 50)
        {
            winText.gameObject.SetActive(true);
            Invoke("death", 2f);
        }

        scoreCurrent = score;
        scoreText.text = score.ToString();
    
    }
    void death()
    {

        SceneManager.LoadScene("MainMenu");

    }
}
