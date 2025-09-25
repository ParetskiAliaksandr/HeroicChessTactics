namespace HCT.Scripts.Config.ConfigManagement
{
    public class GameConfig
    {
        public SceneConfigSO SceneConfig { get; private set; }

        public GameConfig(SceneConfigSO sceneConfig)
        {
            SceneConfig = sceneConfig;
        }
    }
}




