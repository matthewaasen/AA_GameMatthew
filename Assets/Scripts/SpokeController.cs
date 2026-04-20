using System;
using UnityEngine;
using UnityEngine.InputSystem;
//https://discussions.unity.com/t/2d-circular-movement-inside-of-a-circle/808183/3
public class SpokeController : MonoBehaviour
{
    public float throwSpeed = 1f; // Speed of rotation in degrees per second
    public float offset;
    public bool onMiddle;
    private GameObject circle;
    private bool moving;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circle = GameObject.Find("Circle");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Spoke" && other.GetComponent<SpokeController>().onMiddle)
        {
            //Game Over
            
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            GameData.level = -2;
        }
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
                transform.Translate(-Vector3.down * Time.deltaTime * throwSpeed);
            }

            if (transform.position.y > offset)
            {
                transform.position = new Vector2(transform.position.x, offset);
                moving = false;
                transform.SetParent(circle.transform);
                onMiddle = true;
                circle.GetComponent<CircleController>().ammoLeft--;
                if(circle.GetComponent<CircleController>().ammoLeft > 0)
                {
                    //create new Ammo
                    GameObject ammo = Instantiate(circle.GetComponent<CircleController>().spokePrefab, new Vector2(0, -2f), Quaternion.identity);
                    ammo.GetComponent<SpokeController>().onMiddle = false;
                    circle.GetComponent<CircleController>().currentSpoke = ammo;
                }
                else
                {
                    circle.GetComponent<CircleController>().currentSpoke = null;
                }
            } 


        }
        
    }
}
