using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CinemaGad.Data;

namespace CinemaGad.Business
{
    /// <summary>
    /// Фасад  бд операций с сеансами
    /// </summary>
    public static class SessionOperations
    {
        /// <summary>
        /// Поиск сеанса по фильтрам
        /// </summary>
        /// <param name="start"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IEnumerable<session> Find(DateTime? start = null, string where = null)
        {
            using (var ctx = new test5Entities())
            {
                var q = ctx.session.AsQueryable();
                
                if (start.HasValue)
                {
                    var start2 = start.Value.Date.AddDays(1).Date.AddMinutes(-1);
                    q = q.Where(i => i.start >= start && i.start <= start2);
                }

                if (!string.IsNullOrEmpty(where))
                    q = q.Where(i =>  i.cinema.name.Contains( where));

                return q.ToArray();
            }
        }
        
        /// <summary>
        /// Валидация сеанса по параметрам
        /// </summary>
        /// <param name="start"></param>
        /// <param name="filmId"></param>
        /// <param name="cinemaId"></param>
        /// <returns></returns>
        public static bool Validate(DateTime? start, int? filmId, int? cinemaId)
        {
            using (var ctx = new test5Entities())
            {
                return !ctx.session.Any(i => i.cinema_id == cinemaId.Value && i.film_id == filmId.Value &&
                                            i.start==start.Value);
            }
        }

        /// <summary>
        /// Создание нового сеанса
        /// </summary>
        /// <param name="newdate"></param>
        /// <param name="filmId"></param>
        /// <param name="cinemaId"></param>
        public static void Insert(DateTime? newdate, int? filmId, int? cinemaId)
        {
            using (var ctx = new test5Entities())
            {
                Debug.Assert(newdate != null, "newdate != null");
                Debug.Assert(cinemaId != null, "cinemaId != null");
                Debug.Assert(filmId != null, "filmId != null");
                var i = new session
                {
                    start = newdate.Value,
                    cinema_id = cinemaId.Value,
                    film_id = filmId.Value
                };
                ctx.session.Add(i);
                ctx.SaveChanges();
            }
        }
        
        /// <summary>
        /// Удаление сеанса
        /// </summary>
        /// <param name="sessionId"></param>
        public static void Delete(int? sessionId)
        {
            using (var ctx = new test5Entities())
            {
                var s = ctx.session.FirstOrDefault(i => i.session_id == sessionId.Value);

                if (s != null)
                {
                    ctx.session.Remove(s);
                    ctx.SaveChanges();
                }
            }
        }
        
        /// <summary>
        /// Изменение сеанса
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="newdate"></param>
        /// <param name="filmId"></param>
        /// <param name="cinemaId"></param>
        public static void Update(int? sessionId, DateTime newdate, int? filmId, int? cinemaId)
        {
            using (var ctx = new test5Entities())
            {
                var s = ctx.session.FirstOrDefault(i => i.session_id==sessionId.Value);

                if (s != null)
                {
                    Debug.Assert(filmId != null, "filmId != null");
                    s.film_id = filmId.Value;
                    Debug.Assert(cinemaId != null, "cinemaId != null");
                    s.cinema_id = cinemaId.Value;
                    s.start = newdate;

                    ctx.SaveChanges();
                }
            }
        }
    }
}
