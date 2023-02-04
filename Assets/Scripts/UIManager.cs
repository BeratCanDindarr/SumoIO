using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI activedPlayerText;
    public TextMeshProUGUI timerText;
    
    public GameObject pauseButton;
    public GameObject playButton;

    //Karakterin Puanlarýný Ekrandaki kendi yerine yazdýran kod
    public void SetPoint(float _value)
    {
        pointText.text = _value.ToString();
    }

    //UI da bulunan timer ýn hesaplandýðý ve yazdýrýldýðý kýsým
    public void SetTimer(float _value)
    {
        float timer;
        if (_value % 60 == 0)
        {
            timer = _value % 60;
            Debug.Log(timer);
            timerText.text = timer.ToString();
        }
        else
        {
            int a = (int)_value % 60;
            Debug.Log(a);
            int b = (int)_value / 60;
            //Debug.Log(b);
            timerText.text = b + ":" + a;

        }
       
    }
    //Oyun alanýnda bulunan aktif karakterlerin sayýsýný UI da gösteren kod
    public void SetActivedPlayer(int _value)
    {
        activedPlayerText.text = _value.ToString();
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        playButton.SetActive(true);
    }
    public void PlayButton()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        playButton.SetActive(false);
    }
    //Oyun bittiginde tekrar baþlamamýzý saðlayan kod
    public void SceneLoader()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
