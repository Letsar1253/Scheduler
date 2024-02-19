using Client.Core.Receivers;

namespace Client.Console.Receivers
{
    internal class StringReceiver : IStringReceiver
    {
        public string Receive(string request)
        {
            string? str;
            do
            {
                System.Console.WriteLine(request);
                str = System.Console.ReadLine();
            }
            while (str is null);

            return str;
        }
    }
}
