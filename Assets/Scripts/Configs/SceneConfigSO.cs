using HCT.Scripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SceneConfig", menuName = "Configs/SceneConfig")]
public class SceneConfigSO : ScriptableObject
{
    [Serializable]
    public class SceneReference
    {
        public SceneKey Key;
        public string SceneName;
    }

    [SerializeField] private List<SceneReference> scenes;

    private Dictionary<SceneKey, string> _sceneMap;

    private void OnEnable()
    {
        BuildSceneMap();
    }

    private void BuildSceneMap()
    {
        _sceneMap = new Dictionary<SceneKey, string>();

        foreach (SceneReference scene in scenes)
        {
            if (_sceneMap.ContainsKey(scene.Key))
            {
                Debug.LogError($"[SceneConfig] Duplicate key '{scene.Key}' ignored");
                continue;
            }

            _sceneMap.Add(scene.Key, scene.SceneName);
        }
    }

    public string GetSceneName(SceneKey key)
    {
        if (_sceneMap.TryGetValue(key, out string sceneName))
        {
            return sceneName;
        }

        Debug.LogError($"[SceneConfig] Scene with key '{key}' does not exist");
        return null;
    }
}
