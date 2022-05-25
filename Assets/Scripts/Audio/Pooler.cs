using System.Collections.Generic;
using UnityEngine;

namespace DS.Audio
{

    public class Pooler : MonoBehaviour
    {
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int poolSize;
        private List<GameObject> pooledObjects;

        private void Awake()
        {
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectToPool);
                obj.SetActive(false);
                obj.transform.parent = this.transform;
                pooledObjects.Add(obj);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    pooledObjects[i].SetActive(true);
                    return pooledObjects[i];
                }
            }

            // If all instance are busy, instantiate another gameObject and add it to the pool.
            // It's better to increment pool size
            var obj = Instantiate(objectToPool);
            pooledObjects.Add(obj);
            poolSize = pooledObjects.Count;
            obj.SetActive(true);
            Debug.LogWarning($"Pooler: Instatiate another {objectToPool}, pool size: {poolSize}");

            return obj;
        }
    }
}
