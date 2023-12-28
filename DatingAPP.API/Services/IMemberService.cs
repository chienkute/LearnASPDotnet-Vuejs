using DatingApp.API.DTOs;

namespace DatingApp.API.Services
{
    public interface IMemberService
    {
        List<MemberDto> GetMembers(string username, MemberFilterOption option);
        MemberDto GetMemberDetail(string username);
    }
}