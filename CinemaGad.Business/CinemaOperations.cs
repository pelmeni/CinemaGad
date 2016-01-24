using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaGad.Data;

namespace CinemaGad.Business
{
    /// <summary>
    /// Фасад бд операций с кинотеатрами
    /// </summary>
    public static class CinemaOperations
    {
        /// <summary>
        /// Получить список всех кинотеатров
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<cinema> GetAll()
        {
            using (var ctx = new test5Entities())
            {
                return ctx.cinema
                    .OrderBy(i => i.name)
                    .ToArray();
            }
        }

    }
}
