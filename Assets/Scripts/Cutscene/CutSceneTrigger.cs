using Hearth.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneTrigger : MonoBehaviour
{
    private bool interacted;
    [SerializeField] Transform cutSceneLoader;
    [SerializeField] VideoClip Clip;
    [SerializeField] string SceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !interacted)
        {
            cutSceneLoader.GetComponent<CutsceneLoader>().Clip = Clip;
            cutSceneLoader.GetComponent<CutsceneLoader>().SceneName = SceneName;
            Instantiate(cutSceneLoader);
        }
    }
}
