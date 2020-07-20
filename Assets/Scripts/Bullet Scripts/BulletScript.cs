using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float speed = 8f;
    public float deactive_Timer = 3f;

    [HideInInspector]
    public bool is_EnemyBullet = false;
    void Start()
    {

    
        Invoke("DeactivateGameObject", deactive_Timer);
        if (is_EnemyBullet) {
            speed *= -1f;
        } else  {

            transform.Rotate(0, 0, -91);
        }
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move() {

        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
    
    }

    void DeactivateGameObject() {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet" || target.tag == "Enemy") {
            gameObject.SetActive(false);
        }
    }
} 
