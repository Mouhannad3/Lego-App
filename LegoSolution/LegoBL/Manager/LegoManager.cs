
using LegoBL.Interfaces;

namespace LegoBL.Manager
{
    public class LegoManager
    {
        private ILegoRepository repository;

        public LegoManager(ILegoRepository repository)
        {
            this.repository = repository;
        }

        public LegoTheme GetLegoTheme(string legoTheme)
        {
            return repository.GeefLegoTheme(legoTheme);
        }
    }
}
