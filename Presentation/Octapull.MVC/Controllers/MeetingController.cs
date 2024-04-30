using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octapull.Application.Dtos;
using Octapull.Domain.Entities;
using Octapull.Domain.Identity;
using Octapull.MVC.Models;
using System.Text;
using System.Text.Json;

namespace Octapull.MVC.Controllers
{
    [Authorize]
    public class MeetingController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<User> _userManager;

        public MeetingController(HttpClient httpClient, UserManager<User> userManager)
        {
            _httpClient = httpClient;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetMeeting()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateMeeting()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UpdateMeeting()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            string apiUrl = "https://localhost:7289/api/Meeting";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                List<Meeting> meetings = await JsonSerializer.DeserializeAsync<List<Meeting>>(responseStream);

                return View(meetings);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult DeleteMeeting()
        {
            var deleteMeetingViewModel= new DeleteMeetingViewModel();

            return View(deleteMeetingViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeetingAsync(MeetingViewModel meetingViewModel)
        {
            string apiUrl = "https://localhost:7289/api/Meeting";

            var user = await _userManager.GetUserAsync(User);

            var requestData = new Meeting()
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = user.Id,
                Name = meetingViewModel.Name,
                StartDate = meetingViewModel.StartDate,
                EndDate = meetingViewModel.EndDate,
                Description = meetingViewModel.Description,
                Document= meetingViewModel.Document,
            };

            var serializedData = JsonSerializer.Serialize(requestData);

            var content = new StringContent(serializedData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return RedirectToAction(nameof(IndexAsync));
            }
            else
            {
                // Handle the error scenario (e.g., log the error, return an error view)
                return View("Error");
            }
        }



        [HttpPost]
        public async Task<IActionResult> UpdateMeetingAsync(Guid id, MeetingViewModel meetingViewModel)
        {
            string getApiUrl = $"https://localhost:7289/api/Meeting/{id}";
            string putApiUrl = $"https://localhost:7289/api/Meeting/{id}";

            HttpResponseMessage response = await _httpClient.GetAsync(getApiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();

            Meeting meeting = await JsonSerializer.DeserializeAsync<Meeting>(responseStream);

            var user = await _userManager.GetUserAsync(User);

            meeting.Name = meetingViewModel.Name;
            meeting.StartDate = meetingViewModel.StartDate;
            meeting.EndDate = meetingViewModel.EndDate;
            meeting.Description = meetingViewModel.Description;
            meeting.Document = meetingViewModel.Document;

            var serializedData = JsonSerializer.Serialize(meeting);

            var content = new StringContent(serializedData, Encoding.UTF8, "application/json");

            HttpResponseMessage res = await _httpClient.PutAsync(putApiUrl, content);

            if (res.IsSuccessStatusCode)
            {
                string responseContent = await res.Content.ReadAsStringAsync();

                return RedirectToAction(nameof(IndexAsync));
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteMeetingAsync(Guid Id)
        {
            string apiUrl = $"https://localhost:7289/api/Meeting/{Id}";

            HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return RedirectToAction(nameof(IndexAsync));
            }
            else
            {
                // Handle the error scenario (e.g., log the error, return an error view)
                return View("Error");
            }
        }

    }
}
