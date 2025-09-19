using HCT.Scripts.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SceneConfig", menuName = "Configs/SceneConfig")]
public class SceneConfigSO : ScriptableObject 
{
    [Serializable] public class SceneReference
    {
        public string Key;
        public string SceneName;
    }

    [SerializeField] private List<SceneReference> scenes;

    private Dictionary<string, string> _sceneMap;

    [Inject] private ILoggerService _loggerService;

    [Inject]
    public void Initialize(ILoggerService logger)
    {
        _loggerService = logger;

        BuildSceneMap();
    }

    private void BuildSceneMap()
    {
        _sceneMap = new Dictionary<string, string>();

        foreach (SceneReference scene in scenes)
        {
            if (_sceneMap.ContainsKey(scene.Key))
            {
                _loggerService.LogError($"[SceneConfig] Duplicate key '{scene.Key}' ignored");
                continue;
            }

            _sceneMap.Add(scene.Key, scene.SceneName);
        }
    }

    public string GetSceneName(string key)
    {
        if (_sceneMap.TryGetValue(key, out string sceneName))
        {
            return sceneName;
        }

        _loggerService.LogError($"[SceneConfig] Scene with key '{key}' does not exist");
        return null;
    }
}
