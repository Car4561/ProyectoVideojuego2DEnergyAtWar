using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float min_Y = -3.78f, max_Y = 3.78f;
    
    [SerializeField]
    public GameObject[] asteroid_prefab;

    [SerializeField]
    public GameObject enemyPrefab;


    public float timer = 2f;
    public float current_timer ;

    public float velocity_spawn = 1f;


    void Start()
    {
        current_timer = timer;   
        Invoke("SpawnEnemies", timer);
 
    }

    void Update()
    {

    }

    void SpawnEnemies() {

        float pos_Y = Random.Range(min_Y, max_Y);
        Vector3 temp = transform.position;
        temp.y = pos_Y;
        if (Random.Range(0, 2) > 0)
        {

            Instantiate(asteroid_prefab[Random.Range(0, asteroid_prefab.Length)], temp, Quaternion.identity);


        } else {

            Instantiate(enemyPrefab, temp, Quaternion.Euler(0f, 0f, 90f));
        }
       
        current_timer = timer - (Time.time * velocity_spawn/100);
        if (current_timer < 0.1f)
        {
            current_timer = 0.2f;

        }
        Invoke("SpawnEnemies", current_timer);

    }

}
