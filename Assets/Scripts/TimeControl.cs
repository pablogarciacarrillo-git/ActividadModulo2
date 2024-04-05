using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeControl : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        buttonText.text = "Pausar";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetTime(float newTime)
    {
        Time.timeScale = newTime;
    }

    public void ToggleTime()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            buttonText.text = "Pausar";
        } else
        {
            Time.timeScale = 0;
            buttonText.text = "Reanudar";
        }
    }
}
