using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agape.ReservacionVhl.BL;
using Agape.ReservacionVhl.EN;

namespace Agape.ReservacionVhl.UIEmpleadoMVC.Controllers
{
    public class ReservationController : Controller
    {
        ReservationBL _ReservationBL = new ReservationBL();
        Reservation _Reservation = new Reservation();
        // GET: Reservation
        [Authorize]
        public ActionResult Index(int id)
        {
            try
            {
                return View(_ReservationBL.GetReservationEmployee(id));
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
            
      
        }

        public ActionResult CancelarReservacion(int id)
        {
            Reservation reservation = new Reservation();
            ReservationBL reservationBL = new ReservationBL();
            reservation.Action = "Libre";
            reservation.ReservationID = id;
            reservation.State = true;
            reservationBL.Edit(reservation);
            return RedirectToAction("Index", "Reservation", new { id = Session["UserID"] });
        }
    }
}