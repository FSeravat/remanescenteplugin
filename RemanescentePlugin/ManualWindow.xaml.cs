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
using System.IO;
using System.Windows.Forms;
using System.Xml;
using RadioButton = System.Windows.Forms.RadioButton;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Reflection;

namespace ClashMarcacao
{
    /// <summary>
    /// Interaction logic for ManualWindow.xaml
    /// </summary>

    public partial class ManualWindow
    {

        public ManualWindow()
        {
            InitializeComponent();
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string caminhoArquivo = Path.Combine(assemblyFolder, "Maker.xml");
            XmlReader xml = new XmlReader(caminhoArquivo);
            xml.populaObjectList(this);
        }


        public void manualWindow_Loaded(object sender, EventArgs e)
        {
            this.Show();
        }

        public void manualWindow_Loaded()
        {
            this.Show();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}