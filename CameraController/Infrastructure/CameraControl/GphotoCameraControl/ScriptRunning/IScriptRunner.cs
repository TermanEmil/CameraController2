using Processes;

namespace GphotoCameraControl.ScriptRunning
{
    public interface IScriptRunner
    {
        IProcess RunAutoDetection();
    }
}