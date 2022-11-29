using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLaunchCounter : MonoBehaviour
{  
    //tämä scripti tallentaa pelin käynnistyskerrat
    void Awake()
    {
        if (PlayerPrefs.HasKey("LaunchCount"))
        {
            int launchCount = PlayerPrefs.GetInt("LaunchCount");
            launchCount += 1;
            PlayerPrefs.SetInt("LaunchCount", launchCount);

        }
        else PlayerPrefs.SetInt("LaunchCount", 1);
        Destroy(this);
    }
}
