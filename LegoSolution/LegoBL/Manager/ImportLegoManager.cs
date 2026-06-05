using LegoBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoBL.Manager
{
    public class ImportLegoManager
    {
        IFileReader lezer;
         ILegoRepository repo;

        public ImportLegoManager(IFileReader lezer, ILegoRepository repo)
        {
            this.lezer = lezer;
            this.repo = repo;
        }
        public void ImporteerGegegevens(string sourceFile)
        {
            var data = lezer.LeesDataLegoTheme(sourceFile);
            repo.ImporteerLegoTheme(data);
        }
    }
}
