using RoaREngine;
using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool oneShot;
    [SerializeField] private bool autoMove;
    [SerializeField] private float Duration;
    [SerializeField] private int TargetToStart;
    [SerializeField] private Transform[] TargetsPosition;
    private bool isMoving;
    private int index;

    private void Awake()
    {
        index = TargetToStart;
    }

    private void Start()
    {
        if (autoMove)
        {
            Move();
        }
    }

    public void Move()
    {
        if (isMoving)
        {
            return;
        }

        RoarManager.CallPlay("MovingPlatform", transform);
        isMoving = true;
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
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / Duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
        RoarManager.CallStop("MovingPlatform");
        if (!oneShot)
        {
            Move();
        }
    }
}
