using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    private readonly HairSalonContext _db;
    public StylistsController(HairSalonContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Stylist> allStyists = _db.Stylists.ToList();
      return View(allStyists);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Stylist stylist)
    {
      _db.Stylists.Add(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var stylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      return View(stylist);
    }
    public ActionResult Edit(int id)
    {
      var stylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      return View(stylist);
    }
    [HttpPost]
    public ActionResult Edit(Stylist stylist)
    {
      _db.Entry(stylist).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = stylist.StylistId });
    }
    public ActionResult Delete(int id)
    {
      var stylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      return View(stylist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var stylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      foreach (Client c in stylist.Clients)
      {
        var client = _db.Clients.FirstOrDefault(client => client.StylistId == id);
        _db.Clients.Remove(client);
      }
      _db.Stylists.Remove(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}