using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScriptFin : MonoBehaviour
{
    public TMP_Text textoVictoria;
    int playerScore;

    void Start()
    {
        textoVictoria.enabled = false;
    }
    
    void OnTriggerEnter(Collider other)
    {

        PlayerController controller = other.gameObject.GetComponent<PlayerController>();

        if (controller != null)
        {
            textoVictoria.enabled = true;

            controller.score += 100;
            playerScore = controller.score;

            Invoke("loadMenu", 3);
            //Debug.Log("Attacked Player");
        }
    }

    void loadMenu() {
        List<int> puntuaciones = new List<int>();

        for (int i = 0; i < MenuController.numPuntuaciones; i++)
        {
            puntuaciones.Add(PlayerPrefs.GetInt("BestScore" + (i+1), 0));
        }

        puntuaciones.Add(playerScore);

        puntuaciones.Sort();
        puntuaciones.Reverse();

        for (int i = 0; i < MenuController.numPuntuaciones; i++)
        {
            PlayerPrefs.SetInt("BestScore" + (i+1), puntuaciones[i]);
        }
        SceneManager.LoadScene("Menu");
    }
}
