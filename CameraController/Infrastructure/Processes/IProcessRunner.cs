namespace Processes
{
    public interface IProcessRunner
    {
        IProcess Start(string filename, params string[] args);
    }
}
