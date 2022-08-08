using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskTR.Models;
using TaskTR.Models.Repository;

namespace TaskTR.Controllers
{
    public class TubeController : Controller
    {

        private readonly IStore<Tube> _tubeRepository;
       // TubeRepository
        public TubeController()
        {
           this._tubeRepository = new TubeRepository() ;
        }

        // GET: Tube
        public ActionResult Index()
        {
            return View(_tubeRepository.GetList());
        }

        // GET: Tube/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tube/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tube/Create
        [HttpPost]
        public ActionResult Create(Tube tube)
        {
            try
            {
                // TODO: Add insert logic here

                if(ModelState.IsValid)
                {
                   // _tubeRepository.DuplicationNames(tube);
                    if (!_tubeRepository.DuplicationNames(tube))
                    {
                        TempData["error"] = "error";
                        return View(tube);
                    }
                    else { _tubeRepository.Add(tube); }
                   

                }

                return RedirectToAction("Index");
            }
            catch(Exception ex )
            {
                return View(ex.Message);
            }
        }

        // GET: Tube/Edit/5
        public ActionResult Edit(int id)
        {
            //var tube =  _tubeRepository.find(id); 
            // string constr = ConfigurationManager.ConnectionStrings["BioLabTaskConnectionString"].ConnectionString.ToString();
            //SqlConnection con = new SqlConnection(constr);
            //SqlCommand com = new SqlCommand("getCirtinTube", con);
            //com.CommandType = CommandType.StoredProcedure;
            //com.Parameters.AddWithValue("@Id", id);
            //con.Open();

            //var x = com.ExecuteNonQuery();
            //for(int i=0;  i.)
            var tube = _tubeRepository.find(id);
            return View(tube);
        }

        // POST: Tube/Edit/5
        [HttpPost]
        public ActionResult Edit( Tube tube)
        {
            try
            {
                // TODO: Add update logic here 
                var tubeX = _tubeRepository.find(tube.Id);
                tubeX.Id = tube.Id; 
                tubeX.primaryName = tube.primaryName;
                tubeX.secondaryName = tube.secondaryName;
                tubeX.colorHex = tube.colorHex; 
                _tubeRepository.Update(tubeX); 

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tube/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tube/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
