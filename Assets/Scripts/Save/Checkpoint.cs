using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DS.Save
{
    // TODO: add Interactable interface
    public class Checkpoint : MonoBehaviour
    {
        private bool _interacted = false;

        public void Interact()
        {
            if (_interacted)
            {
                return;
            }

            _interacted = true;

        }
    }
}
