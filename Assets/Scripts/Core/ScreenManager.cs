using UnityEngine;
using System.Collections.Generic;
using Zenject;
using MulticastProject.Interfases;
using System;

namespace MulticastProject.Core
{
    public class ScreenManager : MonoBehaviour
    {
        private Dictionary<Type, IScreen> _screens = new Dictionary<Type, IScreen>();

        [Inject]
        public void Construct(List<IScreen> screens)
        {
            foreach (var screen in screens) _screens.Add(screen.GetType(), screen);
        }

        public void ShowScreen<T>() where T : IScreen
        {
            foreach (var screen in _screens.Values)   screen.Hide();    
            if (_screens.ContainsKey(typeof(T)))  _screens[typeof(T)].Show();         
        }

        public IScreen GetScreen<T>() where T : IScreen
        {
            if (_screens.TryGetValue(typeof(T), out var screen)) return screen;
            else { Debug.Log($"{typeof(T)} is not find"); return null; }
        }
    }
}
