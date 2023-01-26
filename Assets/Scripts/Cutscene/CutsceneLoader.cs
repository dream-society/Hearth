using HNC;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneLoader : MonoBehaviour
{
    [SerializeField] private VideoClip clip;
    [SerializeField] private string sceneName;

    private void OnEnable()
    {
        SceneTransition.TransitionFadeOutEnd += OnTransitionFadeOutEnd;
    }

    private void OnDisable()
    {
        SceneTransition.TransitionFadeOutEnd -= OnTransitionFadeOutEnd;
    }

    private void OnTransitionFadeOutEnd()
    {
        VideoPlayerManager.CutsceneStart.Invoke(clip, sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneTransition.TransitionFadeOut?.Invoke();
    }


}
