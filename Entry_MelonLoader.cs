using MelonLoader;

namespace YetAnotherSilkSongPlugin
{
    public class Entry_MelonLoader : MelonMod
    {
        public static MelonLogger.Instance logger;
        public override void OnInitializeMelon()
        {
            logger = LoggerInstance;
            logger.Msg(string.Format("{0} loaded!", ModConfig.NAME));
        }
    }
}
