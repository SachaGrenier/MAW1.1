﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using FilesFinder.Model;
using System.IO;
using System.Windows.Media.Imaging;
using System.Security.Principal;

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
        public string GetTags(string path)
        {
            var fs = File.GetAccessControl(path);

            var sid = fs.GetOwner(typeof(SecurityIdentifier));
           
            var ntAccount = sid.Translate(typeof(NTAccount));

            //découpe le nom avant et après le character "_"
            var name = ntAccount.Value.Split('\\');

            return name[1];

        }


            private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            string[] supportedExtensions = new[] { ".bmp", ".jpeg", ".jpg", ".png", ".tiff", ".doc", ".txt", ".docx", ".xlsx" };

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            //ouvre l'exlporateur de fichier
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //defini le dossier choisi
                DirectoryInfo d = new DirectoryInfo(dlg.SelectedPath);


                //récupère les données de chaque fichier
                var files = Directory.GetFiles(dlg.SelectedPath).Where(s => supportedExtensions.Contains(System.IO.Path.GetExtension(s).ToLower()));

                ObservableCollection<FileDetails> allFile = new ObservableCollection<FileDetails>();


                System.IO.Directory.SetCurrentDirectory(dlg.SelectedPath);

                string currentDirName = System.IO.Directory.GetCurrentDirectory();
                string[] filesMeta = System.IO.Directory.GetFiles(currentDirName, "*.*");


                foreach (string f in filesMeta)
                {

                    System.IO.FileInfo fi = null;

                    try
                    {
                        fi = new System.IO.FileInfo(f);


                    }

                    catch
                    {
                        continue;
                    }

                    //crée un objet contenant les details de l'image
                    FileDetails id = new FileDetails()
                    {
                        filename = fi.Name,
                        path = fi.ToString(),
                        author = GetTags(fi.ToString())

                };
                    allFile.Add(id);


                }


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
                        
                        filename = System.IO.Path.GetFileName(file.ToString())
                    };

                    var FileName = System.IO.Path.GetFullPath(file.ToString());


                }

                RetrieveList.myList = allFile;

                //Remplie le tableau de donnée avec les fichiers trouvé
                FileList.ItemsSource = allFile;

            }


        }

        //paramètre statique pour garder une liste d'image, une date de prise de vue et une date de modification en memoire
        public class RetrieveList
        {
            public static ObservableCollection<FileDetails> myList { get; set; }
            public static string DateList { get; set; }
            public static string DateModificate { get; set; }
        }


        List<FileDetails> listFileSearch = new List<FileDetails>();


        List<FileDetails> listFile = new List<FileDetails>();

        private void txtNameToSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listFileSearch.Clear();



            listFile = RetrieveList.myList.ToList();

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

            var fileauthorFiltered = from file in listFile
                                     let authorfile = file.author

                                     where file.author != null
            //filtre avec ce que l'utilisateur a tapé dans la bar de recherche    
                               where
                                         authorfile.StartsWith(lower)
                                      || authorfile.StartsWith(upper)
                                      || authorfile.Contains(txtOrig)


                               select file;

            //ajoute les images filtré à la liste listeImageSearch
            listFileSearch.AddRange(fileauthorFiltered);

            //enleve les doublon du au deux condition where          
            IEnumerable<FileDetails> sansDoublon = listFileSearch.Distinct();


            FileList.ItemsSource = sansDoublon.OrderBy(FileDetails => FileDetails.filename).ToObservableCollection();
        }

    }
}
