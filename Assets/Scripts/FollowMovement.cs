using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocity;
    void Start()
    {
        
    }

    // Update is called once per frame
  void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + velocity, transform.position.z);
    }
}
