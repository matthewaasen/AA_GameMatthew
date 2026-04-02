using System;
using UnityEngine;
using UnityEngine.InputSystem;
//https://discussions.unity.com/t/2d-circular-movement-inside-of-a-circle/808183/3
public class SpokeController : MonoBehaviour
{
    public float throwSpeed = 1f; // Speed of rotation in degrees per second
    public float offset;
    private bool onMiddle;
    private GameObject circle;
    private bool moving;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        onMiddle = false;
        circle = GameObject.Find("Circle");
    }

    // Update is called once per frame
    void Update()
    {
        if(!onMiddle)
        {
           if (!moving)
            {
                if(Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    moving = true;
                }
            }

            if (moving)
            {
                transform.Translate(Vector3.up * Time.deltaTime * throwSpeed);
            }

            if (transform.position.y > offset)
            {
                moving = false;
                transform.SetParent(circle.transform);
                onMiddle = true;
            } 
        }
        
    }
}
