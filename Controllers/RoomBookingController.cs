
using Hostel_V.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Hostel_V.Controllers
{
    public class RoomBookingController : Controller
    {
        private readonly HttpClient _httpClient;

        public RoomBookingController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("api");
        }

        // ================== BOOKING LIST ==================
        public async Task<IActionResult> Index()
        {
            //if (HttpContext.Session.GetString("UserId") == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            var response = await _httpClient.GetAsync("RoomBooking");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to fetch data from API.";
                return View(new List<RoomBookingModel>());
            }

            var data = await response.Content.ReadAsStringAsync();

            var bookings = JsonConvert.DeserializeObject<List<RoomBookingModel>>(data)
                          ?? new List<RoomBookingModel>();

            return View(bookings);
        }

        // ================== ADD BOOKING (GET) ==================
        public IActionResult Add()
        {
            return View(new RoomBookingModel());
        }

        // ================== EDIT BOOKING (GET) ==================
        public async Task<IActionResult> Edit(int Booking_Id)
        {
            var response = await _httpClient.GetAsync($"RoomBooking/{Booking_Id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var data = await response.Content.ReadAsStringAsync();

            var booking = JsonConvert.DeserializeObject<RoomBookingModel>(data);

            return View("Add", booking);
        }

        // ================== ADD / UPDATE BOOKING (POST) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoomBookingModel booking)
        {
            if (!ModelState.IsValid)
                return View(booking);

            var json = JsonConvert.SerializeObject(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            if (booking.Booking_Id == 0)
                response = await _httpClient.PostAsync("RoomBooking", content);
            else
                response = await _httpClient.PutAsync($"RoomBooking/{booking.Booking_Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "API Error: " + error);
                return View(booking);
            }

            // ✅ Redirect to Dashboard
            TempData["Success"] = "Booking saved successfully!";
            return RedirectToAction("Index", "Dashboard");
        }




    }
}