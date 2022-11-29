using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Tämä scripti on Loading sceneä varten. Kopioitu teemmun linkkaamasta gdpr videosta
    [SerializeField] GameObject panelMenu;
    void Awake()
    {
       panelMenu.SetActive(false);
    }

    public void ShowMenu()
    {
        panelMenu.SetActive(true);
    }

    public void HideMenu()
    {
        panelMenu.SetActive(false);
    }

    public void OnUserClickedAdsSettings()
    {
        PlayerPrefs.SetInt("npa", -1);
        SceneManager.LoadScene(0);
    }
}
