using UnityEngine;

public class MenuController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI levelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Level: " + (GameData.level).ToString() + " complete!";
    }
}
