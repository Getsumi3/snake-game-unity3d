using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public float playerHpRate;
    [HideInInspector]
    public float foodSpawnRate;
    [HideInInspector]
    public float playerMinSpeed, playerMaxSpeed;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void Trigger_EasyGame()
    {
        playerHpRate = 1.25f;
        foodSpawnRate = 3;
        playerMaxSpeed = 0.15f;
        playerMinSpeed = 0.08f;
        SceneManager.LoadScene(1);
    }

    public void Trigger_NormalGame()
    {
        playerHpRate = 1.5f;
        foodSpawnRate = 5;
        playerMaxSpeed = 0.08f;
        playerMinSpeed = 0.05f;
        SceneManager.LoadScene(1);
    }

    public void Trigger_HardGame()
    {
        playerHpRate = 2f;
        foodSpawnRate = 7;
        playerMaxSpeed = 0.05f;
        playerMinSpeed = 0.01f;
        SceneManager.LoadScene(1);
    }
}
