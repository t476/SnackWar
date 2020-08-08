using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu _instance;

    public GameObject pauseMenu;
    public GameObject RuleMenu;
    public GameObject FinishMenu;
    public AudioMixer audioMixer;
    public Text finalText;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        GetComponent<AudioSource>().Play();

    }

    public void PlayGame()
    {
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void UIEnable()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);

    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;//暂停，慢速
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void SetVolume(float value)//混音器
    {
        audioMixer.SetFloat("MainVolume", value);
    }
    public void Rule()
    {
        RuleMenu.SetActive(true);

    }
    public void ReturnGame()
    {
        RuleMenu.SetActive(false);

    }
    public void FinishGame()
    {
        FinishMenu.SetActive(true);
        finalText = GameManager.Instance.scoreText;//butaihaoyong
        int finalscore = GameManager.Instance.score;
        if (finalscore >= 0 && finalscore < 1500)
            GameObject.Find("Canvas/FinalPanel/Image2").SetActive(true);
        if (finalscore >= 1500 && finalscore < 4000)
            GameObject.Find("Canvas/FinalPanel/Image1").SetActive(true);
        if (finalscore > 4000)
            GameObject.Find("Canvas/FinalPanel/Image").SetActive(true);

    }
}
