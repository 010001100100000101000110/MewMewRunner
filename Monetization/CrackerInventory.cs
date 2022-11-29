using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackerInventory : MonoBehaviour
{
    //t‰m‰ scripti tallentaa crackereiden m‰‰r‰‰

    [SerializeField] int crackerStartingAmount;
    private void Start()
    {
        CreateCrackerInventory();
    }

    void CreateCrackerInventory()
    {
        if (!PlayerPrefs.HasKey("CrackerCount"))
        {            
            PlayerPrefs.SetInt("CrackerCount", crackerStartingAmount);
        }
    }

    public void AddToCrackerCount(int amount)
    {
        int crackerCount = PlayerPrefs.GetInt("CrackerCount");
        crackerCount += 1;
        PlayerPrefs.SetInt("CrackerCount", crackerCount);
    }

    public void SubtractFromCrackerCount(int amount)
    {
        int crackerCount = PlayerPrefs.GetInt("CrackerCount");
        crackerCount -= 1;
        PlayerPrefs.SetInt("CrackerCount", crackerCount);
    }
}
