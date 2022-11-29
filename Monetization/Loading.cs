using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Loading : MonoBehaviour
{
    //Tämä scripti on Loading sceneä varten. Kopioitu teemmun linkkaamasta gdpr videosta
    [SerializeField] float loadingDelay = 2f;
    [SerializeField] GameObject gDPR_Popup;
    [SerializeField] string privacyPolicyURL;
    [SerializeField] TextMeshProUGUI loadingText;
    InterstitialOnLaunch interstitial;

    void Start()
    {
        interstitial = FindObjectOfType<InterstitialOnLaunch>();
        CheckForGDPR();
        //interstitial.ShowInterstitialOnLaunch();
        Invoke("StartGame", loadingDelay);
    }

    void StartGame()
    {        
        SceneManager.LoadSceneAsync(1);
    }

    void CheckForGDPR()
    {
        if (PlayerPrefs.GetInt("npa", -1) == -1)
        {
            gDPR_Popup.SetActive(true);
            loadingText.enabled = false;
            Time.timeScale = 0;
        }
    }

    public void OnUserClickAccept()
    {
        PlayerPrefs.SetInt("npa", 0);
        gDPR_Popup.SetActive(false);
        loadingText.enabled = true;
        Time.timeScale = 1;
    }
    public void OnUserClickCancel()
    {
        PlayerPrefs.SetInt("npa", 1);
        gDPR_Popup.SetActive(false);
        loadingText.enabled = true;
        Time.timeScale = 1;
    }
    public void OnUserClickPrivacyPolicy()
    {
        Application.OpenURL(privacyPolicyURL);
    }
}
