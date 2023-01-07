using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneLoader : MonoBehaviour
{
    public VideoClip Clip;
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        VideoPlayerManager.CutsceneStart.Invoke(Clip, SceneName);
    }
}
