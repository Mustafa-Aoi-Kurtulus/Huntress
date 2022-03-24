using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Huntress.Core
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Transform parentObject;
        [SerializeField] int objectToPoolNumber;
        Queue<GameObject> pool;
        void Awake()
        {
            pool = new Queue<GameObject>();
            for (int i = 0; i <= objectToPoolNumber; i++)
            {
                GameObject cloneObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity, parentObject);
                cloneObject.SetActive(false);
                pool.Enqueue(cloneObject);
            }
        }
        public GameObject PoolObject()
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);

            pool.Enqueue(obj);
            return obj;
        }
    }
}
