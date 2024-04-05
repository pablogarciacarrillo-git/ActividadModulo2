using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject panelInicial;
    public GameObject panelControles;
    public GameObject panelMejorPuntuacion;
    public static int numPuntuaciones = 10;
    public TMP_Text textMejorPuntuacion;

    // Start is called before the first frame update
    void Start()
    {
        panelInicial.SetActive(true);
        panelControles.SetActive(false);
        panelMejorPuntuacion.SetActive(false);

        string textPuntuaciones = "";

        for (int i = 0; i < numPuntuaciones; i++)
        {
            textPuntuaciones = textPuntuaciones + (i+1) + "º: " + PlayerPrefs.GetInt("BestScore" + (i+1), 0) + "\n";
        }

        textMejorPuntuacion.SetText(textPuntuaciones);

        /*
        int puntuacion1 = PlayerPrefs.GetInt("BestScore1", 0);
        int puntuacion2 = PlayerPrefs.GetInt("BestScore2", 0);
        int puntuacion3 = PlayerPrefs.GetInt("BestScore3", 0);
        
        textMejorPuntuacion.SetText("1º: " + puntuacion1 
                            + "\n\n\n2º: " + puntuacion2 
                            + "\n\n\n3º: " + puntuacion3);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadStage1()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void openControles()
    {
        panelInicial.SetActive(false);

        panelControles.SetActive(true);
    }

    public void closeControles()
    {
        panelInicial.SetActive(true);

        panelControles.SetActive(false);
    }

    public void openPuntuaciones()
    {
        panelInicial.SetActive(false);

        panelMejorPuntuacion.SetActive(true);
    }

    public void closePuntuaciones()
    {
        panelInicial.SetActive(true);

        panelMejorPuntuacion.SetActive(false);
    }
}
