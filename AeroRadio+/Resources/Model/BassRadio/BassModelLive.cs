using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Windows;
using AeroRadio_;

namespace AeroRadio_.Resources.Model.BassRadio
{
    public class BassModelLive
    {
        private static int HZ = 44100;

        private static bool intDeviceDefault;

        public static int audioStreamBass;

        public static int Volume = 100;
                      
        
        private static bool InitBass(int hz)
        {
            if (!intDeviceDefault)
            {
                intDeviceDefault = Bass.BASS_Init(-1, hz, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            }
            return  intDeviceDefault;
        }

        public static async void startBassStream(string files , int vol)
        {
            
            await Task.Run(() =>
            {
                Bass.BASS_StreamFree(audioStreamBass);

                if (InitBass(HZ))
                {                    
                    Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_ASYNCFILE_BUFFER, 5000);
                    Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 5000);
                    audioStreamBass = Bass.BASS_StreamCreateURL(files, 0, BASSFlag.BASS_STREAM_AUTOFREE | BASSFlag.BASS_STREAM_STATUS, null, IntPtr.Zero);
                    BassTag.syncStreamTitleUpdates(audioStreamBass);
                }
                if (audioStreamBass != 0 && Bass.BASS_ChannelPlay(audioStreamBass, true))
                {
                    Volume = vol;
                    Bass.BASS_ChannelSetAttribute(audioStreamBass, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100.0F);
                    BassTag.updateTags();
                }
                else
                {
                    MessageBox.Show("Радиостанция в данный момент недоступна.\nПроверьте интернет-соединение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
           
        }

        public static void PlayStop(Button StartStop)
        {
            if (audioStreamBass != 0)
            {                
                 if (Bass.BASS_ChannelIsActive(audioStreamBass) == BASSActive.BASS_ACTIVE_PLAYING)
                {
                    Bass.BASS_ChannelPause(audioStreamBass);
                    StartStop.Style = Application.Current.FindResource("playButton") as Style;
                }
                else
                {
                    Bass.BASS_ChannelPlay(audioStreamBass, false);
                    StartStop.Style = Application.Current.FindResource("pauseButton") as Style;
                }

            }
            else
            {
                MessageBox.Show("Клпикните 2 раза по станции", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }          
        

        public static void setVolumeToStream(int vol)
        {
            Volume = vol;
            Bass.BASS_ChannelSetAttribute(audioStreamBass, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100.0F);
        }
    }
}
