using LegoBL.Manager;
using LegoUtil;
using Microsoft.Extensions.Configuration;

namespace ConsoleAppLego
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World! het is Mouhannad hier.");

            /*
             Console.WriteLine("Hello, World! Het is <jouw naam> hier.");
             ...
             var res = legoManager.GetLegoTheme("Vikings");
             Console.WriteLine(res);
            */

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            string connectionsString = config.GetConnectionString("SQLServerConnection");
            string sourceFile = config.GetSection("AppSettings")["sourceFile"];
            string sourceFileType = config.GetSection("AppSettings")["sourceFileType"];
            string databaseType = config.GetSection("AppSettings")["databaseType"];



            LegoManager legoManager = new LegoManager(RepositoryFactory.GeefRepository(databaseType, connectionsString));

            var res = legoManager.GetLegoTheme("Vikings");
            Console.WriteLine(res);

            //ImportLegoManager beheerder = new ImportLegoManager(
            //    FileReaderFactory.GeefFileReader(sourceFileType),
            //    RepositoryFactory.GeefRepository(databaseType, connectionsString));
            //beheerder.ImporteerGegegevens(sourceFile);


        }
    }
}