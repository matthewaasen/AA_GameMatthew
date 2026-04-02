using UnityEngine;

public class CircleController : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject spokePrefab;
    public float offset;
    public float clockwise; // 1 for clockwise, -1 for counterclockwise
    public float[][] levels = new float[][]
    {
        //level, spokes, ammo, speed, direction
        new float[] {1, 10, 5, 100, 1}
    };
    public int level;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = 0;
        rotationSpeed = levels[level][3];
        clockwise = levels[level][4];

        //create spokes on wheel
        for(int i = 0; i < levels[level][1]; i++)
        {
            float angle = i * (360f / levels[level][1]);
            GameObject spoke = Instantiate(spokePrefab, transform.position, Quaternion.Euler(0, 0, angle));
            spoke.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime * clockwise);
    }
}
