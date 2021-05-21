using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSalonContext _db;
    public ClientsController(HairSalonContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Client> clients = _db.Clients.Include(client => client.Stylist).ToList();
      return View(clients);
    }
    public ActionResult Create()
    {
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Client client)
    {
      _db.Clients.Add(client);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = client.ClientId });
    }

    public ActionResult Details(int id)
    {
      var client = _db.Clients.FirstOrDefault(c => c.ClientId == id);
      return View(client);
    }
    public ActionResult Edit(int id)
    {
      var client = _db.Clients.FirstOrDefault(client => client.ClientId == id);
      ViewBag.StylistId = new SelectList(_db.Clients, "StylistId", "Name");
      return View(client);
    }

    [HttpPost]
    public ActionResult Edit(Client client)
    {
      _db.Entry(client).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var client = _db.Clients.FirstOrDefault(stylist => stylist.ClientId == id);
      return View(client);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var client = _db.Clients.FirstOrDefault(client => client.ClientId == id);
      _db.Clients.Remove(client);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}