using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject firstMenu, optionsMenu;
    public static bool isOptions = false;

    public Slider masterVol, musicVol, sfxVol;
    public AudioSource bgMusic;

    private PlayerPrefs MasterVolume, MusicVolume, sfxVolume;

    public void GoToOptions()
    {
        firstMenu.gameObject.SetActive(false);
        optionsMenu.SetActive(true);
        isOptions = true;
    }

    public void GoBack()
    {
        firstMenu.gameObject.SetActive(true);
        optionsMenu.SetActive(false);
        isOptions = false;
    }

    public void StartGame()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {

        bgMusic.volume = (masterVol.value * musicVol.value) / 2;

    }
}
