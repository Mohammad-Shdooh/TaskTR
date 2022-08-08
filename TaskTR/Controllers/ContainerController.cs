using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskTR.Models;
using TaskTR.Models.Repository;

namespace TaskTR.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IStore<Containers> _containerRepository;
        private readonly IStore<Tube> _tubeRepository; 
        public ContainerController()
        {
            _containerRepository = new ContainersRepository(); 
            _tubeRepository = new TubeRepository();
        }


        // GET: Container
        public ActionResult Index()
        {

            return View(_containerRepository.GetList());
        }

        // GET: Container/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Container/Create
        public ActionResult Create()
        {
            ViewData["FK_TubeTypeId"] = new SelectList(_tubeRepository.GetList(), "Id", "primaryName");

            return View();
        }

        // POST: Container/Create
        [HttpPost]
        public ActionResult Create(Containers container)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    ViewData["FK_TubeTypeId"] = new SelectList(_tubeRepository.GetList(), "Id", "PrimaryName", container.FK_TubeTypeId);

                    _containerRepository.Add(container);
                }
                return RedirectToAction("Index");
            } 
            catch
            {
                return View();
            }
        }

        // GET: Container/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Container/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Container/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Container/Delete/5
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
