using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace FilesFinder
{
    public static class LinqExtensions
    {
        //Convert an element into a ObservableCollection
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _LinqResult)
        {
            return new ObservableCollection<T>(_LinqResult);
        }
    }
}
