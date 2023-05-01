using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class EndGameScreen : MonoBehaviour
{
    public void goMainMenu() {
        SceneManager.LoadScene("MainMenu");
        PlayerCombat.ResetPlayerStats();
        
    }
}
