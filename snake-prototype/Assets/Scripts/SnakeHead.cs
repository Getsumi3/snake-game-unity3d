using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeHead : MonoBehaviour {

    private int score;
    private Vector2 dir = Vector2.right;
    private List<Transform> body = new List<Transform>();

    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.3f);
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = -Vector2.up;    
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = -Vector2.right; 
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }

    void Move()
    {
        Vector2 v = transform.position;

        transform.Translate(dir);

        if (body.Count > 0)
        {
            body.Last().position = v;

            body.Insert(0, body.Last());
            body.RemoveAt(body.Count - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "apple")
        {
            score++;
            print(score);
            Destroy(collision.gameObject);
            FoodSpawner.instance.Spawn();
        }
    }


}
