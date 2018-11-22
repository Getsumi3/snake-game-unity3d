using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [Header("[Snake body]")]
    public Transform snake;
    public GameObject tailPrefab;
    //public float maxTickTime = 0.1f;
    //public float minTickTime = 0.05f;

    [Header("[Audio settigs]")]
    public AudioClip eatSfx;
    public AudioClip dieSfx;
    private AudioSource audioSource;
    


    //default move direction
    private Vector2 dir = Vector2.up;
    //snake length
    private List<Transform> tails = new List<Transform>();
    //will our snake grow?
    private bool grow = false;

    //score
    private int score;
    //snake health
    private float hp = 100;


    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Move", 0f, GameManager.Instance.playerMaxSpeed);
        GameSceneManager.instance.InvokeRepeating("Spawn", 0, GameManager.Instance.foodSpawnRate);
    }

    private void Update()
    {
        hp -= Time.deltaTime * GameManager.Instance.playerHpRate;
        GameSceneManager.instance.hpTxt.text = "Health: " + hp.ToString("0");

        if (Input.GetAxis("Horizontal") > 0 && dir != Vector2.left)
            dir = Vector2.right;
        else if (Input.GetAxis("Vertical") < 0 && dir != Vector2.up)
            dir = Vector2.down;
        else if (Input.GetAxis("Horizontal") < 0 && dir != Vector2.right)
            dir = Vector2.left;
        else if (Input.GetAxis("Vertical") > 0 && dir != Vector2.down)
            dir = Vector2.up;
        
    }

    void Move()
    {
        // current head position
        Vector2 headPos = transform.position;

        // move to dir
        transform.Translate(dir);

        // if we ate - we grow. expand our tail
        if (grow)
        {
            GameObject tail = Instantiate(tailPrefab, headPos, Quaternion.identity, snake);
            tails.Insert(0, tail.transform);
            grow = false;
        }

        else if (tails.Count > 0)
        {

            tails.Last().position = headPos;
            tails.Insert(0, tails.Last());
            tails.RemoveAt(tails.Count - 1);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "food")
        {
            //let's increase player speed when he eat
            if (GameManager.Instance.playerMaxSpeed >= GameManager.Instance.playerMinSpeed)
            {
                GameManager.Instance.playerMaxSpeed -= 0.005f;
                CancelInvoke("Move");
                InvokeRepeating("Move", 0f, GameManager.Instance.playerMaxSpeed);
            }
            grow = true;
            score += 5;
            hp += 5;
            if (hp > 100)
            {
                hp = 100;
            }
            GameSceneManager.instance.hpTxt.text = "Health: " + hp;
            Destroy(other.gameObject);
            GameSceneManager.instance.Spawn();
            
            audioSource.PlayOneShot(eatSfx);
        }
       
        else
        {
            audioSource.clip = dieSfx;
            audioSource.Play();
            GameOver();
        }
    }

    private void GameOver()
    {
        GameSceneManager.instance.gameOver = true;
        
        //get time on hit with collider, calculate and display final score
        float finalTimer = GameSceneManager.instance.timer;
        print(finalTimer);
        float finalScore = finalTimer + score;
        print(finalScore);
        //save final score
        PlayerPrefs.SetFloat("Last score", finalScore);
        if (finalScore > PlayerPrefs.GetFloat("Best score"))
        {
            PlayerPrefs.SetFloat("Best score", finalScore);
        }
        PlayerPrefs.Save();

        SceneManager.LoadScene(0);
    }
}
