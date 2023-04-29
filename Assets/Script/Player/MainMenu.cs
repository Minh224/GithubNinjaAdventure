using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;
    private float value;
    private void Start()
    {
        mixer.GetFloat("volume" , out value );
        volumeSlider.value = value;
    }
    public void SetVolume()
    {
        mixer.SetFloat("volume", volumeSlider.value);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
