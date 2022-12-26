using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using RadioButton = System.Windows.Controls.RadioButton;
using System.Reflection;
using System.Windows;

namespace ClashMarcacao
{
    class XmlReader
    {


        XmlDocument _xmlFile = new XmlDocument();
        List<string> _xmlContent = new List<string>();
        string _xmlPath = "";

        public XmlReader(string xmlPath)
        {
            _xmlPath = xmlPath;
            XmlParser(_xmlPath);
        }

        private void XmlParser(string xmlXpath)
        {
            try
            {
                _xmlFile.Load(_xmlPath);

                XmlNodeList listNode = _xmlFile.SelectNodes("Marcadores")[0].ChildNodes;

                foreach (XmlElement elemento in listNode)
                {
                    _xmlContent.Add(elemento.InnerText);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public List<string> XmlContent
        {
            get
            {
                return _xmlContent;
            }
        }

        public void populaObjectList(ManualWindow manual)
        {
            List<string> list = XmlContent;
            foreach (string item in list)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Content = item;
                manual.listObj.Items.Add(radioButton);
            }
        }
    }
}