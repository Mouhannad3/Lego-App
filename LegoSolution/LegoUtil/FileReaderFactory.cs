using LegoBL.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoUtil
{
    public static class FileReaderFactory
    {

        public static IFileReader GeefFileReader(string fileType)
        {
            switch (fileType)
            {
                case "CSV": return new FileReader();

                default: return null;
            }
        }


    }
}
