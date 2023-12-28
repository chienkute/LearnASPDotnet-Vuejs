using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.Databases;
using DatingApp.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Services
{
    public class MemberService : IMemberService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MemberService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public MemberDto GetMemberDetail(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            return _mapper.Map<MemberDto>(user);
        }

        public List<MemberDto> GetMembers(string username, MemberFilterOption option)
        {
            return _context.Users
                .Include(u => u.UserType)
                .Where(u => u.Username != username)
                .Where(u =>
                    string.IsNullOrEmpty(option.KeySearch) ||
                    (
                        u.Username.Contains(option.KeySearch) ||
                        u.Email.Contains(option.KeySearch) ||
                        u.KnownAs.Contains(option.KeySearch)
                    )
                )
                .Skip(option.CurrentPage * option.PageSize)
                .Take(option.PageSize)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}