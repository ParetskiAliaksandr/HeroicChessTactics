namespace HCT.Scripts.Config.ConfigManagement
{
    public class GameConfig
    {
        public SceneConfigSO SceneConfigSO { get; private set; }

        public GameConfig(SceneConfigSO sceneConfig)
        {
            SceneConfigSO = sceneConfig;
        }
    }
}




