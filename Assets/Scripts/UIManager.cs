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

    //Karakterin Puanlar�n� Ekrandaki kendi yerine yazd�ran kod
    public void SetPoint(float _value)
    {
        pointText.text = _value.ToString();
    }

    //UI da bulunan timer �n hesapland��� ve yazd�r�ld��� k�s�m
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
    //Oyun alan�nda bulunan aktif karakterlerin say�s�n� UI da g�steren kod
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
    //Oyun bittiginde tekrar ba�lamam�z� sa�layan kod
    public void SceneLoader()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
