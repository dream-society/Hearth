using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    private float length;
    private float startPos;
    [SerializeField] private float parallax;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        var cam = Camera.main;

        float threshold = cam.transform.position.x * (1 - parallax);
        float dist = cam.transform.position.x * parallax;

        transform.position = new Vector3(startPos + dist, cam.transform.position.y, transform.position.z);

        if (threshold > startPos + length)
        {
            startPos += length;
        }
        else if (threshold < startPos - length)
        {
            startPos -= length;
        }
    }
}
