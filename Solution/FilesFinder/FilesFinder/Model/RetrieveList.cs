
using System.Collections.ObjectModel;
using FilesFinder.Model;


namespace FilesFinder
{
    //paramètre statique pour garder une liste de fichiers en memoire
    public class RetrieveList
    {
       
            public static ObservableCollection<FileDetails> myList { get; set; }
            public static string DateList { get; set; }
            public static string DateModificate { get; set; }

            public static string RadiobuttonKeep { get; set; }

        

    }
}
