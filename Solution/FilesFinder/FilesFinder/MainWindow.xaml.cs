using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using FilesFinder.Model;
using System.IO;

namespace FilesFinder
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            string[] supportedExtensions = new[] { ".bmp", ".jpeg", ".jpg", ".png", ".tiff", ".doc", ".txt", ".docx" };

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            //ouvre l'exlporateur de fichier
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //defini le dossier choisi
                DirectoryInfo d = new DirectoryInfo(dlg.SelectedPath);

                //récupère les données de chaque fichier
                FileInfo[] infos = d.GetFiles();

                //récupère les données de chaque fichier
                var files = Directory.GetFiles(dlg.SelectedPath).Where(s => supportedExtensions.Contains(System.IO.Path.GetExtension(s).ToLower()));

                ObservableCollection<FileDetails> allFile = new ObservableCollection<FileDetails>();

                //parcours le tableau de données
                foreach (var file in files)
                {

                    FileDetails id = new FileDetails()
                    {
                        Path = file.ToString(),
                        filename = System.IO.Path.GetFileName(file.ToString())
                    };

                    var FileName = System.IO.Path.GetFullPath(file.ToString());


                    allFile.Add(id);

                }
                //Remplie le tableau de donnée avec les fichiers trouvé
                FileList.ItemsSource = allFile;

            }


        }

        List<FileDetails> listFileSearch = new List<FileDetails>();
        List<FileDetails> listFile = new List<FileDetails>();
        private void txtNameToSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listFileSearch.Clear();
        }

    }
}
