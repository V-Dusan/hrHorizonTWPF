using Autofac;
using hrHorizonT.DataAccess;
using hrHorizonT.UI.Data;
using hrHorizonT.UI.ViewModel;

namespace hrHorizonT.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<hrHorizonTDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<FriendDataService>().As<IFriendDataService>();


            return builder.Build();
        }
    }
}
