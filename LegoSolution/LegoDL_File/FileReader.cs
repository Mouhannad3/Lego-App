using LegoBL.Interfaces;
using System.Globalization;

public class FileReader : IFileReader
{
    public List<LegoTheme> LeesDataLegoTheme(string pad)
    {
        Dictionary<string, LegoTheme> themes = new Dictionary<string, LegoTheme>();
        int themeId = 1;

        using (StreamReader sr = new StreamReader(pad))
        {
            string lijn;
            while ((lijn = sr.ReadLine()) != null)
            {
                try
                {
                    if (lijn.Trim() != "")
                    {
                        string[] ss = lijn.Split('|');

                        if (ss.Length >= 14)
                        {
                            if (ss[0] != "set_id")
                            {
                                string setId = ss[0];
                                string naam = ss[1];
                                int jaar = int.Parse(ss[2]);
                                string themeNaam = ss[3];

                                int pieces = 0;
                                if (ss[7] != "")
                                {
                                    pieces = int.Parse(ss[7]);
                                }

                                int minifigs = 0;
                                if (ss[8] != "")
                                {
                                    minifigs = int.Parse(ss[8]);
                                }

                                int minAge = 0;
                                if (ss[9] != "")
                                {
                                    minAge = int.Parse(ss[9]);
                                }

                                double retailPrice = 0;
                                if (ss[10] != "")
                                {
                                    retailPrice = double.Parse(ss[10], CultureInfo.InvariantCulture);
                                }

                                string imageUrl = ss[13];

                                LegoSet legoSet = new LegoSet(setId, naam, jaar, pieces, minifigs, minAge, imageUrl, retailPrice);

                                if (!themes.ContainsKey(themeNaam))
                                {
                                    LegoTheme theme = new LegoTheme(themeId, themeNaam);
                                    theme.AddLegoSet(legoSet);
                                    themes.Add(themeNaam, theme);
                                    themeId++;
                                }
                                else
                                {
                                    themes[themeNaam].AddLegoSet(legoSet);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new LegoException("Fout bij het lezen van de file: " + ex.Message);
                }
            }
        }

        return themes.Values.ToList();
    }


}