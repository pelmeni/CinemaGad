using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using CinemaGad.Business;
using MvcApplication1.Models;
using MvcApplication1.Properties;


namespace MvcApplication1.Controllers
{
    
    public class CinemaController : Controller
    {
        bool IsEditMode
        {
            get
            {
                if (Session["IsEditMode"] == null)
                    Session["IsEditMode"] = false;
                return (bool)Session["IsEditMode"];
            }
            set { Session["IsEditMode"] = value; }
        }

        string FilterText
        {
            get
            {
                return (string)Session["FilterText"];
            }
            set { Session["FilterText"] = value; }
        }

        DateTime? FilterDate
        {
            get
            {
                return (DateTime?)Session["FilterDate"];
            }
            set { Session["FilterDate"] = value; }
        }

        CinemaFilmSessionsViewModel InnerIndex(bool editMode = false, DateTime? start = null, string text = null)
        {
            IsEditMode = editMode;

            if(start.HasValue)
                FilterDate = start.Value;
            else
                FilterDate= FilterDate??DateTime.UtcNow.AddHours(Settings.Default.TimeZone).Date;

            if (!string.IsNullOrEmpty(text))
                FilterText = text;

            ViewBag.IsEditMode = editMode;

            ViewBag.TimeZone = Settings.Default.TimeZone;

            var films = FilmOperations.GetAll().ToDictionary(i => i.film_id, i => i.name);

            var cinemas = CinemaOperations.GetAll().ToDictionary(i => i.cinema_id, i => i.name);

            var sessions = SessionOperations.Find(FilterDate, FilterText).ToList();

            var qresult = sessions.Select(i => new {i.cinema_id})
                .Distinct()
                .ToDictionary(i => i.cinema_id,
                    i =>
                        sessions.Where(j => j.cinema_id == i.cinema_id).Select(j => j.film_id)
                            .Distinct()
                            .ToDictionary(z => z,
                                z =>
                                    sessions.Where(j => j.cinema_id == i.cinema_id && j.film_id == z)
                                        .OrderBy(s => s.start)
                                        .ToArray()

                            )
                );


            var model = CinemaFilmSessionsViewModel.From(sessions, films, cinemas, qresult);

            return model;
        }


        public ActionResult Index(bool editMode=false, DateTime? start = null, string text = null)
        {

            var model = InnerIndex(editMode, start, text);

            return View("Index", model);
            
        }

        public ActionResult CinemaPartial(bool editMode = false, DateTime? start=null, string text=null)
        {
            var model = InnerIndex(editMode, start, text);

            return View("CinemaPartial",model);
        }
     


        [HttpPost]
        public ActionResult SaveSession(DateTime? start, string[] time, int? filmId, int? cinemaId)
        {
            Debug.Assert(start != null, "start != null");
            var newdate = DateTime.Parse($"{start.Value.ToString("yyyy-MM-dd")} {time[0]}");

            var r = SessionOperations.Validate(newdate, filmId, cinemaId);

            if (r)
                SessionOperations.Insert(newdate, filmId, cinemaId);
            else 
                throw new ApplicationException("Validate error!");

            return RedirectToAction("CinemaPartial", new {EditMode = IsEditMode, start=start.Value.Date});
        }

        public bool ValidateSession(DateTime? start, string[] time, int? filmId, int? cinemaId)
        {
            Debug.Assert(start != null, "start != null");
            var newdate = DateTime.Parse($"{start.Value.ToString("yyyy-MM-dd")} {time[0]}");

            var r = SessionOperations.Validate(newdate, filmId, cinemaId);

            return r;
        }

        public ActionResult EditMode()
        {
            IsEditMode = !IsEditMode;

            return RedirectToAction("CinemaPartial", new { editMode = IsEditMode});
        }

        public ActionResult DeleteSession(int? sessionId)
        {
            SessionOperations.Delete(sessionId);

            return RedirectToAction("CinemaPartial", new {editMode = IsEditMode});
        }

        public ActionResult UpdateSession(int? sessionId, DateTime? start, string[] time, int? filmId, int? cinemaId)
        {
            Debug.Assert(start != null, "start != null");
            var newdate = DateTime.Parse($"{start.Value.ToString("yyyy-MM-dd")} {time[0]}");

            SessionOperations.Update(sessionId, newdate, filmId, cinemaId);

            return RedirectToAction("CinemaPartial", new { editMode = IsEditMode, start = start.Value.Date });
        }
    }
}
