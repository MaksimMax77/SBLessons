using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Pool
{
    public class ObjectsPool : MonoBehaviour
    {
        private Dictionary<Type, Queue<MonoBehaviour>> _pooledObjects = new();
        public T GetOrCreateObject<T>(T type) where T : MonoBehaviour
        {
            var obj = GetObject(type);

            if (obj == null)
            {
                obj = Instantiate(type);
            }
            obj.gameObject.SetActive(true);
            return obj;
        }
        
        private T GetObject<T>(T type) where T : MonoBehaviour
        {
            if (!_pooledObjects.TryGetValue(type.GetType(), out var pooledObjectsQueue) 
                || !pooledObjectsQueue.TryDequeue(out var pooledObject))
            {
                return null;
            }
            
            return (T) pooledObject;
        }
        
        public void PutAndDisable<T>(T obj) where T : MonoBehaviour
        {
            obj.gameObject.SetActive(false);
            if (_pooledObjects.TryGetValue(obj.GetType(), out var pooledObjectsQueue))
            {
                pooledObjectsQueue.Enqueue(obj);
            }
            else
            {
                pooledObjectsQueue = new Queue<MonoBehaviour>();
                pooledObjectsQueue.Enqueue(obj);
                _pooledObjects.Add(obj.GetType(), pooledObjectsQueue);
            }
        }
    }
}