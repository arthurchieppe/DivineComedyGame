using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    Image lifeBar;
    int maxHealth = 100;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        lifeBar = GetComponent<Image>();
        health = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(health/(float)maxHealth);
        lifeBar.fillAmount = PlayerCombat.currentHealth/(float)maxHealth;
    }
}
