using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleController : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject spokePrefab;
    
    public float offset;
    public float clockwise; // 1 for clockwise, -1 for counterclockwise
    public float[][] levels = new float[][]
    {
        //level, spokes, ammo, speed, direction
        new float[] {1, 2, 2, 50, 1}
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
            GameObject ammo = Instantiate(spokePrefab, new Vector2(0, -2f), Quaternion.identity);
            ammo.GetComponent<SpokeController>().onMiddle = false;
            currentSpoke = ammo;
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
            SceneManager.LoadScene("InBetweenLevels");
        }
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            ammoText.text = (ammoLeft).ToString();
            levelText.text = (level+1).ToString();
        }
        
    }

    public void StartGame()
    {
        if(level < 0)
        {
            GameData.level = 0;
        }
        SceneManager.LoadScene("Game");
    }
}
