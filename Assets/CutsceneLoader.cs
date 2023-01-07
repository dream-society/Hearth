using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneLoader : MonoBehaviour
{
    [SerializeField] private VideoClip clip;
    [SerializeField] private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        VideoPlayerManager.CutsceneStart.Invoke(clip, sceneName);
    }
}
