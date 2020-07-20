using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

    public float speed = 5f;
    public float rotate_speed = 50f;

    public bool canShoot;
    public bool canRotate;
    public bool canMove = true;

    public float bound_X = -11f;

    public float ShootingRespawn_initial_max = 2f;
    public float velocity_shooting = 1f;

    public float current_shooting_respawn_limit;
    public Text winText;


    public Transform attack_point;
    public GameObject bulletPrefab;

    private Animator anim;
    private AudioSource explosionSound;




    void Awake()
    {

        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();

    }   

    void Start()
    {

        if (canRotate) {
            if (Random.Range(1, 2) > 0) {

                rotate_speed = Random.Range(rotate_speed, rotate_speed + 20f);
                rotate_speed *= -1f;

            }
        }

        if (canShoot)
        {
            Invoke("StartShooting", Random.Range(1f, 3f));
        }

         
    }

    
    void Update()
    {

        Move();
        RotateEnemy();


    }   

    void Move() {
        if (canMove) {
            
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;
            if (temp.x < bound_X) {
                gameObject.SetActive(false);
                canShoot = false;
            }     

       } 
    }

    void RotateEnemy() {
        if (canRotate) {
            transform.Rotate(new Vector3(0f, 0f, rotate_speed * Time.deltaTime), Space.World);
        }
        
    }

    void StartShooting() {
        GameObject bullet = Instantiate(bulletPrefab, attack_point.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().is_EnemyBullet = true;

        if (canShoot) {
            current_shooting_respawn_limit = ShootingRespawn_initial_max - (Time.time * velocity_shooting / 100f);
            Invoke("StartShooting", Random.Range(0.5f, current_shooting_respawn_limit));

        }
    }
    void TurnOffGameObject() {

        gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D target)
    {
      
      
        if (target.tag == "Bullet") {

            canMove = false;

            if (canShoot) {

                canShoot = false;
                CancelInvoke("StartShooting");

            }

            explosionSound.Play();
            anim.Play("Destroy");

            Invoke("TurnOffGameObject", 1f);
            ScoreScript.score++;
        }
     
    }

    void death()
    {

        SceneManager.LoadScene("MainMenu");

    }
}
