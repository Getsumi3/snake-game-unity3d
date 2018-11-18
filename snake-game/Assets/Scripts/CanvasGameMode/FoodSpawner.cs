using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSpawner : MonoBehaviour {

    public static FoodSpawner instance;

    public GameObject foodPref;
    public Canvas canvas;
    public RectTransform playGround;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Spawn();
        //InvokeRepeating("Spawn", 0.3f, 0.3f);
    }

    public void Spawn()
    {

        Vector3[] v = new Vector3[4];
        playGround.GetWorldCorners(v);

        int x = (int)Random.Range(v[0].x, v[3].x);

        int y = (int)Random.Range(v[0].y, v[1].y);

        Instantiate(foodPref,  new Vector2(x, y), Quaternion.identity, canvas.transform);
    }
}
