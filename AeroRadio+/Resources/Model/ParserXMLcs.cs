using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;

namespace AeroRadio_.Resources.Model
{
  public class ParserXMLcs
    {
        public static  List<Stations> list = new List<Stations>();

        public static void loadPlayList(string xmlFiles,ListBox rigrightListBox)
        {         
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlFiles);
            XmlNodeList xmlNodeList = xDoc.SelectNodes("//stations/station");
            foreach(XmlNode Node in xmlNodeList)
            {
                Stations echo = new Stations();
                echo.Name = Node.Attributes.GetNamedItem("name").Value;
                echo.Images = Node.Attributes.GetNamedItem("images").Value;
                echo.Url = Node.Attributes.GetNamedItem("url").Value;
                list.Add(echo);                
            }
            foreach (Stations s in list)
                rigrightListBox.Items.Add(s);
        }
       
    }
}
