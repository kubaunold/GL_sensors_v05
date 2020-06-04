using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GL_sensors_v0_4.Data;
using GL_sensors_v0_4.Models;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace GL_sensors_v0_4.Controllers
{
    public class MeasurementsController : Controller
    {
        private readonly SnakeyDataContext _context = new SnakeyDataContext();

        //public MeasurementsController(SnakeyDataContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        public ActionResult GetLast(int id)
        {
            //write logic here to get data
            var output = _context.Measurements.Where(e => e.sensorId == id).ToList();
            //List<Measurement> array = output.ToList();
            Measurement last = new Measurement();
            last = output[0];
            foreach(var i in output)
            {
                if (i.time > last.time) last = i;
            }
            string json = last.ToJson();
            //JsonObject _json = json;
            return Json(json);
            
        }

        public async Task<IActionResult> ReturnMeasurements(int? id)
        {
            //var query = from Measurements in _context
            //            where Measurements.sensorId == id
            //            select Measurements.temp;
            var gL_Sensors_v0_2Context = _context.Measurements.Where(e => e.sensorId == id);
            return View(await gL_Sensors_v0_2Context.ToListAsync());
            //return View();
        }


        // GET: Measurements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Measurements.ToListAsync());
        }

        // GET: Measurements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (measurement == null)
            {
                return NotFound();
            }

            return View(measurement);
        }

        // GET: Measurements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Measurements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,sensorId,pm_1_0,pm_2_5,pm_10,temp,hum,time")] Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(measurement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(measurement);
        }

        // GET: Measurements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }
            return View(measurement);
        }

        // POST: Measurements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,sensorId,pm_1_0,pm_2_5,pm_10,temp,hum,time")] Measurement measurement)
        {
            if (id != measurement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(measurement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasurementExists(measurement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(measurement);
        }

        // GET: Measurements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (measurement == null)
            {
                return NotFound();
            }

            return View(measurement);
        }

        // POST: Measurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            _context.Measurements.Remove(measurement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeasurementExists(int id)
        {
            return _context.Measurements.Any(e => e.Id == id);
        }
    }
}
