using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrackerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI crackerAmountText;

    private void Start()
    {
        crackerAmountText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        CrackerAmountToText();
    }

    private void CrackerAmountToText()
    {
        crackerAmountText.text = PlayerPrefs.GetInt("CrackerCount").ToString();
    }
}
