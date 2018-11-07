using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using FilesFinder.Model;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Windows.Media.Imaging;
using System.Security.Principal;
using Application = Microsoft.Office.Interop.Word.Application;
using System.Text;


namespace FilesFinder
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        //sets reference for filter
        private string Filter = null;

        //lists of extentions accepted when filtered
        List<string> Extentions_Image = new List<string>(new string[] { "bmp", "gif", "ico", "jpeg", "jpg", "png" });
        List<string> Extentions_Audio = new List<string>(new string[] { "mp3", "aac", "flac" });
        List<string> Extentions_Document = new List<string>(new string[] { "csv", "dot", "html","md", "odm", "gdoc","dot","dotx","doc","docx","xml" });
        List<string> Extentions_Video = new List<string>(new string[] { "flv", "cam", "mov","mpeg","mkv","webm","gif", "avi", "mpg" });

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


        public string GetWord(string path)
        {
            StringBuilder text = new StringBuilder();
            Application app = new Application();
            app.Visible = false;
            Document doc = app.Documents.Open(path);
            for (int i = 0; i < doc.Paragraphs.Count; i++)
            {
                text.Append(" \r\n " + doc.Paragraphs[i + 1].Range.Text.ToString());
            }
            
            doc.Close(false);         
            app.Quit(false);          
            return text.ToString();


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
                var files = Directory.GetFiles(dlg.SelectedPath).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower()));

                ObservableCollection<FileDetails> allFile = new ObservableCollection<FileDetails>();


                Directory.SetCurrentDirectory(dlg.SelectedPath);

                string currentDirName = Directory.GetCurrentDirectory();
                string[] filesMeta = Directory.GetFiles(currentDirName, "*.*");


                foreach (string f in filesMeta)
                {

                    FileInfo fi = null;

                    try
                    {
                        fi = new FileInfo(f);
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

                    FileInfo[] fileNames = d.GetFiles("*.*");


                    foreach (FileInfo fi in fileNames)
                    {
                        Console.WriteLine("{0}: {1}: {2}", fi.Name, fi.LastAccessTime, fi.Length);
                    }

                    FileDetails id = new FileDetails()
                    {

                        filename = Path.GetFileName(file.ToString())
                    };

                    var FileName = Path.GetFullPath(file.ToString());


                }


                RetrieveList.myList = allFile;

                //Remplie le tableau de donnée avec les fichiers trouvé
                FileList.ItemsSource = allFile;

            }


        }

        //paramètre statique pour garder une liste de fichiers en memoire
        public class RetrieveList
        {
            public static ObservableCollection<FileDetails> myList { get; set; }
            public static string DateList { get; set; }
            public static string DateModificate { get; set; }

            public static string RadiobuttonKeep { get; set; }
         
        }


        List<FileDetails> listFileSearch = new List<FileDetails>();

        List<FileDetails> listFile = new List<FileDetails>();

        List<WordDetails> wordFile = new List<WordDetails>();

        List<WordDetails> wordFileSearch = new List<WordDetails>();


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // ... Get RadioButton reference.
            var button = sender as System.Windows.Controls.RadioButton;       
            // ... Display button content as title.
            Filter = button.Content.ToString();

            RetrieveList.RadiobuttonKeep = Filter;
        }

        
        private void txtNameToSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listFileSearch.Clear();
            wordFileSearch.Clear();
            wordFile.Clear();

            if (RetrieveList.myList != null)
            {
                listFile = RetrieveList.myList.ToList();

                if (RetrieveList.RadiobuttonKeep != null)
                {
                    if (RetrieveList.RadiobuttonKeep.Contains("Documents"))
                {
                    foreach (var list in listFile)
                    {
                        if (list.filename.ToString().Contains(".docx"))
                        {
                            //crée un objet contenant les details de l'image
                            WordDetails id = new WordDetails()
                            {

                                content = GetWord(list.path.ToString()),
                                name = list.filename

                            };
                            wordFile.Add(id);

                        }
                    }

                }
                }

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

                //ajoute les fichiers filtré à la liste fileauthorFiltered
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

                //ajoute les fichiers filtré à la liste listFileSearch
                listFileSearch.AddRange(fileauthorFiltered);

                if (RetrieveList.RadiobuttonKeep != null)
                {
                    if (RetrieveList.RadiobuttonKeep.Contains("Documents"))
                    {
                        var RadioCheck = RetrieveList.RadiobuttonKeep;

                        var WordFiltered = from file in wordFile
                                           let wordfile = file.content

                                           where file.content != null
                                           //filtre avec ce que l'utilisateur a tapé dans la bar de recherche    
                                           where
                                                     wordfile.StartsWith(lower)
                                                  || wordfile.StartsWith(upper)
                                                  || wordfile.Contains(txtOrig)


                                           select file;


                        //ajoute les fichier word filtré à la liste wordFileSearch
                        wordFileSearch.AddRange(WordFiltered);



                        IEnumerable<WordDetails> sansDoublonWord = wordFileSearch.Distinct();

                        FileList.ItemsSource = sansDoublonWord.OrderBy(WordDetails => WordDetails.name).ToObservableCollection();
                    }                  
                }

                else
                {

                    //enleve les doublon du au deux condition where          
                    IEnumerable<FileDetails> sansDoublon = listFileSearch.Distinct();

                    FileList.ItemsSource = sansDoublon.OrderBy(FileDetails => FileDetails.filename).ToObservableCollection();
                }
            }

            else
            {
                System.Windows.MessageBox.Show("Y faut des fichiers connard");
            }

                      
        }

    }
}
