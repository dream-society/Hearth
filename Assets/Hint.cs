using Hearth.Player;
using System.Collections;
using UnityEngine;

public class Hint : InteractableBase
{
    private SpriteRenderer sp;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float duration = 3f;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(startPosition.x, startPosition.y + 1.5f, startPosition.z);
    }

    public override void Interact()
    {
        interacted = true;
        SpawnHint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!interacted)
        {
            if (collision.GetComponent<CharacterRun>() != null)
            {
                Interact();
            }
        }
    }

    private void SpawnHint()
    {
        StartCoroutine(SpawnHintCoroutine());
    }

    private IEnumerator SpawnHintCoroutine()
    {
        float time = 0.0f;
        float aColor = 0.0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            if (time < duration / 2f)
            {
                aColor += Time.deltaTime;
            }
            else
            {
                aColor -= Time.deltaTime;
            }
            sp.color = new Color(1, 1, 1, aColor);
            yield return null;
        }
    }
}
