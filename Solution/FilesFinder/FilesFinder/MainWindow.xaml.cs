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
                var files = Directory.GetFiles(dlg.SelectedPath).Where(s => supportedExtensions.Contains(System.IO.Path.GetExtension(s).ToLower()));

                ObservableCollection<FileDetails> allFile = new ObservableCollection<FileDetails>();
                var shellAppType = Type.GetTypeFromProgID("Shell.Application");
                dynamic shellApp = Activator.CreateInstance(shellAppType);
                var folder = shellApp.NameSpace(files);
                System.IO.DriveInfo di = new System.IO.DriveInfo(@"C:\");
                System.IO.DirectoryInfo dirInfo = di.RootDirectory;

                //parcours le tableau de données
                foreach (var file in files)
                {

                    System.IO.FileInfo[] fileNames = d.GetFiles("*.*");


                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        Console.WriteLine("{0}: {1}: {2}", fi.Name, fi.LastAccessTime, fi.Length);
                    }

                    FileDetails id = new FileDetails()
                    {
                        path = file.ToString(),
                        filename = System.IO.Path.GetFileName(file.ToString())
                    };

                    var FileName = System.IO.Path.GetFullPath(file.ToString());

                    //crée un objet avec les paramètres voulue
                    FileDetails Meta = new FileDetails()
                    {
                       

                    };


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

            //assigne la valeur tapé dans la bar de recherche à la variable txtOrig
            string txtOrig = txtNameToSearch.Text;

            //Convertie la valeur tapé dans la bar de recherche en majuscule
            string upper = txtOrig.ToUpper();

            //Convertie la valeur tapé dans la bar de recherche en minuscule
            string lower = txtOrig.ToLower();

            //requete pour filtrer les fichier
            var fileFiltered = from file in listFile
                                  let enamefile = file.filename

                                  //filtre avec ce que l'utilisateur a tapé dans la bar de recherche    
                                  where
                                            enamefile.StartsWith(lower)
                                         || enamefile.StartsWith(upper)
                                         || enamefile.Contains(txtOrig)


                                  select file;

            //ajoute les images filtré à la liste listeImageSearch
            listFileSearch.AddRange(fileFiltered);

           
            FileList.ItemsSource = listFileSearch.OrderBy(FileDetails => FileDetails.filename).ToObservableCollection();
        }

    }
}
