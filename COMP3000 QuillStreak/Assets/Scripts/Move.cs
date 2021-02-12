using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    Transform PTransform;
    public float Value;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 ToMove;
    void Start()
    {
        PTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, ToMove, ref velocity, smoothTime); //smooths the movement between changes in the slider value

    }
    public void move(float value) 
    {
        ToMove = transform.position;
        ToMove.x = value;
    }

}
