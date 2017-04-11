using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroRadio_.Resources.Model
{
   public class Albums : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string _artist;
        public string Artist
        {
            get
            {
                return _artist;
            }
            set
            {
                _artist = value;
                onPropertyChanged("Artist");
            }
        }

        string _title;
        public string Titles
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                onPropertyChanged("Titles");
            }
        }

        string _imgURL;
        public string ImgUrl
        {
            get
            {
                return _imgURL;
            }
            set
            {
                _imgURL = value;
                onPropertyChanged("ImgUrl");
            }
        }

        string _customUrl;
        public string CustomUrl
        {
            get
            {
                return _customUrl;
            }
            set
            {
                _customUrl = value;
                onPropertyChanged("CustomUrl");
            }
        } 

        void onPropertyChanged(params string[] propertyNames)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                foreach (var pn in propertyNames)
                {
                    handler(this, new PropertyChangedEventArgs(pn));
                }
            }
        }
    }
}
