using UnityEngine;
using UnityEngine.Events;

namespace HNC
{
    public class SceneTransition : MonoBehaviour
    {
        public static UnityAction TransitionFadeOut;
        public static UnityAction TransitionFadeIn;
        public static UnityAction TransitionFadeOutEnd;

        private Animator anim;

        private void Awake() => anim = GetComponent<Animator>();
        private void OnEnable()
        {
            TransitionFadeOut += OnTransitionFadeOut;
            TransitionFadeIn += OnTransitionFadeIn;
        }

        private void OnDisable()
        {
            TransitionFadeOut -= OnTransitionFadeOut;
            TransitionFadeIn -= OnTransitionFadeIn;
        }
        private void OnTransitionFadeOut() => anim.SetTrigger("FadeOut");
        private void OnTransitionFadeIn() => anim.SetTrigger("FadeIn");

        private void OnEnd()
        {
            TransitionFadeOutEnd?.Invoke();
        }
    }
}
