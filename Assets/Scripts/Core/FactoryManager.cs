using MulticastProject.Interfaces;
using MulticastProject.Scr;
using MulticastProject.Factory;
using UnityEngine;
using Zenject;

namespace MulticastProject.Core
{
    /// <summary>
    /// FactoryManager class for factoryObjects.
    /// </summary>
    public class FactoryManager : MonoBehaviour
    {
        [SerializeField] private PrefabConfig _prefabConfig;

        private IFactoryObject _factory;
        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
            InitializeFactory(_prefabConfig);
        }

        void Awake()
        {
            if (_container == null) InitializeFactory(_prefabConfig);
        }

        private void InitializeFactory(PrefabConfig config)
        {
            _factory = new FactoryObjects(config, _container);
        }

        public T CreateObject<T>(string key, Transform parent) where T : MonoBehaviour
        {
            return _factory.CreateObject<T>(key, parent);
        }
    }
}