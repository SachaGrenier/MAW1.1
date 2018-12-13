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
using Spire.Doc;
using Spire.Xls;
using Spire.Doc.Documents;
using System.Diagnostics;
using PdfSharp.Pdf;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace FilesFinder
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>   
    public partial class MainWindow : System.Windows.Window
    {
        List<FileDetails> listFileSearch = new List<FileDetails>();

        List<FileDetails> listFile = new List<FileDetails>();

        List<WordDetails> wordFile = new List<WordDetails>();

        List<WordDetails> wordFileSearch = new List<WordDetails>();

        List<PDFdetails> PDFFile = new List<PDFdetails>();

        List<PDFdetails> PDFFileSearch = new List<PDFdetails>();

        List<ImageDetails> imageFile = new List<ImageDetails>();

        List<ImageDetails> imageFileSearch = new List<ImageDetails>();

        List<AudioDetails> audioFile = new List<AudioDetails>();

        List<AudioDetails> audioFileSearch = new List<AudioDetails>();

        List<VideoDetails> videoFile = new List<VideoDetails>();

        List<VideoDetails> videoFileSearch = new List<VideoDetails>();

        List<OtherDetails> otherFile = new List<OtherDetails>();

        List<OtherDetails> otherFileSearch = new List<OtherDetails>();


        //sets reference for filter
        private string Filter = null;

        //lists of extentions accepted when filtered
        List<string> Extentions_Image = new List<string>(new string[] { ".bmp", ".gif", ".ico", ".jpeg", ".jpg", ".png" });
        List<string> Extentions_Audio = new List<string>(new string[] { ".mp3", ".aac", ".flac", ".ogg" });
        List<string> Extentions_Video = new List<string>(new string[] { ".flv", ".cam", ".mov", ".mpeg", ".mkv", ".webm", ".gif", ".avi", ".mpg" });
        List<string> Extentions_All = new List<string>(new string[] { ".flv", ".cam", ".mov", ".mpeg", ".mkv", ".webm", ".gif", ".avi", ".mpg", ".mp3", ".aac", ".flac", ".ogg", ".bmp", ".gif", ".ico", ".jpeg", ".jpg", ".png", ".dot", ".dotx", ".doc", ".docx", ".pdf" });


        public MainWindow()
        {
            InitializeComponent();
            BrowseButton_Click(null, null);


        }

        //Counts the quantity of elements display in the datagrid
        public int CountElement(int num)
        {
            NumberArray.Text = num.ToString() + " élément" + (num > 1 ? "s" : "") + " trouvé" + (num > 1 ? "s" : "");
            return num;

        }


        //Gets the metadata of file
        public string GetTags(string path)
        {
            var fs = File.GetAccessControl(path);

            var sid = fs.GetOwner(typeof(SecurityIdentifier));

            var ntAccount = sid.Translate(typeof(NTAccount));

            var name = ntAccount.Value.Split('\\');

            return name[1];

        }

        //Allows to read inside a word document to find specific text inside the document
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
        //Allows to read inside a PDF to find specific text inside the document
        public string GetPDF(string path)
        {
            StringBuilder text = new StringBuilder();

            if (File.Exists(path))
            {
                PdfReader pdfReader = new PdfReader(path);

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text.Append(currentText);
                }
                pdfReader.Close();
            }
            return text.ToString();
        }

        //Clears all list in order to back to square one 
        public void ClearList()
        {
            listFileSearch.Clear();
            wordFileSearch.Clear();
            wordFile.Clear();
            PDFFile.Clear();
            PDFFileSearch.Clear();
            imageFile.Clear();
            imageFileSearch.Clear();
            audioFile.Clear();
            audioFileSearch.Clear();
            videoFile.Clear();
            videoFileSearch.Clear();
            otherFile.Clear();
            otherFileSearch.Clear();
        }


        //Creates the datalist model
        public void makeList()
        {
            listFile = RetrieveList.myList.ToList();

            if (RetrieveList.RadiobuttonKeep.Contains("Word"))
            {
                if (RetrieveList.mywordList != null)
                {
                    RetrieveList.mywordList.Clear();
                }
                wordFile.Clear();
                foreach (var list in listFile)
                {
                    if (list.filename.ToString().ToLower().Contains(".doc"))
                    {
                        
                        WordDetails id = new WordDetails()
                        {

                            content = GetWord(list.path.ToString()),
                            name = list.filename,
                            path = list.path,
                            folderPath = list.folderPath
                        };
                        wordFile.Add(id);
                    }
                }
                IEnumerable<WordDetails> sansDoublonWord = wordFile.Distinct();
                int num = wordFile.Count();
                CountElement(num);
                RetrieveList.mywordList = wordFile.ToObservableCollection();
            }

            if (RetrieveList.RadiobuttonKeep.Contains("PDF"))
            {
                if (RetrieveList.mypdfList != null)
                {
                    RetrieveList.mypdfList.Clear();
                }
                PDFFile.Clear();
                foreach (var list in listFile)
                {
                    if (list.filename.ToString().ToLower().Contains(".pdf"))
                    {
                       
                        PDFdetails id = new PDFdetails()
                        {

                            content = GetPDF(list.path.ToString()),
                            name = list.filename,
                            path = list.path,
                            folderPath = list.folderPath

                        };

                        PDFFile.Add(id);
                    }

                }
                IEnumerable<PDFdetails> sansDoublonPDF = PDFFile.Distinct();
                int num = sansDoublonPDF.Count();
                CountElement(num);
                RetrieveList.mypdfList = PDFFile.ToObservableCollection();
            }

            if (RetrieveList.RadiobuttonKeep.Contains("Images"))
            {
                if (RetrieveList.myimageList != null)
                {
                    RetrieveList.myimageList.Clear();
                }
                imageFile.Clear();
                foreach (var list in listFile)
                {
                    if (Extentions_Image.Any(list.filename.ToLower().Contains))
                    {
                       
                        ImageDetails id = new ImageDetails()
                        {

                            FileName = list.filename,
                            Path = list.path,
                            folderPath = list.folderPath
                        };

                        imageFile.Add(id);
                    }
                }
                IEnumerable<ImageDetails> sansDoublonImage = imageFile.Distinct();
                int num = sansDoublonImage.Count();
                CountElement(num);
                RetrieveList.myimageList = imageFile.ToObservableCollection();
            }

            if (RetrieveList.RadiobuttonKeep.Contains("Audio"))
            {
                if (RetrieveList.myaudioList != null)
                {
                    RetrieveList.myaudioList.Clear();
                }

                audioFile.Clear();
                foreach (var list in listFile)
                {

                    if (Extentions_Audio.Any(list.filename.ToLower().Contains))
                    {
                        
                        AudioDetails id = new AudioDetails()
                        {

                            FileName = list.filename,
                            path = list.path,                          
                            folderPath = list.folderPath
                        };

                        audioFile.Add(id);
                    }

                }
                IEnumerable<AudioDetails> sansDoublonAudio = audioFile.Distinct();
                int num = sansDoublonAudio.Count();
                CountElement(num);
                RetrieveList.myaudioList = audioFile.ToObservableCollection();
            }

            if (RetrieveList.RadiobuttonKeep.Contains("Vidéos"))
            {
                if (RetrieveList.myvideoList != null)
                {
                    RetrieveList.myvideoList.Clear();
                }
                videoFile.Clear();
                foreach (var list in listFile)
                {

                    if (Extentions_Video.Any(list.filename.ToString().ToLower().Contains))
                    {
                        
                        VideoDetails id = new VideoDetails
                        {
                            FileName = list.filename,
                            Path = list.path,
                            folderPath = list.folderPath
                        };

                        videoFile.Add(id);
                    }

                }
                IEnumerable<VideoDetails> sansDoublonVideo = videoFile.Distinct();
                int num = sansDoublonVideo.Count();
                CountElement(num);
                RetrieveList.myvideoList = videoFile.ToObservableCollection();
            }

            if (RetrieveList.RadiobuttonKeep.Contains("Autres"))
            {


                if (RetrieveList.myotherList != null)
                {
                    RetrieveList.myotherList.Clear();
                }
                otherFile.Clear();

                foreach (var list in listFile)
                {
                 
                    if (!Extentions_All.Any(list.filename.ToString().ToLower().Contains))
                    {
                      
                        OtherDetails id = new OtherDetails
                        {

                            FileName = list.filename,
                            path = list.path,
                            folderPath = list.folderPath
                        };

                        otherFile.Add(id);
                    }

                }
                IEnumerable<OtherDetails> sansDoublonOther = otherFile.Distinct();
                int num = sansDoublonOther.Count();
                CountElement(num);
                RetrieveList.myotherList = otherFile.ToObservableCollection();
            }

        }

        //Select the source directory and display it 
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
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
                        
                        FileDetails id = new FileDetails()
                        {
                            filename = fi.Name,
                            path = fi.ToString(),
                            author = GetTags(fi.ToString()),
                            extension = fi.Extension,
                            folderPath = fi.Directory.ToString()

                        };

                        allFile.Add(id);
                    }
                }


                int num = allFile.Count;
                RetrieveList.myList = allFile;
                CountElement(num);               
                FileList.ItemsSource = allFile;

            }
        }

        //Open the directory of the selected file
        private void Row_RightClick(object sender, RoutedEventArgs e)
        {
            FileDetails fl = FileList.SelectedItem as FileDetails;
            ImageDetails Il = FileList.SelectedItem as ImageDetails;
            WordDetails Wl = FileList.SelectedItem as WordDetails;
            PDFdetails Pl = FileList.SelectedItem as PDFdetails;
            VideoDetails Vl = FileList.SelectedItem as VideoDetails;
            AudioDetails Al = FileList.SelectedItem as AudioDetails;
            OtherDetails Ol = FileList.SelectedItem as OtherDetails;


            if (fl != null)
            {
                var test = fl.folderPath;
                Process.Start("explorer.exe", fl.folderPath.ToString());
            }
            if (Il != null)
            {
                Process.Start("explorer.exe", Il.folderPath.ToString());
            }

            if (Wl != null)
            {
                Process.Start("explorer.exe", Wl.folderPath.ToString());
            }

            if (Pl != null)
            {
                Process.Start("explorer.exe", Wl.folderPath.ToString());
            }
            if (Vl != null)
            {
                Process.Start("explorer.exe", Vl.folderPath.ToString());
            }
            if (Al != null)
            {
                Process.Start("explorer.exe", Al.folderPath.ToString());
            }
            if (Ol != null)
            {

                Process.Start("explorer.exe", Ol.folderPath.ToString());
            }

        }

        //Open the selected file with the corresponding program
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                FileDetails fl = FileList.SelectedItem as FileDetails;
                ImageDetails Il = FileList.SelectedItem as ImageDetails;
                WordDetails Wl = FileList.SelectedItem as WordDetails;
                PDFdetails Pl = FileList.SelectedItem as PDFdetails;
                VideoDetails Vl = FileList.SelectedItem as VideoDetails;
                AudioDetails Al = FileList.SelectedItem as AudioDetails;
                OtherDetails Ol = FileList.SelectedItem as OtherDetails;
                try
                {
                    if (fl != null)
                    {
                        System.Diagnostics.Process.Start(fl.path.ToString());
                    }
                    if (Il != null)
                    {
                        System.Diagnostics.Process.Start(Il.Path.ToString());
                    }

                    if (Wl != null)
                    {
                        System.Diagnostics.Process.Start(Wl.path.ToString());
                    }

                    if (Pl != null)
                    {
                        System.Diagnostics.Process.Start(Wl.path.ToString());
                    }
                    if (Vl != null)
                    {
                        System.Diagnostics.Process.Start(Vl.Path.ToString());
                    }
                    if (Al != null)
                    {
                        System.Diagnostics.Process.Start(Al.path.ToString());
                    }
                    if (Ol != null)
                    {
                        System.Diagnostics.Process.Start(Ol.path.ToString());
                    }

                }
                catch
                {

                }


            }
        }

        //Keeps in memory the selected radiobutton, check if there is text in the searchbox and refreshes the datagrid with the returned data
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // ... Get RadioButton reference.
            var button = sender as System.Windows.Controls.RadioButton;
            // ... Display button content as title.
            Filter = button.Content.ToString();
            RetrieveList.RadiobuttonKeep = Filter;
            if (RetrieveList.myList != null)
            {
                if (Filter == "Tout")
                {
                    FileList.ItemsSource = RetrieveList.myList;
                    int num = listFile.Count();
                    CountElement(num);
                }
                else if (Filter == "Images")
                {

                    makeList();
                    if (txtNameToSearch.Text != null)
                    {
                        txtNameToSearch_TextChanged(null,null);
                    }
                    else
                    {
                        FileList.ItemsSource = RetrieveList.myimageList;
                    }
                    

                }
                else if (Filter == "Audio")
                {
                    makeList();
                    if (txtNameToSearch.Text != null)
                    {
                        txtNameToSearch_TextChanged(null, null);
                    }
                    else
                    {
                        FileList.ItemsSource = RetrieveList.myaudioList;
                    }
                    

                }
                else if (Filter == "Word")
                {
                    makeList();

                    if (txtNameToSearch.Text != null)
                    {
                        txtNameToSearch_TextChanged(null, null);
                    }
                    else
                    {
                        FileList.ItemsSource = RetrieveList.mywordList;
                    }
                    

                }
                else if (Filter == "PDF")
                {
                    makeList();

                    if (txtNameToSearch.Text != null)
                    {
                        txtNameToSearch_TextChanged(null, null);
                    }
                    else
                    {
                        FileList.ItemsSource = RetrieveList.mypdfList;
                    }
               

                }
                else if (Filter == "Vidéos")
                {
                    makeList();

                    if (txtNameToSearch.Text != null)
                    {
                        txtNameToSearch_TextChanged(null, null);
                    }
                    else
                    {
                        FileList.ItemsSource = RetrieveList.myvideoList;
                    }
                   

                }
                else if (Filter == "Autres")
                {
                    makeList();

                    if (txtNameToSearch.Text != null)
                    {
                        txtNameToSearch_TextChanged(null, null);
                    }
                    else
                    {
                        FileList.ItemsSource = RetrieveList.myotherList;
                    }
                 

                }
            }

        }

        //Search the file corresponding with the typed text in the searchbox and return the result
        private void txtNameToSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            ClearList();
            FileList.ItemsSource = null;
            CountElement(0);

            if (RetrieveList.myList != null)
            {
                listFile = RetrieveList.myList.ToList();
                {
                    makeList();

                    
                    string txtOrig = txtNameToSearch.Text;

                 
                    string upper = txtOrig.ToUpper();

                    string lower = txtOrig.ToLower();


                    if (RetrieveList.RadiobuttonKeep != "Tout")
                    {
                        if (RetrieveList.RadiobuttonKeep.Contains("Word"))
                        {
                       

                            var WordFiltered = from file in wordFile
                                               let wordfile = file.content

                                               where file.content != null
                                                
                                               where
                                                         wordfile.StartsWith(lower)
                                                      || wordfile.StartsWith(upper)
                                                      || wordfile.Contains(txtOrig)

                                               select file;


                            
                            wordFileSearch.AddRange(WordFiltered);

                            IEnumerable<WordDetails> sansDoublonWord = wordFileSearch.Distinct();

                            int num = sansDoublonWord.Count();
                            FileList.ItemsSource = sansDoublonWord.OrderBy(WordDetails => WordDetails.name).ToObservableCollection();
                            CountElement(num);


                        }


                        if (RetrieveList.RadiobuttonKeep.Contains("PDF"))
                        {

                            var PDFFiltered = from file in PDFFile
                                              let PDFFile = file.content

                                              where file.content != null
                                          
                                              where
                                                        PDFFile.StartsWith(lower)
                                                     || PDFFile.StartsWith(upper)
                                                     || PDFFile.Contains(txtOrig)

                                              select file;


                            PDFFileSearch.AddRange(PDFFiltered);

                            IEnumerable<PDFdetails> sansDoublonPDF = PDFFileSearch.Distinct();
                   
                            int num = sansDoublonPDF.Count();
                            FileList.ItemsSource = sansDoublonPDF.OrderBy(PDFdetails => PDFdetails.name).ToObservableCollection();
                          
                            CountElement(num);

                        }

                        if (RetrieveList.RadiobuttonKeep.Contains("Images"))
                        {
                            foreach (var list in listFile)
                            {
                                if (Extentions_Image.Any(list.extension.Contains))
                                {
                                  
                                    var ImageFiltered = from file in imageFile
                                                        let enamefile = file.FileName

                                                       
                                                        where
                                                                  enamefile.StartsWith(lower)
                                                               || enamefile.StartsWith(upper)
                                                               || enamefile.Contains(txtOrig)


                                                        select file;

                                
                                    imageFileSearch.AddRange(ImageFiltered);
                                    IEnumerable<ImageDetails> sansDoublon = imageFileSearch.Distinct();

                                    int num = sansDoublon.Count();
                                    FileList.ItemsSource = sansDoublon.OrderBy(ImageDetails => ImageDetails.FileName).ToObservableCollection();
                                    CountElement(num);
                                }

                            }

                        }

                        if (RetrieveList.RadiobuttonKeep.Contains("Audio"))
                        {
                            foreach (var list in listFile)
                            {
                                if (Extentions_Audio.Any(list.extension.Contains))
                                {
                                 
                                    var AudioFiltered = from file in audioFile
                                                        let enamefile = file.FileName

                                                  
                                                        where
                                                                  enamefile.StartsWith(lower)
                                                               || enamefile.StartsWith(upper)
                                                               || enamefile.Contains(txtOrig)


                                                        select file;

                                
                                    audioFileSearch.AddRange(AudioFiltered);
                                    IEnumerable<AudioDetails> sansDoublon = audioFileSearch.Distinct();

                                    int num = sansDoublon.Count();
                                    FileList.ItemsSource = sansDoublon.OrderBy(AudioDetails => AudioDetails.FileName).ToObservableCollection();
                                    CountElement(num);
                                }
                            }
                        }

                        if (RetrieveList.RadiobuttonKeep.Contains("Vidéos"))
                        {
                            foreach (var list in listFile)
                            {
                                if (Extentions_Video.Any(list.extension.Contains))
                                {
                              
                                    var VideoFiltered = from file in videoFile
                                                        let enamefile = file.FileName

                                                    
                                                        where
                                                                  enamefile.StartsWith(lower)
                                                               || enamefile.StartsWith(upper)
                                                               || enamefile.Contains(txtOrig)


                                                        select file;

                                    videoFileSearch.AddRange(VideoFiltered);
                                    IEnumerable<VideoDetails> sansDoublon = videoFileSearch.Distinct();

                                    int num = sansDoublon.Count();
                                    FileList.ItemsSource = sansDoublon.OrderBy(VideoDetails => VideoDetails.FileName).ToObservableCollection();
                                    CountElement(num);
                                }
                            }
                        }


                        if (RetrieveList.RadiobuttonKeep.Contains("Autres"))
                        {
                            foreach (var list in listFile)
                            {
                                if (Extentions_All.Any(list.extension.Contains))
                                {
                              
                                    var OtherFiltered = from file in otherFile
                                                        let enamefile = file.FileName

                                                        
                                                        where
                                                                  enamefile.StartsWith(lower)
                                                               || enamefile.StartsWith(upper)
                                                               || enamefile.Contains(txtOrig)


                                                        select file;

                                    
                                    otherFileSearch.AddRange(OtherFiltered);
                                    IEnumerable<OtherDetails> sansDoublon = otherFileSearch.Distinct();
                                    int num = sansDoublon.Count();
                                    FileList.ItemsSource = sansDoublon.OrderBy(OtherDetails => OtherDetails.FileName).ToObservableCollection();
                                    CountElement(num);
                                }
                            }
                        }
                    }

                    else
                    {
                    
                        var fileFiltered = from file in listFile
                                           let enamefile = file.filename

                                          
                                           where
                                                     enamefile.StartsWith(lower)
                                                  || enamefile.StartsWith(upper)
                                                  || enamefile.Contains(txtOrig)


                                           select file;

              
                        listFileSearch.AddRange(fileFiltered);

                        var fileauthorFiltered = from file in listFile
                                                 let authorfile = file.author

                                                 where file.author != null
                                                  
                                                 where
                                                           authorfile.StartsWith(lower)
                                                        || authorfile.StartsWith(upper)
                                                        || authorfile.Contains(txtOrig)

                                                 select file;

                        listFileSearch.AddRange(fileauthorFiltered);
                      
                        IEnumerable<FileDetails> sansDoublon = listFileSearch.Distinct();
                        int num = sansDoublon.Count();
                        FileList.ItemsSource = sansDoublon.OrderBy(FileDetails => FileDetails.filename).ToObservableCollection();
                        CountElement(num);

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
