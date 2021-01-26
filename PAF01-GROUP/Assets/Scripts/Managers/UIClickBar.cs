using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClickBar : MonoBehaviour
{
    public float maxAmountClick;
    private float clickAmount;

    public Transform progressBar;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerController>().OnPlayerClick += GetValue;
    }

    private void Update()
    {
        SetValue(clickAmount);
    }

    void SetValue(float clickAmount)
    {
        progressBar.GetComponent<Image>().fillAmount = clickAmount / maxAmountClick;
    }

    void GetValue()
    {
        clickAmount++;
        if (clickAmount >= maxAmountClick)
            FindObjectOfType<PlayerController>().OnPlayerClick -= GetValue;
    }
}
