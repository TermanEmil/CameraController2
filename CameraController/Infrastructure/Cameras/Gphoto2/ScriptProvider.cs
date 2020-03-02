namespace Cameras.Gphoto2
{
    public interface IScriptProvider
    {
        string GetScriptForAutoDetect();
    }

    public class ScriptProvider : IScriptProvider
    {
        public string GetScriptForAutoDetect()
        {
            return "auto-detect.sh";
        }
    }
}
