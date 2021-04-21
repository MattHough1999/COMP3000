using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    Transform PTransform;
    public  Animator animator;
    
    public float Value;
    public float smoothTime = 0.3F;
    public Vector3 velocity = Vector3.zero;
    private Vector3 ToMove;
    float i = 0.00f;
    //public Rigidbody rb;

    void Start()
    {
        PTransform = transform;
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        transform.position = Vector3.SmoothDamp(transform.position, ToMove, ref velocity, smoothTime); //smooths the movement between changes in the slider value
       
        animator.SetFloat("Velocity", velocity.x);
    }
    public void move(float value) 
    {
        
        ToMove = transform.position;
        ToMove.x = value;
        
    }

}
