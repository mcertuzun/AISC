using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Object = System.Object;

namespace Util
{
    [System.Serializable]
    public class PoolTest : MonoBehaviour
    {
        [Header("POOL VALUES")]
        [SerializeField] private List<GameObject> pooledList;
        public int spawnedCount;
        public int pooledCount;
        [Space]
        public GameObject prefab;
        public SpawnType spawnType;
        private float timer;
        public enum SpawnType
        {
            OnClick,
            Auto,
        }

        void Update()
        {
            timer += Time.deltaTime;
            
            if (spawnType == SpawnType.OnClick)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(prefab.InternalDeSpawnAfterDelay(transform.position, 1));
                }
            }
            pooledList = ObjectPool.instance.pooledObjects[prefab];
            spawnedCount = ObjectPool.CountSpawned(prefab);
            pooledCount = ObjectPool.CountPooled(prefab);
            
            if (spawnType == SpawnType.Auto && timer > 0.1f)
            {
                StartCoroutine(prefab.InternalDeSpawnAfterDelay(transform.position, 1));
                timer = 0;
            }
        }
        
     
    }
}