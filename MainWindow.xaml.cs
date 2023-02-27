using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;

namespace WPF_Audio_Player
{


    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timerTick);
        }

        void timerTick(object sender, EventArgs e)
        {
            if(mediaPlayer.Source!= null && mediaPlayer.NaturalDuration.HasTimeSpan) { 
                txtCzas.Text = mediaPlayer.Position.ToString(@"mm\:ss");
                TimeSpan ts = mediaPlayer.NaturalDuration.TimeSpan;
                pbGra.Maximum = 100;
                pbGra.Value = ((Double) mediaPlayer.Position.TotalMilliseconds / ts.TotalMilliseconds) * 100;
            }
        }

        private void btnWybierz_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog= new OpenFileDialog();
            dialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                mediaPlayer.Open(new Uri(dialog.FileName));
                txtUtwor.Text = String.Format("Utwór: (0)", dialog.FileName);
                btnPlay.IsEnabled= true;
                btnPause.IsEnabled= true;
                btnStop.IsEnabled= true;
                timer.Start();
            }
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }
        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }
        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }
        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            string kolor = (radio.Content.ToString() == "Niebieski") ? "LightSkyBlue" : "LightGreen";
            pbGra.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(kolor);
        }

    }
}
