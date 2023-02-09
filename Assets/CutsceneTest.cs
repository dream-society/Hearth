using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneTest : MonoBehaviour
{
    public static UnityAction<VideoClip, string> CutsceneStart;
    public static UnityAction CutsceneEnd;

    private VideoPlayer player;
    private string sceneName;
    [SerializeField] private VideoClip[] clips;
    private int index = 0;

    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
    }

    private void OnEnable()
    {
        CutsceneStart += OnCutsceneStart;
    }

    private void OnDisable()
    {
        CutsceneStart -= OnCutsceneStart;
    }

    private void Start()
    {
        OnCutsceneStart(clips[index], null);
    }

    private void OnCutsceneStart(VideoClip clip, string scene)
    {
        Debug.Log(index);
        sceneName = scene;
        if (player != null)
        {
            player.enabled = true;
            player.clip = clip;
            player.loopPointReached += EndReached;
            player.Play();
        }
    }

    private void EndReached(VideoPlayer source)
    {
        player.loopPointReached -= EndReached;
        CutsceneEnd?.Invoke();
        index++;
        OnCutsceneStart(clips[index], null);
    }
}
