using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    Transform PTransform;
    public float Value;
    void Start()
    {
        PTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void move(float value) 
    {
        transform.position = new Vector3(value, 0,0);
    }
}
