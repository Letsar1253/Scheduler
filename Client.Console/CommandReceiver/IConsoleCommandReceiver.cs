namespace Client.Console.CommandReceiver
{
    public interface IConsoleCommandReceiver
    {
        void DoReceive(CancellationToken cancellationToken);
    }
}
