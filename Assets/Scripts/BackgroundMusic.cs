using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;

        // GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        // if (objs.Length > 1) {
        //     Destroy(this.gameObject);
        // }

        DontDestroyOnLoad(this.gameObject);
    }


}
