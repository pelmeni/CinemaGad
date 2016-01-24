using System.Collections.Generic;
using CinemaGad.Data;

namespace MvcApplication1.Models
{
    public class CinemaFilmSessionsViewModel
    {

        public static CinemaFilmSessionsViewModel From(IEnumerable<session> sessions, Dictionary<int,string> films, Dictionary<int, string> cinemas, Dictionary<int, Dictionary<int, session[]>> qresult)
        {
            return new CinemaFilmSessionsViewModel()
            {
                Cinemas = cinemas,
                Films = films,
                QResult = qresult,
                Sessions = sessions
            };
        }


        public IEnumerable<session> Sessions { get; private set; }

        public Dictionary<int,string> Films { get; private set; }

        public Dictionary<int, string> Cinemas { get; private set; }

        public Dictionary<int, Dictionary<int, session[]>> QResult { get; private set; }


    }
}