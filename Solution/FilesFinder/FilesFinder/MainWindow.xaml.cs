using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using FilesFinder.Model;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Windows.Input;
using System.Windows.Shapes;
using Spire.Doc;
using Spire.Pdf;
using Spire.Doc.Documents;
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
        List<string> Extentions_Audio = new List<string>(new string[] { "mp3", "aac", "flac", "ogg" });
        List<string> Extentions_Document = new List<string>(new string[] { "csv", "dot", "html", "md", "odm", "gdoc", "dot", "dotx", "doc", "docx", "xml" });
        List<string> Extentions_Video = new List<string>(new string[] { "flv", "cam", "mov", "mpeg", "mkv", "webm", "gif", "avi", "mpg" });

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
            Document document = new Document();
            document.LoadFromFile(path);


            foreach (Section section in document.Sections)
            {
                foreach (Paragraph paragraph in section.Paragraphs)
                {
                    text.AppendLine(paragraph.Text);
                }
            }        
            return text.ToString();

        }

        public string GetPDF(string path)
        {

            //Create a pdf document.
            
            PdfDocument doc = new PdfDocument();
           
            doc.LoadFromFile(path);
          
            StringBuilder buffer = new StringBuilder();
                              
            foreach (PdfPageBase page in doc.Pages)
                
            {              
                buffer.Append(page.ExtractText());
                                            
            }

            return buffer.ToString();
        }


        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            

            FolderBrowserDialog dlg = new FolderBrowserDialog();

            //ouvre l'exlporateur de fichier
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //defini le dossier choisi
                DirectoryInfo d = new DirectoryInfo(dlg.SelectedPath);

         

                ObservableCollection<FileDetails> allFile = new ObservableCollection<FileDetails>();
                Directory.SetCurrentDirectory(dlg.SelectedPath);
                string currentDirName = Directory.GetCurrentDirectory();
                string[] filesMeta = Directory.GetFiles(currentDirName, "*.*", SearchOption.AllDirectories);



                foreach (string f in filesMeta)
                {
                    if (!f.ToString().Contains(".ini") && !f.ToString().Contains("~"))
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
                }
               int num = allFile.Count;

                RetrieveList.myList = allFile;
        
                //Remplie le tableau de donnée avec les fichiers trouvé
                FileList.ItemsSource = allFile;

                NumberArray.Text = num.ToString() + " élément"+ (num > 1 ? "s" : "") +" trouvé"+(num > 1 ? "s" : "" );

            }
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
                               
                FileDetails fl = FileList.SelectedItem as FileDetails;
                System.Diagnostics.Process.Start(fl.path.ToString());
                                        
        }

        List<FileDetails> listFileSearch = new List<FileDetails>();

        List<FileDetails> listFile = new List<FileDetails>();

        List<WordDetails> wordFile = new List<WordDetails>();

        List<WordDetails> wordFileSearch = new List<WordDetails>();

        List<PDFdetails> PDFFile = new List<PDFdetails>();

        List<PDFdetails> PDFFileSearch = new List<PDFdetails>();

        

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
            PDFFile.Clear();
            PDFFileSearch.Clear();
         

            if (RetrieveList.myList != null)
            {
                listFile = RetrieveList.myList.ToList();

                {
                    if (RetrieveList.RadiobuttonKeep.Contains("Documents"))
                    {
                        foreach (var list in listFile)
                        {
                            if (list.filename.ToString().Contains(".doc"))
                            {
                                //crée un objet contenant les details de l'image
                                WordDetails id = new WordDetails()
                                {

                                    content = GetWord(list.path.ToString()),
                                    name = list.filename

                                };
                                wordFile.Add(id);

                            }


                            if (list.filename.ToString().Contains(".pdf"))
                            {
                                //crée un objet contenant les details de l'image
                                PDFdetails id = new PDFdetails()
                                {

                                    content = GetPDF(list.path.ToString()),
                                    name = list.filename

                                };

                                PDFFile.Add(id);
                                
                            }
                        }
                    }

                    //assigne la valeur tapé dans la bar de recherche à la variable txtOrig
                    string txtOrig = txtNameToSearch.Text;

                    //Convertie la valeur tapé dans la bar de recherche en majuscule
                    string upper = txtOrig.ToUpper();

                    //Convertie la valeur tapé dans la bar de recherche en minuscule
                    string lower = txtOrig.ToLower();


                    if (RetrieveList.RadiobuttonKeep != "Tout")
                    {
                        if (RetrieveList.RadiobuttonKeep.Contains("Documents"))
                        {
                            //  var RadioCheck = RetrieveList.RadiobuttonKeep;

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

                           

                            //  var RadioCheck = RetrieveList.RadiobuttonKeep;

                        }


                        if (RetrieveList.RadiobuttonKeep.Contains("PDF"))
                        {

                            var PDFFiltered = from file in PDFFile
                                              let PDFFile = file.content

                                              where file.content != null
                                              //filtre avec ce que l'utilisateur a tapé dans la bar de recherche    
                                              where
                                                        PDFFile.StartsWith(lower)
                                                     || PDFFile.StartsWith(upper)
                                                     || PDFFile.Contains(txtOrig)

                                              select file;


                            //ajoute les fichier pdf filtré à la liste wordFileSearch
                            PDFFileSearch.AddRange(PDFFiltered);

                            IEnumerable<PDFdetails> sansDoublonPDF = PDFFileSearch.Distinct();

                            FileList.ItemsSource = sansDoublonPDF.OrderBy(PDFdetails => PDFdetails.name).ToObservableCollection();

                        }

                        if (RetrieveList.RadiobuttonKeep.Contains("Images"))
                        {
                            foreach (var list in listFile)
                            {
                                string extensions = System.IO.Path.GetExtension(list.path);
                                string chemin = System.IO.Path.GetFullPath(list.path);

                            }

                        }
                    }

                    else
                    {
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
                        //enleve les doublon du au deux condition where          
                        IEnumerable<FileDetails> sansDoublon = listFileSearch.Distinct();

                        FileList.ItemsSource = sansDoublon.OrderBy(FileDetails => FileDetails.filename).ToObservableCollection();
                    }
                }
            }

            else
            {
                System.Windows.MessageBox.Show("Veuillez choisir un dossier");
            }


        }

    }
}
