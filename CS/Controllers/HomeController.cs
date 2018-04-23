using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace CS.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            MyModel model = new MyModel() {
                Name = "DevExpress User",
                Day = 5
            };

            ViewData["Items"] = GetItems();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index([ModelBinder(typeof(DevExpressEditorsBinder))] MyModel model) {
            TempData["PostedModel"] = model;
            return RedirectToAction("Success");
        }

        public ActionResult Success() {
            return View(TempData["PostedModel"]);
        }

        private IList GetItems() {
            List<TrackBarItemObject> items = new List<TrackBarItemObject>();
            XDocument document = XDocument.Load(Server.MapPath("~/App_Data/TimeLapse.xml"));
            foreach(var item in document.Root.Elements("Day")) {
                items.Add(new TrackBarItemObject() {
                    DayNum = Convert.ToInt32(item.Attribute("DayNum").Value),
                    ToolTip = (string)item.Attribute("ToolTip").Value,
                    Text = (string)item.Attribute("Text").Value
                });
            }

            return items;
        }
    }
    
    public class MyModel {
        public string Name { get; set; }
        public int Day { get; set; }
    }

    public class TrackBarItemObject {
        public int DayNum { get; set; }
        public string ToolTip { get; set; }
        public string Text { get; set; }
    }
}
