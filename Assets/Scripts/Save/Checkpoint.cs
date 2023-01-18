using Hearth.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DS.Save
{
    // TODO: add Interactable interface
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private SaveSystem saveSystem;

        private bool _interacted = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Interact(collision.transform);
        }

        public void Interact(Transform playerTransform)
        {
            if (_interacted)
            {
                return;
            }

            _interacted = true;
            CharacterRun cr = playerTransform.gameObject.GetComponent<CharacterRun>();
            PlayerPowerManagement ppm = playerTransform.gameObject.GetComponent<PlayerPowerManagement>();
            SaveSystem.PlayerSave.Invoke(SceneManager.GetActiveScene(), playerTransform, cr.PlasticBottles, ppm.haveGazzaPower);
        }
    }
}
