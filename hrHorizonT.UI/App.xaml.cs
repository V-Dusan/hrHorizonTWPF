using Autofac;
using hrHorizonT.UI.Startup;
using System;
using System.Windows;

namespace hrHorizonT.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            var shellWindow = container.Resolve<ShellWindow>();
            //System.Threading.Thread.Sleep(3000);
            //await Task.Delay(1000);
            shellWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Dogodila se nepredviđena greška. Molimo Vas obavestite tehničku podršku."
                + Environment.NewLine + e.Exception.Message, "Nepredviđena greška");

            e.Handled = true;
        }
    }
}
