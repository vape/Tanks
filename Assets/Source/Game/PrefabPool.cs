using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanks.Game
{
    public interface IPoolSpawnedHandler
    {
        void OnSpawned();
    }

    public static class PrefabPool
    {
        private class PrefabPoolContainer : MonoBehaviour
        {
            private void OnDestroy()
            {
                OnContainerDestroying(transform);
            }
        }

        private static Dictionary<GameObject, HashSet<GameObject>> pool = new Dictionary<GameObject, HashSet<GameObject>>();
        private static Dictionary<Scene, Transform> containers = new Dictionary<Scene, Transform>();
        private static Dictionary<GameObject, GameObject> instancePrefabReferences = new Dictionary<GameObject, GameObject>();

        public static T Instantiate<T>(T component)
            where T: Component
        {
            return Instantiate(component.gameObject).GetComponent<T>();
        }

        public static GameObject Instantiate(GameObject obj)
        {
            if (pool.TryGetValue(obj, out var objects) && objects.Count > 0)
            {
                var gameObject = objects.First();
                gameObject.SetActive(true);

                foreach (var component in gameObject.GetComponents<Component>())
                {
                    var poolSpawnHandler = component as IPoolSpawnedHandler;
                    if (poolSpawnHandler != null)
                    {
                        poolSpawnHandler.OnSpawned();
                    }
                }

                objects.Remove(gameObject);

                return gameObject;
            }

            var instance = GameObject.Instantiate(obj);
            instancePrefabReferences.Add(instance, obj);
            return instance;
        }

        public static void Destroy<T>(T component)
            where T: Component
        {
            Destroy(component.gameObject);
        }

        public static void Destroy(GameObject obj)
        {
            if (!instancePrefabReferences.TryGetValue(obj, out var prefab))
            {
                Debug.LogWarning($"Failed to find base prefab for given instance, {obj.name} has been spawned outside prefab pool");
                Destroy(obj);
                return;
            }

            obj.SetActive(false);

            if (!pool.TryGetValue(prefab, out var set))
            {
                set = new HashSet<GameObject>();
                pool.Add(prefab, set);
            }

            if (!containers.TryGetValue(obj.scene, out var container))
            {
                container = new GameObject("Pool Container").transform;
                container.gameObject.AddComponent<PrefabPoolContainer>();

                containers.Add(obj.scene, container);
            }

            obj.transform.SetParent(container);
            set.Add(obj);
        }

        private static void OnContainerDestroying(Transform container)
        {
            var removed = new HashSet<GameObject>();

            for (int i = 0; i < container.childCount; ++i)
            {
                var child = container.GetChild(i);

                if (instancePrefabReferences.TryGetValue(child.gameObject, out var prefab))
                {
                    instancePrefabReferences.Remove(child.gameObject);

                    if (pool.TryGetValue(prefab, out var instances))
                    {
                        foreach (var instance in instances)
                        {
                            if (removed.Contains(instance))
                            {
                                continue;
                            }

                            removed.Add(instance);
                            GameObject.Destroy(instance);
                        }

                        instances.Clear();
                    }
                }
            }

            ClearNullReferences();
            containers.Remove(container.gameObject.scene);
        }

        private static void ClearNullReferences()
        {
            var toRemove = new HashSet<GameObject>();

            foreach (var kv in instancePrefabReferences)
            {
                if (kv.Key == null)
                {
                    toRemove.Add(kv.Key);
                }
            }

            foreach (var key in toRemove)
            {
                instancePrefabReferences.Remove(key);
            }
        }
    }
}
