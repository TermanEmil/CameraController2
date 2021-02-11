using Processes;

namespace GphotoCameraControl.ScriptRunning
{
    public interface IScriptRunner
    {
        IProcess RunAutoDetection();
        IProcess RunCaptureImage(string filename, string port);
        IProcess RunCapturePreview(string port);
    }
}