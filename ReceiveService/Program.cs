using Topshelf;

namespace ReceiveService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.EnableServiceRecovery(rc =>
                {
                    rc.RestartService(1);
                });
                x.Service<RabbitMQService>(s =>
                {
                    s.ConstructUsing(name => new RabbitMQService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });

                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.SetServiceName("RabbitMQService");
                x.SetDisplayName("RabbitMQService Display Name");
                x.SetDescription("RabbitMQService Description");
            });
        }
    }
}