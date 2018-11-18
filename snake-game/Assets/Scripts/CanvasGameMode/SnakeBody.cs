using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour {

    public Transform snakeHead;

    void Update()
    {
        if (snakeHead == null) return;
        Vector3 headDistance = transform.position - snakeHead.position;
        headDistance = headDistance.normalized * transform.localScale.y;
        transform.position = headDistance + snakeHead.position;
    }
}
