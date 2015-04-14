using System.Web.Mvc;

namespace SimpleToDoList.Controllers
{
    public class TodoListController : Controller
    {
        //
        // GET: /TodoList/

        public ActionResult Index()
        {
            return View();
        }

        /* Commented out
        //
        // GET: /TodoList/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TodoList/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TodoList/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TodoList/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TodoList/Edit/5

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

        //
        // GET: /TodoList/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TodoList/Delete/5

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
         */ 
    }
}
