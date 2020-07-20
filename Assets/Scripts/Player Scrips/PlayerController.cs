using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 7.5f;

    public float min_Y = -3.78f, max_Y = 3.78f;

    [SerializeField]
    public GameObject player_bullet;

    [SerializeField]
    public Transform attack_point;

    public float attack_timer = 0.35f;
    public float time_acumlated = 0.0f;

    public float heath_current;
    public float lives_current;



    private float current_attack_timer;
    private bool canAttackt;
    private bool start;
    private AudioSource laserAudio;
    private Animator anim;


    void Awake()
    {

        laserAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();


    }
    void Start()
    {
        start = true;
        current_attack_timer = attack_timer;

    }

    // Update is called once per frame
    void Update()
    {

        MovePlayer();
        Attack();
    }

    void MovePlayer() {

        if (Input.GetAxisRaw("Vertical") > 0f) {

            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > max_Y) {
                temp.y = max_Y;
            }


            transform.position = temp;


        } else if (Input.GetAxisRaw("Vertical") < 0f) {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            if (temp.y < min_Y)
            {
                temp.y = min_Y;
            }

            transform.position = temp;

        }

    }

    void Attack() {

        time_acumlated += Time.deltaTime;
        if (time_acumlated - attack_timer > current_attack_timer || start == true) {
            canAttackt = true;
            attack_timer = time_acumlated;
            start = false;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)) {
            if (canAttackt)
            {

                attack_timer = time_acumlated;
                canAttackt = false;

                Instantiate(player_bullet, attack_point.position, Quaternion.identity);

                laserAudio.Play();
                //    current_attack_timer = attack_timer;
            }
        }
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    //continuar aprendiendo
    private void OnTriggerEnter2D(Collider2D target)
    {

            lives_current = LifeScript.lives;
            heath_current = LifeScript.heath;
            if (target.tag == "Enemy" || target.tag == "Bullet")
            {
        
                if (target.tag == "Enemy") 
                {
                    LifeScript.heath -= 0.3f;

                } else  {
                    LifeScript.heath -= 0.15f;
                }
          
                if (lives_current < 1)
                {

                    anim.Play("Destroy");
                    Invoke("TurnOffGameObject", 3f);

                }

         }
       
    }

   
}
