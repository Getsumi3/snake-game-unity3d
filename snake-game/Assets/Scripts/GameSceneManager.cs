using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

    public static GameSceneManager instance;
    // Food Prefab
    public GameObject foodPrefab;
    //where in the hierrachy we will spawn our food
    public Transform foodParent;

    [Header("Borders")]
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    [Header("UI")]
    public Text timeTxt;
    public Text hpTxt;

    [HideInInspector]
    public float timer = 0;
    [HideInInspector]
    public bool gameOver = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //get our last final score
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        hpTxt.text = "Health: " + 100;
    }

    private void Update()
    {
        if (gameOver == false)
        {
            timer += Time.deltaTime;
            timeTxt.text = "Time: " + timer.ToString("0.#");
        }
    }

    // Spawn one piece of food
    public void Spawn() {


        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);

        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity, foodParent);
    }

    
}
