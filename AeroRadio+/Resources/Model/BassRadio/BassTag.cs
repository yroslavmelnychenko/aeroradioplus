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

using System.Windows.Media.Imaging;

namespace AeroRadio_.Resources.Model.BassRadio
{
    public class BassTag
    {
        public static string API_KEY = "29f45b8bf883f3f46ca766b238868e60";
        public static string API_SECRET = "9d8f623cf118dc94b6fb7538709aa14c";
        public static Albums titleArtist = new Albums();
        public static SYNCPROC _mySync;
        public static TAG_INFO _tags;
        public delegate void updateTagDelegate();

        public static void syncStreamTitleUpdates(int channel)
        {
            _tags = new TAG_INFO();
            _mySync = new SYNCPROC(metaSync);
            Bass.BASS_ChannelSetSync(channel, BASSSync.BASS_SYNC_META, 0, _mySync, IntPtr.Zero);
        }

        private static void metaSync(int handle, int channel, int data, IntPtr user)
        {
            user = Bass.BASS_ChannelGetTags(channel, BASSTag.BASS_TAG_META);
            _tags.UpdateFromMETA(user, TAGINFOEncoding.Ansi, false);
            Application.Current.Dispatcher.Invoke(new updateTagDelegate(updateTags));
        }

        public static void updateTags()
        {
            if (BassTags.BASS_TAG_GetFromURL(BassModelLive.audioStreamBass, _tags) && _tags.artist != "" && _tags.artist != ";")
            {
                
                Session session = new Session(API_KEY, API_SECRET);
                titleArtist.Artist = _tags.artist;
                titleArtist.Titles = _tags.title;
                try
                {
                    Album track = new Album(_tags.artist, _tags.title, session);
                    titleArtist.ImgUrl = track.GetImageURL();
                }
                catch
                {
                    Artist track = new Artist(_tags.artist, session);
                    if (track.GetImageURL() != null)
                   {                        
                        titleArtist.ImgUrl = track.GetImageURL();
                   }
                   
                }
            }
            else 
            {
                titleArtist.Artist = _tags.album;
                titleArtist.Titles = _tags.comment;
            }

        }
    }
}
