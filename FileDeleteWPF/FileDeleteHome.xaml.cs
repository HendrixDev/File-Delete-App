using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using FileDeleteWPF.Properties;

namespace FileDeleteWPF
{
    /// <summary>
    /// Interaction logic for FileDeleteHome.xaml
    /// </summary>
    public partial class FileDeleteHome : Page
    {
        public string folderPath1;
        public string folderPath2;
        public string firstFile;
        public string secondFile;
        public string newFile;
        public List<FileInfo> File1Info = new List<FileInfo>();
        public List<FileInfo> File2Info = new List<FileInfo>();
        FolderBrowserDialog dlg = new FolderBrowserDialog();
        int deleted = 0;

        public FileDeleteHome()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            dlg.ShowDialog();
            folderPath2 = dlg.SelectedPath;
            textBox.Text = folderPath2;
            DirectoryInfo d2 = new DirectoryInfo(folderPath2);
            var tempFiles = d2.GetFiles();
            foreach (var item in tempFiles)
            {
                File2Info.Add(item);
            }
            textBox.Tag = folderPath2;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            dlg.ShowDialog();
            folderPath1 = dlg.SelectedPath;
            textBox2.Text = folderPath1;
            DirectoryInfo d1 = new DirectoryInfo(folderPath1);
            var tempFiles = d1.GetFiles();
            foreach (var item in tempFiles)
            {
                File1Info.Add(item);
            }
            textBox2.Tag = folderPath1;
        }

        public void Button4_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < File1Info.Count(); i++)
            {
                firstFile = File1Info[i].ToString().ToLower();//add first file to list to be checked.

                for (int j = 0; j < File2Info.Count(); j++)
                {
                    secondFile = File2Info[j].ToString().ToLower();//add second file to list to be checked.
                    if (firstFile == secondFile)//if file names are identical, delete them.
                    {
                        deleted++;
                        File.SetAttributes(textBox.Tag + "\\" + secondFile, FileAttributes.Normal);//Changing file permissions to allow deletion.
                        File.Delete(textBox.Tag + "\\" + secondFile);
                    }
                }
            }

            MessageBoxResult result = System.Windows.MessageBox.Show($"{deleted} files were deleted from {textBox.Tag}", "Success", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)//reset variables to use app again.
            {
                deleted = 0;
                textBox.Clear();
                folderPath2 = "";
                secondFile = "";
                newFile = "";
                File2Info.Clear();
                Settings.Default.Save();
            }
        }
    }
}
