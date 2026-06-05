using LegoBL.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoBL.Interfaces
{
    public interface ILegoRepository
    {
        LegoTheme GeefLegoTheme(string legoTheme);
        void ImporteerLegoTheme(List<LegoTheme> data);
    }
}
