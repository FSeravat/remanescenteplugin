using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NWDTierExporter
{
    class FileOperations
    {

        public FileOperations()
        {

        }


        public void CsvCreator(List<string[]> entrada, string docName, string headerPrefix)
        {

            FolderBrowserDialog folderdialog = new FolderBrowserDialog();
            string outPath = "";
            string fullname = "";
            if (folderdialog.ShowDialog() == DialogResult.OK)
            {
                outPath = folderdialog.SelectedPath;
                fullname = outPath + "\\" + docName + ".csv";
            }

            if (outPath != "")
            {
                using (StreamWriter fs = new StreamWriter(fullname, false, Encoding.UTF8))
                {
                    int maior = entrada.Max(item => item.Length);

                    //Header
                    List<string> header = new List<string>();

                    for (int col = 1; col <= maior; col++)
                    {
                        header.Add($"{headerPrefix}{col}");
                    }

                    fs.WriteLine(String.Join(";", header));

                    foreach (string[] hier in entrada)
                    {
                        fs.WriteLine(String.Join(";", hier));
                    }
                }
            }

        }

    }
}
