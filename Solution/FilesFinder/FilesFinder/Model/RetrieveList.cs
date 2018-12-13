
using System.Collections.ObjectModel;
using FilesFinder.Model;


namespace FilesFinder
{
    //static parameter in order to keep a list of file in memory
    public class RetrieveList
    {
        public static ObservableCollection<FileDetails> myList { get; set; }

        public static ObservableCollection<AudioDetails> myaudioList { get; set; }

        public static ObservableCollection<ImageDetails> myimageList { get; set; }

        public static ObservableCollection<PDFdetails> mypdfList { get; set; }

        public static ObservableCollection<VideoDetails> myvideoList { get; set; }

        public static ObservableCollection<WordDetails> mywordList { get; set; }

        public static ObservableCollection<OtherDetails> myotherList { get; set; }

        public static string DateList { get; set; }
        public static string DateModificate { get; set; }

        public static string RadiobuttonKeep { get; set; }



    }
}
