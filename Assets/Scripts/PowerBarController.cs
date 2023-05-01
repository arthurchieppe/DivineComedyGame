using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerBarController : MonoBehaviour
{
    Image powerBar;
    int maxPower = 5;
    public int power;
    // Start is called before the first frame update
    void Start()
    {
        powerBar = GetComponent<Image>();
        power = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(health/(float)maxHealth);
        powerBar.fillAmount = power/(float)maxPower;
        if (power == maxPower){
            power = 0;
        }
    }
}
