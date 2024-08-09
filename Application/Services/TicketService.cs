using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly IUserService _userService;

        public TicketService(ITicketRepository repository, IHttpContextAccessor http, IUserService userService)
        {
            _repository = repository;
            _http = http;
            _userService = userService;
        }
        public async Task<ApiResult> AddTicket(Ticket ticket)
        {
            var userId = (Guid?)_http.HttpContext.Items["userId"];
            ticket.UserId = userId.GetValueOrDefault();

            await _repository.AddTicket(ticket);
            return new ApiResult { Msg = "تیکت شما با موفقیت ثبت شد" };
        }

        public async Task<ApiResult> AddTicketPost(TicketPost ticketPost)
        {
            var userId = (Guid?)_http.HttpContext.Items["userId"];
            var ticket = await GetTicketById(ticketPost.TicketId);
            if (ticket.UserId != userId)
                return new ApiResult { Msg = "تیکت متعلق به شما نیست" };

            var user = await _userService.GetById(userId.GetValueOrDefault());
            ticketPost.UserName = user?.UserName;
            await _repository.AddTicketPost(ticketPost);
            return new ApiResult { Msg = "پاسخ شما با موفقیت ثبت شد" };
        }

        public async Task<ApiResult> AddTicketPostFile(byte[] content, long ticketId, string fileName)
        {
            if (content.Length > 1024 * 1024 * 10)
                return new ApiResult { Msg = "سایز فایل بیشتر از 10 مگابایت مجاز نیست" };

            var extensions = new List<string> { "png", "jpg", "jpeg", "rar", "zip", "mpeg", "avi", "mp4", "mkv" };
            var ext = Path.GetExtension(fileName).ToLower().Replace(".", "");
            if (extensions.IndexOf(ext) == -1)
                return new ApiResult { Msg = "پسوند های مجاز  : " + string.Join(",", extensions) };

            var userId = (Guid?)_http.HttpContext.Items["userId"];
            var ticket = await GetTicketById(ticketId);
            if (ticket?.UserId != userId)
                return new ApiResult { Msg = "تیکت متعلق به شما نیست" };

            var user = await _userService.GetById(userId.GetValueOrDefault());
            var ticketPost = new TicketPost { DateTime = DateTime.UtcNow, File = content, TicketId = ticketId, FileName = fileName, FileLength = content.Length };
            ticketPost.UserName = user?.UserName;
            await _repository.AddTicketPost(ticketPost);
            return new ApiResult { Msg = "پاسخ شما با موفقیت ثبت شد" };
        }

        public async Task<Ticket?> GetTicketById(long ticketId)
        {
            return await _repository.GetTicketById(ticketId);
        }

        public async Task<List<TicketPost>> GetTicketPosts(long ticketId)
        {
            var res = await _repository.GetTicketPosts(ticketId);
            return res.ToList();
        }

        public async Task<List<Ticket>> GetUserTickets()
        {
            var userId = (Guid)_http.HttpContext.Items["userId"];
            var res = await _repository.GetUserTickets(userId);
            return res.ToList();
        }
    }
}
