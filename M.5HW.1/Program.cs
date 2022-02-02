using System;
using System.Text;
using Newtonsoft.Json;

namespace M._5HW._1

{
    class Program
    {
        static async Task Main(string[] args)
        {
            var requests = await Task.WhenAll(
                Program.GetlistUsers(),
                Program.GetSingleUser(),
                Program.GetSingleUserNotFound(),
                Program.GetlistResources(),
                Program.GetSingleResource(),
                Program.GetSingleResourceNotFound(),
                Program.PostCreate(),
                Program.PutUpdate(),
                Program.PatchUpdate(),
                Program.Delete(),
                Program.PostRegisterSuccessful(),
                Program.PostRegisterUnsuccessful(),
                Program.PostLoginSuccessful(),
                Program.PostLoginUnsuccessful(),
                Program.GetDelayedResponse());

            foreach (var item in requests)
            {
                //var url = item.
                var status = item.StatusCode;
                var result = await item.Content.ReadAsStringAsync();
                Console.WriteLine($"Response status: {status}, \n Response: {result} \n");
            }
        }

        public static async Task<HttpResponseMessage> GetlistUsers()
        {
            using (var http = new HttpClient())
            {
                var result = await http.GetAsync("https://reqres.in/api/users?page=2");
                var response = await DeserializeResponse<PageUsers>(result);
                return result;
            }
        }

        public static async Task<HttpResponseMessage> GetSingleUser()
        {
            using (var http = new HttpClient())
            {
                var result = await http.GetAsync("https://reqres.in/api/users/2");
                var response = await DeserializeResponse<ResponseSingleUser>(result);
                return result;

            }
        }

        public static async Task<HttpResponseMessage> GetSingleUserNotFound()
        {
            using (var http = new HttpClient())
            {
                var result = await http.GetAsync("https://reqres.in/api/users/23");
                return result;
            }
        }

        public static async Task<HttpResponseMessage> GetlistResources()
        {
            using (var http = new HttpClient())
            {
                var result = await http.GetAsync("https://reqres.in/api/unknown");
                var response = await DeserializeResponse<PageResources>(result);
                return result;
            }
        }

        public static async Task<HttpResponseMessage> GetSingleResource()
        {
            using (var http = new HttpClient())
            {
                var result = await http.GetAsync("https://reqres.in/api/unknown/2");
                var response = await DeserializeResponse<ResponseSingleResource>(result);
                return result;
            }
        }

        public static async Task<HttpResponseMessage> GetSingleResourceNotFound()
        {
            using (var http = new HttpClient())
            {
                var result = await http.GetAsync("https://reqres.in/api/unknown/23");
                return result;
            }
        }

        public static async Task<HttpResponseMessage> PostCreate()
        {
            using (var http = new HttpClient())
            {
                var user = new PostUser()
                {
                    name = "morpheus",
                    job = "leader"
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json");
                var result = await http.PostAsync("https://reqres.in/api/users",
                    httpContent);
                var response = await DeserializeResponse<ResponseCreate>(result);
                return result;
            }
        }

        public static async Task<HttpResponseMessage> PutUpdate()
        {
            using (var http = new HttpClient())
            {
                var user1 = new PostUser()
                {
                    name = "morpheus",
                    job = "zion resident"
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(user1),
                    Encoding.UTF8, "application/json");
                var result = await http.PutAsync("https://reqres.in/api/users/2", httpContent);
                var response = await DeserializeResponse<ResponseUpdate>(result);
                return result;
            }
        }

        public static async Task<HttpResponseMessage> PatchUpdate()
        {
            using (var http = new HttpClient())
            {
                var user1 = new PostUser()
                {
                    name = "morpheus",
                    job = "zion resident"
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(user1),
                    Encoding.UTF8, "application/json");
                var result = await http.PatchAsync("https://reqres.in/api/users/2", httpContent);
                var response = await DeserializeResponse<ResponseUpdate>(result);
                return result;
            }
        }

        public static async Task<HttpResponseMessage> Delete()
        {
            using (var http = new HttpClient())
            {
                var result = await http.DeleteAsync("https://reqres.in/api/users/2");
                return result;
            }
        }

        public static async Task<HttpResponseMessage> PostRegisterSuccessful()
        {
            using (var http = new HttpClient())
            {
                var register = new FullResponse()
                {
                    email = "eve.holt@reqres.in",
                    password = "pistol"
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(register),
                    Encoding.UTF8, "application/json");
                var result = await http.PostAsync("https://reqres.in/api/users",
                    httpContent);
                var response = await DeserializeResponse<RegisteredSuccessful>(result);
                return result;
            }
        }

        public static async Task<HttpResponseMessage> PostRegisterUnsuccessful()
        {
            using (var http = new HttpClient())
            {
                var register = new ResponseOnlyEmail()
                {
                    email = "esydney@fife"
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(register),
                    Encoding.UTF8, "application/json");
                var result = await http.PostAsync("https://reqres.in/api/login",
                    httpContent);
                var response = await DeserializeResponse<ResponseUnsuccessful>(result);
                return result;
            }
        }

        public static async Task<HttpResponseMessage> PostLoginSuccessful()
        {
            using (var http = new HttpClient())
            {
                var register = new FullResponse()
                {
                    email = "eve.holt@reqres.in",
                    password = "cityslicka"
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(register),
                    Encoding.UTF8, "application/json");
                var result = await http.PostAsync("https://reqres.in/api/login",
                    httpContent);
                var response = await DeserializeResponse<LoginSuccessful>(result);
                return result;
            }
        }

        public static async Task<HttpResponseMessage> PostLoginUnsuccessful()
        {
            using (var http = new HttpClient())
            {
                var register = new ResponseOnlyEmail()
                {
                    email = "peter@klaven"
                };
                var httpContent = new StringContent(JsonConvert.SerializeObject(register),
                    Encoding.UTF8, "application/json");
                var result = await http.PostAsync("https://reqres.in/api/login",
                    httpContent);
                var response = await DeserializeResponse<ResponseUnsuccessful>(result);
                return result;
            }
        }

        public static async Task<HttpResponseMessage> GetDelayedResponse()
        {
            using (var http = new HttpClient())
            {
                var result = await http.GetAsync("https://reqres.in/api/users?delay=3");
                var response = await DeserializeResponse<PageUsers>(result);
                return result;
            }
        }

        public static async Task<T> DeserializeResponse<T> (HttpResponseMessage response)
        {
            string data = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(data);
            return result;
        }
    }
}


