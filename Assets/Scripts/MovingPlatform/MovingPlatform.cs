using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float Duration;
    public int TargetToStart;
    public Transform[] TargetsPosition;
    private int index;

    private void Awake()
    {
        index = TargetToStart;
    }

    public void Move()
    {
        Vector3 startPosition = TargetsPosition[index].position;
        index = index == TargetsPosition.Length - 1 ? 0 : index += 1;
        Vector3 targetPosition = TargetsPosition[index].position;
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