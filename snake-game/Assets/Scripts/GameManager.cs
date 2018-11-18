using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
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
    public Text scoreTxt;
    public Text lastScoreTxt;
    public Text timeTxt;
    public Text hpTxt;
    public GameObject mainMenu;

    [HideInInspector]
    public float timer = 0;
    public bool gameOver = true;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //get our last final score
        scoreTxt.text = "Your best: " + PlayerPrefs.GetFloat("Best score").ToString("0.#");
        lastScoreTxt.text = "Your last: " + PlayerPrefs.GetFloat("Last score").ToString("0.#");
        hpTxt.text = "Health: " + 100;
    }

    private void Update()
    {
        if (!gameOver)
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

    public void Trigger_Start()
    {
        mainMenu.SetActive(false);
        gameOver = false;
    }

    public void Trigger_Exit()
    {
        Application.Quit();

        //in case you'll want to reset saves
        //PlayerPrefs.DeleteAll();
    }
}
