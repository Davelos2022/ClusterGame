using MulticastProject.Interfaces;
using MulticastProject.Scr;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

namespace MulticastProject.Factory
{
    /// <summary>
    /// Factory for game.
    /// </summary>
    public class FactoryObjects : IFactoryObject
    {
        private readonly DiContainer _container;
        private readonly Dictionary<string, GameObject> _prefabRegistry;

        [Inject]
        public FactoryObjects(PrefabConfig config, DiContainer container)
        {
            _container = container;
            _prefabRegistry = new Dictionary<string, GameObject>();

            foreach (var entry in config.PrefabEntries)
            {
                _prefabRegistry[entry.Key] = entry.Prefab;
            }
        }

        public T CreateObject<T>(string key, Transform parent) where T : MonoBehaviour
        {
            if (!_prefabRegistry.ContainsKey(key))
            {
                throw new ArgumentException($"Prefab for key '{key}' is not registered.");
            }

            GameObject prefab = _prefabRegistry[key];
            GameObject instance = _container.InstantiatePrefab(prefab, parent);

            T component = instance.GetComponent<T>();
            if (component == null)
            {
                component = instance.AddComponent<T>();
            }

            return component;
        }

        public GameObject CreateObject(string key, Transform parent)
        {
            if (!_prefabRegistry.ContainsKey(key))
            {
                throw new ArgumentException($"Prefab for key '{key}' is not registered.");
            }

            GameObject prefab = _prefabRegistry[key];
            return _container.InstantiatePrefab(prefab, parent);
        }
    }
}