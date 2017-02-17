using Lastfm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.XPath;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;
namespace AeroRadio_.Resources.Model.BassRadio
{
   public class BassTag
    {
       public static Albums titleArtist = new Albums();
        public static SYNCPROC _mySync;
        public static TAG_INFO _tags;
        public delegate void updateTagDelegate();

        public static void syncStreamTitleUpdates( int channel)
        {
            _tags = new TAG_INFO();
            _mySync = new SYNCPROC(metaSync);
            Bass.BASS_ChannelSetSync(channel, BASSSync.BASS_SYNC_META, 0, _mySync, IntPtr.Zero);                
        }

        private static void metaSync(int handle , int channel, int data , IntPtr user)
        {
            user = Bass.BASS_ChannelGetTags(channel, BASSTag.BASS_TAG_META);
            _tags.UpdateFromMETA(user, TAGINFOEncoding.Ansi, false);
            Application.Current.Dispatcher.Invoke(new updateTagDelegate(updateTags));
        }
        
        public static void updateTags()
        {            
            if (BassTags.BASS_TAG_GetFromURL(BassModelLive.audioStreamBass, _tags))
            {
               
                titleArtist.Artist = _tags.artist;
                titleArtist.Titles = _tags.title;
                
            }
        }
            
    }
}
