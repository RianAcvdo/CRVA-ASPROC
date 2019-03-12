using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agape.ReservacionVhl.BL;
using Agape.ReservacionVhl.EN;

namespace MapaGoogle.Controllers
{

    public class MapController : Controller
    {
        // GET: Map
        [HttpGet]
        [Authorize]
        public ActionResult Index(int ID)
        {
            CarBL _car = new CarBL();

            if (ID <= 0)
                return RedirectToAction("Index", "Catalogo");
            return View(_car.SearchID(ID));
        }
        [HttpPost]
        public ActionResult GuardarReservacion(Reservation pReservation)
        {
            bool valor;
            try
            {
                pReservation.Action = "Reservado";
                pReservation.State = true;
                ReservationBL _reservationBL = new ReservationBL();
                if (_reservationBL.Create(pReservation) != null)
                    valor = true;
                else
                    valor = false;

                return Json(valor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return View("Error", new HandleErrorInfo(ex, "Home", "Index"));
            }

        }
    }
}