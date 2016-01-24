using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaGad.Data;

namespace CinemaGad.Business
{
    /// <summary>
    /// Фасад бд операций с фильмами
    /// </summary>
    public static class FilmOperations
    {
        /// <summary>
        /// Получить список всех фильмов
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<film> GetAll()
        {
            using (var ctx = new test5Entities())
            {
                return ctx.film
                    .OrderBy(i => i.name)
                    .ToArray();
            }
        }
    }
}
