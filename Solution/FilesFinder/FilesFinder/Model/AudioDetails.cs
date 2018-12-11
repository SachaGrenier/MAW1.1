using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesFinder.Model
{
  public  class AudioDetails
    {
        //le nom du fichier audio        
        public string FileName { get; set; }


        //le chemin complet
        public string Path { get; set; }

        public string Extension { get; set; }

    }
}
