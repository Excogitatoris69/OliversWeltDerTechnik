using OtpAdapter;
using OtpApplication;
using OtpCore.interfaces;
using OtpMainGui.viewmodels;
using PersistenceAdapter;
using System.Configuration;
using System.Data;
using System.Windows;

namespace OtpMainGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private OtpApplicationService otpApplicationService = null;


        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            if(otpApplicationService!=null)
                otpApplicationService.saveData();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string pathDatafile = @"E:\eigenes\Projekte\Tutorial_TOTP\testdaten\otpdata.csv";
            string issuer = "OliversOtpDemo";

            otpApplicationService = new OtpApplicationService(
                new OtpAdapterImpl(),
                new PersistenceAdapterImpl(pathDatafile),
                issuer
                );
            otpApplicationService.init();
            openMainView();
        }

        private void openMainView()
        {
            MainWindowVM mainWindowVM = new MainWindowVM(otpApplicationService);
            mainWindowVM.windowTitle = "Olivers OTP-Demo v1.0";
            mainWindowVM.windowLeft = 200;
            mainWindowVM.windowTop = 200;
            mainWindowVM.windowWidth = 450;
            mainWindowVM.windowHeight = 700;
            MainWindow mainWindow = new MainWindow(mainWindowVM);
            mainWindow.Show();
        }
    }

}
