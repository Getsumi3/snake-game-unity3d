using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeHead : MonoBehaviour {

    public Canvas canvas;
    public GameObject tailPrefab;
    private int score;
    private Vector2 dir = Vector2.up;
    private List<Transform> body = new List<Transform>();

    private bool canGrow = false;

    void Start()
    {
        InvokeRepeating("Move", 0, 0.3f);
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
        // Save current position (gap will be here)
        Vector2 headPos = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir);

        // Ate something? Then insert new Element into gap
        if (canGrow)
        {
            // Load Prefab into the world
            GameObject _tail = (GameObject)Instantiate(tailPrefab, headPos, Quaternion.identity, canvas.transform);

            // Keep track of it in our tail list
            body.Insert(0, _tail.transform);

            // Reset the flag
            canGrow = false;
        }
        // Do we have a Tail?
        else if (body.Count > 0)
        {
            // Move last Tail Element to where the Head was
            body.Last().position = headPos;

            // Add to front of list, remove from the back
            body.Insert(0, body.Last());
            body.RemoveAt(body.Count - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "food")
        {

            canGrow = true;
            score++;
            print(score);
            Destroy(collision.gameObject);
            FoodSpawner.instance.Spawn();
        }
        else
        {
            print("game over. food collected: " + score);
            CancelInvoke("Move");
        }
    }


}
