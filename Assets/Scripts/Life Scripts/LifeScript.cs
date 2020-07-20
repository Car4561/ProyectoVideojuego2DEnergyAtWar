using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{

    public Scrollbar healthBar;
    public static float heath = 1f;
    public float heath_current;
    public float life_current;

    public GameObject[] livesObject;

    public static int lives = 3;

    void Start()
    {
        heath_current = heath;
    }

    void Update()
    {
        for (int i = lives; i < livesObject.Length; i++)
        {
            livesObject[i].SetActive(false);
        }

        if (lives == 0)
        {

            Invoke("death", 3f);

        }
        else
        {
            if (heath <= 0)
            {
                heath = 1f;
                lives--;
            }
            life_current = lives;
            heath_current = heath;
            healthBar.size = heath_current;

        }
   
        
    }


    void death()
    {

        SceneManager.LoadScene("MainMenu");

    }

  

}
