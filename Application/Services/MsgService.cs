using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class MsgService : IMsgService
    {
        private readonly IMsgRepository _repository;
        private readonly IHttpContextAccessor _http;

        public MsgService(IMsgRepository repository, IHttpContextAccessor http)
        {
            _repository = repository;
            _http = http;
        }

        public async Task<ApiResult> AddMsg(string title, string body, string icon, Guid userId, MsgType type)
        {
            await _repository.AddMsg(new Msg { Title = title, Body = body, Icon = icon, UserId = userId, DateTime = DateTime.UtcNow, MsgType = type });
            return new ApiResult { Success = true };
        }

        public async Task<List<Msg>> GetUserMsg(int page)
        {
            var userId = (Guid)_http.HttpContext.Items["userId"];
            var res = await _repository.GetUserMsgs(userId, page);
            return res.ToList();
        }

        private async Task PushNotification(Msg req)
        {
            var applicationID =
                "AAAAxA7nAeE:APA91bHw2RFudheoAKwsreekYEkkrMQlTZZN38N8HIkEJ7YySChRgGlLA3LY2-46NY5QkkjGg4pTAOgwo8ekuPV1WYr_YkVe6fJ10-eTwXlN6OFQyJLZ5jtvk2MtbM1y7EhzjsLdtpmC";
            var senderId = "842063610337";


            var tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add($"Authorization: key={applicationID}");
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add($"Sender: id={senderId}");
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = "/topics/test",
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = req.Body,
                    title = req.Title
                },
                //data = new
                //{
                //    req.body,
                //    title = req.title,
                //}

            };

            var postBody = JsonSerializer.Serialize(payload);
            var byteArray = Encoding.UTF8.GetBytes(postBody);
            tRequest.ContentLength = byteArray.Length;
            using var dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            using var tResponse = tRequest.GetResponse();
            using var dataStreamResponse = tResponse.GetResponseStream();
            if (dataStreamResponse == null)
                return;

            using var tReader = new StreamReader(dataStreamResponse);
            var sResponseFromServer = tReader.ReadToEnd();
            //result.Response = sResponseFromServer;
        }
    }
}
