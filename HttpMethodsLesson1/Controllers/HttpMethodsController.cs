using HttpMethodsLesson1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HttpMethodsLesson1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HttpMethodsController : ControllerBase
    {
        private readonly string _url;
        private User _user = new User();
        public HttpMethodsController(string url = "https://62c6c24f74e1381c0a68083f.mockapi.io/api/v1/users")
        {
            this._url = url;
        }

        [HttpGet("getAllusers")]
        public async Task<IActionResult> GetAllUsersAsync()
        {

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_url);
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(response);
                }
                var result = response.Content.ReadFromJsonAsync<List<User>>().Result;

                return Ok(result);

            }
        }
        [HttpGet("getuser")]
        public async Task<IActionResult> GetUserAsync(string userId)
        {

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{this._url}/{userId}");
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(response);
                }
                var result = response.Content.ReadFromJsonAsync<User>().Result;
                return Ok(result);

            }
        }
        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser(UserModel userModel)
        {

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"{_url}", userModel);
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(response);
                }

                var result = response.Content.ReadFromJsonAsync<User>().Result;
                return Created(uri: $"{_url}/{result!.Id}", result);

            }
        }

        [HttpPut("editUser")]
        public IActionResult EditPerson(UserModel userModel, string userId)
        {
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = client.GetAsync($"{_url}/{userId}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(response);
                }
                HttpResponseMessage res = client.PutAsJsonAsync($"{_url}/{userId}", userModel).Result;
                if (!res.IsSuccessStatusCode) 
                { 
                    return BadRequest(res);
                }
                var result = res.Content.ReadFromJsonAsync<UserModel>();
                return Ok(result);

            }

        }
        [HttpDelete("deleteUser")]
        public IActionResult DeleteUser(string userId)
        {
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = client.GetAsync($"{_url}/{userId}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(response);
                }
                HttpResponseMessage res = client.DeleteAsync($"{_url}/{userId}").Result;
                if (!res.IsSuccessStatusCode)
                {
                    return BadRequest(res);
                }
                var result = res.Content.ReadFromJsonAsync<UserModel>();
                return Ok(result);

            }

        }
    }
}
