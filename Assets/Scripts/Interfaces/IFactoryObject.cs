using UnityEngine;

namespace MulticastProject.Interfaces
{
    public interface IFactoryObject
    {
        T CreateObject<T>(string key, Transform parent) where T : MonoBehaviour;
        GameObject CreateObject(string key, Transform parent);
    }
}