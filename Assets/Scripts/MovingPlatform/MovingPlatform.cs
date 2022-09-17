using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float Duration;
    public Transform[] targetsPosition;
    public int IndexToStart;
    private int index;

    private void Start()
    {
        index = IndexToStart;
        Move();
    }

    public void Move()
    {
        Vector3 startPosition = targetsPosition[index].position;
        index = index == targetsPosition.Length - 1 ? 0 : index += 1;
        Vector3 targetPosition = targetsPosition[index].position;
        StartCoroutine(MoveCoroutine(startPosition, targetPosition));
    }

    private IEnumerator MoveCoroutine(Vector3 startPosition, Vector3 targetPosition)
    {
        float time = 0f;
        while (time < Duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time/Duration);  
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        Move();
    }
}
