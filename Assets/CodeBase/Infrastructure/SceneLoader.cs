using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        public void LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name != name)
            {
                AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);
                waitNextScene.completed += _ => onLoaded?.Invoke();
                
            }
            else
            {
                onLoaded?.Invoke();
            }
        }
    }
}