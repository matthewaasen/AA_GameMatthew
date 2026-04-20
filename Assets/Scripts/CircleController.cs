using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CircleController : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject spokePrefab;
    
    public float offset;
    public float clockwise; // 1 for clockwise, -1 for counterclockwise
    public float[][] levels = new float[][]
    {
        //level, spokes, ammo, speed, direction
        new float[] {1,  2,  2,  50,  1},
        new float[] {2,  2,  5,  50,  1},
        new float[] {3,  3,  5,  50,  1},
        new float[] {4,  3,  5,  50,  1},
        new float[] {5,  3,  7,  50,  1},
        new float[] {6,  4,  7,  50,  1},
        new float[] {7,  4,  7,  75, 1},
        new float[] {8,  4,  8,  75, 1},
        new float[] {9,  5,  8,  75, 1},
        new float[] {10, 5,  8,  75, -1},
        new float[] {11, 5,  9, 75, -1},
        new float[] {12, 5,  9, 100, -1},
        new float[] {13, 5,  10, 100, -1},
        new float[] {14, 5,  12, 100, 1},
        new float[] {15, 6,  12, 100, 1},
        new float[] {16, 6,  13, 100, -1},
        new float[] {17, 6,  13, 115, -1},
        new float[] {18, 6,  14, 115, -1},
        new float[] {19, 10,  1, 175, 1},
        new float[] {20, 7,  14, 115, -1},

    };
    public int level;

    public GameObject currentSpoke;
    public int ammoLeft;
    public TMPro.TextMeshProUGUI ammoText;
    public TMPro.TextMeshProUGUI levelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = GameData.level;
        if(level >= 0)
        {
            rotationSpeed = levels[level][3];
            clockwise = levels[level][4];

            //create spokes on wheel
            for(int i = 0; i < levels[level][1]; i++)
            {
                float angle = i * (360f / levels[level][1]);
                GameObject spoke = Instantiate(spokePrefab, transform.position, Quaternion.Euler(0, 0, angle));
                spoke.transform.SetParent(transform);
                spoke.GetComponent<SpokeController>().onMiddle = true;
            }

            //set ammo
            ammoLeft = (int)levels[level][2];
            //create first Ammo
            if(SceneManager.GetActiveScene().name == "Game"){
                GameObject ammo = Instantiate(spokePrefab, new Vector2(0, -2f), Quaternion.identity);
                ammo.GetComponent<SpokeController>().onMiddle = false;
                currentSpoke = ammo;
            }
            
        }
        else //Menu Screen setting
        {
            rotationSpeed = 100;
            clockwise = 1;
        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime * clockwise);
        if(SceneManager.GetActiveScene().name == "Game" && ammoLeft <= 0 && currentSpoke == null)
        {
            GameData.level++;
            if(level >= levels.Length-1)
            {
                SceneManager.LoadScene("YouWin");
            }
            else
            {
                SceneManager.LoadScene("InBetweenLevels");
            }
            
        }
        if(SceneManager.GetActiveScene().name == "Game")
        {
            ammoText.text = (ammoLeft).ToString() + " left";
            levelText.text = (level+1).ToString();
        }
        
    }

    public void StartGame()
    {
        if(level < 0)
        {
            GameData.level = 0;
        }
        if(Keyboard.current.digit4Key.isPressed) //skips to level 20
        {
            GameData.level = 19;
        }
        if(Keyboard.current.digit3Key.isPressed) //skips to level 19
        {
            GameData.level = 18;
        }
        if(Keyboard.current.digit1Key.isPressed) //skips to level 11
        {
            GameData.level = 10;
        }
        if(Keyboard.current.digit2Key.isPressed) //skips to level 16
        {
            GameData.level = 15;
        }
        SceneManager.LoadScene("Game");
    }
}
