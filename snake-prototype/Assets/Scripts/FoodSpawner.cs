using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    public static FoodSpawner instance;

    public GameObject foodPref;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {

        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);

        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        Instantiate(foodPref, new Vector2(x, y), Quaternion.identity); 
    }
}
