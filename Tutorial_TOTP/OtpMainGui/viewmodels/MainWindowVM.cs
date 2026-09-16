using OtpApplication;
using OtpCore.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;

namespace OtpMainGui.viewmodels
{
    public class MainWindowVM:BasicVM
    {
        public string  username { get; set; }
        public string  password { get; set; }
        public string  code { get; set; }
        public string  tbCode { get; set; }

        public BitmapImage qrImageRegister { get; set; }
        public BitmapImage qrImageShow { get; set; }

        private OtpApplicationService applicationService;
        public MainWindowVM(OtpApplicationService applicationService)
        {
            this.applicationService = applicationService;
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000; // ~ 10 seconds
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            tbCode = applicationService.getOptCodeOfAllCredentials();
            OnPropertyChanged(nameof(tbCode));
        }


        public void register()
        {
            CredentialsDto credentialsDto = new CredentialsDto();
            credentialsDto.username  = username;
            credentialsDto.password = password;
            applicationService.registerNewUser(credentialsDto);
            buildQRImageRegister();
        }

        public void show()
        {
            CredentialsDto credentialsDto = applicationService.getCredentialsOfUsername(username);
            if (credentialsDto != null && credentialsDto.password.Equals(password))
            {
                buildQRImageShow();
            }
        }

        public void logon()
        {
            bool result = applicationService.validateUser(username, code);
            if (result)
                MessageBox.Show("Erfolgreich", "Status", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Falsch", "Status", MessageBoxButton.OK, MessageBoxImage.Warning);

        }
        public void clearRegister()
        {
            qrImageRegister = null;
            OnPropertyChanged(nameof(qrImageRegister));
        }

        public void clearShow()
        {
            qrImageShow = null;
            OnPropertyChanged(nameof(qrImageShow));
        }


        private void buildQRImageRegister()
        {
            BitmapImage btm;
            byte[] imageData = applicationService.getQRImageAsByteArray(username, password);
            if (imageData == null || imageData.Length == 0) return;
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                btm = new BitmapImage();
                btm.BeginInit();
                btm.StreamSource = ms;
                // Below code for caching is crucial.
                btm.CacheOption = BitmapCacheOption.OnLoad;
                btm.EndInit();
                btm.Freeze();
            }
            qrImageRegister = btm;
            OnPropertyChanged(nameof(qrImageRegister));
        }

        private void buildQRImageShow()
        {
            BitmapImage btm;
            byte[] imageData = applicationService.getQRImageAsByteArray(username, password);
            if (imageData == null || imageData.Length == 0) return;
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                btm = new BitmapImage();
                btm.BeginInit();
                btm.StreamSource = ms;
                // Below code for caching is crucial.
                btm.CacheOption = BitmapCacheOption.OnLoad;
                btm.EndInit();
                btm.Freeze();
            }
            qrImageShow = btm;
            OnPropertyChanged(nameof(qrImageShow));
        }

    }



}
