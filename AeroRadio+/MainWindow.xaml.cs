using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using MahApps.Metro.Controls;
using AeroRadio_.Resources.Model;
using System.Xml;
using AeroRadio_.Resources.Model.BassRadio;
using System.Collections.ObjectModel;

namespace AeroRadio_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public ObservableCollection<Albums> Albums;
        public MainWindow()
        {
            InitializeComponent();
            loadGenreLists();
           
        }
       
        private void loadGenreLists()
        {
            leftListBox.Items.Add("Club");
            leftListBox.Items.Add("Pop");
            leftListBox.Items.Add("Rap/HipHop/R&B");
            leftListBox.Items.Add("Рок");
            leftListBox.Items.Add("Шансон");
          
        }


        private void leftListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (leftListBox.SelectedIndex)
            {
                case 0:
                    ParserXMLcs.loadPlayList(@"http://aeroradioplus.ml/PlayList/Club.xml", rightListBox);
                    break;
                case 1:
                    ParserXMLcs.loadPlayList(@"http://aeroradioplus.ml/PlayList/Pop.xml", rightListBox);
                    break;
                case 2:
                    ParserXMLcs.loadPlayList(@"http://aeroradioplus.ml/PlayList/RnB.xml", rightListBox);
                    break;
                case 3:
                    ParserXMLcs.loadPlayList(@"http://aeroradioplus.ml/PlayList/Rock.xml", rightListBox);
                    break;
                case 4:
                    ParserXMLcs.loadPlayList(@"http://aeroradioplus.ml/PlayList/Shanson.xml", rightListBox);
                    break;
            }
        }
        private void Play()
        {
            BassModelLive.startBassStream(ParserXMLcs.list[rightListBox.SelectedIndex].Url, BassModelLive.Volume);
            DataContext = BassTag.titleArtist;
            playButton.Style = FindResource("pauseButton") as Style;

        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            BassModelLive.PlayStop(playButton);
        }


        private void playStationList1(object sender, MouseButtonEventArgs e)
        {
            Play();
            
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if ((rightListBox.SelectedIndex >= 0) && (rightListBox.SelectedIndex < (rightListBox.Items.Count - 1)))
            {
                rightListBox.SelectedIndex++;
                rightListBox.ScrollIntoView(rightListBox.Items[rightListBox.SelectedIndex]);
                Play();
            }
            else
            {
                MessageBox.Show("Конец списка. Действие невозможно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            if (rightListBox.SelectedIndex > 0)
            {
                rightListBox.SelectedIndex--;
                rightListBox.ScrollIntoView(rightListBox.Items[rightListBox.SelectedIndex]);
                Play();
            }
            else
            {
                MessageBox.Show("Начало списка. Действие невозможно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BassModelLive.setVolumeToStream((int)volumeSlider.Value);
         //   volumeLabel.Content = Convert.ToInt16(volumeSlider.Value * 1).ToString();
            double startVolume = volumeSlider.Value / 100.0F;

            if (startVolume == 0)
            {
                volumeImages.Source = new BitmapImage(new Uri(String.Format(@"./Resources/Images/volume-low.png"), UriKind.Relative));
            }
            else if (startVolume > 0 && startVolume <= 0.31)
            {
                volumeImages.Source = new BitmapImage(new Uri(String.Format(@"./Resources/Images/volume-medium.png"), UriKind.Relative));
            }
            else if (startVolume > 0.33 && startVolume == 1)
            {                
                volumeImages.Source = new BitmapImage(new Uri(String.Format(@"./Resources/Images/volume-high.png"), UriKind.Relative));
            }
        }

    }
}

