using Client.Core.Receivers;
using Client.Core.Receivers.Factories;

namespace Client.Console.Receivers.Factories
{
    public class StringReceiverFactory : IStringReceiverFactory
    {
        public IStringReceiver Create() => new StringReceiver();
    }
}
