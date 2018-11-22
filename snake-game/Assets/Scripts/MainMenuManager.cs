using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    [Header("UI")]
    public Text scoreTxt;
    public Text lastScoreTxt;
    public Slider volumeSlider;

    // Use this for initialization
    void Start () {
        //get our last final score
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        volumeSlider.value = AudioListener.volume;
        scoreTxt.text = "Your best: " + PlayerPrefs.GetFloat("Best score").ToString("0.#");
        lastScoreTxt.text = "Your last: " + PlayerPrefs.GetFloat("Last score").ToString("0.#");

    }

    public void Trigger_Volume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void Trigger_Start()
    {
        SceneManager.LoadScene(1);
    }

    public void Trigger_Exit()
    {
        Application.Quit();
    }
}
