using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Using necesarios para el catalogo
using System.Data.Entity.Infrastructure;
using System.IO;
using Agape.ReservacionVhl.EN;
using Agape.ReservacionVhl.BL;
using System.Windows.Media.Imaging;
using PagedList;
using System.Net;

namespace EmpleadoUI.Controllers
{
    
    public class CatalogoController : Controller
    {
        ReservationBL _reservacion = new ReservationBL();

        CarBL _Catalogo = new CarBL();
        Car _Pro = new Car();
       
        // GET: Catalogo
        [HttpGet]
        [Authorize]
        public ActionResult Index(string buscar, string currentFilter, int? page)
        {
            if(Session["Name"] != null)
            {
                //sirve para la varibale de session.
                // TempData.Keep();
                var c = _Catalogo.GetAll(); //Metodo que carga la lista de los carros

                //Metodo que recorre los registros para hacer una busqueda
                if (buscar != null)
                {
                    page = 1;
                }
                else
                {
                    buscar = currentFilter;
                }
                ViewBag.CurrentFilter = buscar;

                var Vehiculo = from s in _Catalogo.GetAll() select s;

                if (!String.IsNullOrEmpty(buscar))
                {
                    Vehiculo = Vehiculo.Where(s => s.Mark.Name.ToUpper().Contains(buscar.ToUpper()));
                }

                //La capacidad de item para crear la paginación
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(Vehiculo.ToPagedList(pageNumber, pageSize));
            }
            return RedirectToAction("Index","Home");
        }
     

        private static BitmapImage BytesToImage(byte[] bytes)
        {
            var Bitmap = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                stream.Position = 0;
                Bitmap.BeginInit();
                Bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                Bitmap.CacheOption = BitmapCacheOption.OnLoad;
                Bitmap.UriSource = null;
                Bitmap.StreamSource = stream;
                Bitmap.EndInit();
            }
            return Bitmap;
        }
        public PartialViewResult Reservacion(Reservation ID)
        {
            if (_reservacion.GetReservation(ID) != null)
                return PartialView(true);
            return PartialView(false);
        }

        [HttpPost]
        public ActionResult GuardarReservacion(Reservation pReservation)
        {
            if (pReservation.ReservationID < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
                return View(_reservacion.Create(pReservation));
        }

        public ActionResult logOut()
        {
            if (Session["Name"] != null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            return View("Index");
        }
    }
}