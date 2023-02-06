using RoaREngine;
using System.Collections;
using TMPro;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool active = false;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    private void Start()
    {
        transform.position = start.position;
    }

    void FixedUpdate()
    {
        if (!active) return;

        Vector3 direction = (target.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            SwapTarget();
        }
    }

    void SwapTarget()
    {
        Transform tmp = target;
        target = start;
        start = tmp;
    }

    public void Move()
    {
        active = true;
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    velocity *= -1f;
    //}

    //private void Start()
    //{
    //    index = TargetToStart;
    //    NextTarget();

    //    if (autoMove)
    //    {
    //        isMoving = true;
    //    }
    //}

    //private void Update()
    //{
    //    if (!isMoving)
    //    {
    //        return;
    //    }

    //    time += Time.deltaTime;

    //    //transform.position = Vector3.Lerp(startPosition, targetPosition, time / Duration);
    //    transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f * Time.deltaTime);

    //    if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
    //    {
    //        transform.position = targetPosition;
    //        NextTarget();
    //    }
    //}

    //private void NextTarget()
    //{
    //    startPosition = TargetsPosition[index].position;
    //    transform.position = startPosition;

    //    index = index == TargetsPosition.Length - 1 ? 0 : index += 1;
    //    targetPosition = TargetsPosition[index].position;
    //}

    //public void Move()
    //{
    //    isMoving = true;
    //}

    //public void Move()
    //{
    //    if (isMoving)
    //    {
    //        return;
    //    }

    //    RoarManager.CallPlay("MovingPlatform", transform);
    //    isMoving = true;
    //    Vector3 startPosition = TargetsPosition[index].position;
    //    index = index == TargetsPosition.Length - 1 ? 0 : index += 1;
    //    Vector3 targetPosition = TargetsPosition[index].position;
    //    StartCoroutine(MoveCoroutine(startPosition, targetPosition));
    //}

    //private IEnumerator MoveCoroutine(Vector3 startPosition, Vector3 targetPosition)
    //{
    //    float time = 0f;
    //    while (time < Duration)
    //    {
    //        transform.position = Vector3.Lerp(startPosition, targetPosition, time / Duration);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    //transform.position = targetPosition;
    //    isMoving = false;
    //    RoarManager.CallStop("MovingPlatform");
    //    if (!oneShot)
    //    {
    //        Move();
    //    }
    //}
}
