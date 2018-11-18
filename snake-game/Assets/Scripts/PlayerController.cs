using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public Transform snake;
    public GameObject tailPrefab;
    public float tickTime = 0.1f;

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
        InvokeRepeating("Move", 0f, tickTime);
        GameManager.instance.InvokeRepeating("Spawn", 0, 3);
    }

    private void Update()
    {
        hp -= Time.deltaTime * 1.25f;
        GameManager.instance.hpTxt.text = "Health: " + hp.ToString("0");

        if (Input.GetAxis("Horizontal") > 0)
            dir = Vector2.right;
        else if (Input.GetAxis("Vertical") < 0)
            dir = Vector2.down;
        else if (Input.GetAxis("Horizontal") < 0)
            dir = Vector2.left;
        else if (Input.GetAxis("Vertical") > 0)
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
            if (tickTime >= 0.03f)
            {
                tickTime -= 0.005f;
                CancelInvoke("Move");
                InvokeRepeating("Move", 0f, tickTime);
            }
            grow = true;
            score++;
            hp += 5;
            if (hp > 100)
            {
                hp = 100;
            }
            GameManager.instance.hpTxt.text = "Health: " + hp;
            Destroy(other.gameObject);
            GameManager.instance.Spawn();
        }
       
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        GameManager.instance.gameOver = true;
        //get time on hit with collider, calculate and display final score
        float finalTimer = GameManager.instance.timer;
        float finalScore = finalTimer + score;
        GameManager.instance.scoreTxt.text = "Your last: " + finalScore.ToString("0.#");

        //save final score
        PlayerPrefs.SetFloat("Last score", finalScore);
        if (finalScore > PlayerPrefs.GetFloat("Best score"))
        {
            PlayerPrefs.SetFloat("Best score", finalScore);
        }
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
