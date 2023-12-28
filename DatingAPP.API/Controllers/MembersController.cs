using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/members")]
    [ApiController]
    [Authorize]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MemberDto>> Get([FromQuery] MemberFilterOption option)
        {
            var currentUsername = User.Claims
                .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                ?.Value;
            if (string.IsNullOrEmpty(currentUsername)) return Unauthorized();
            return _memberService.GetMembers(currentUsername, option);
        }

        [HttpGet("{username}")]
        public ActionResult<MemberDto> Get(string username)
        {
            return new MemberDto();
        }
    }
}