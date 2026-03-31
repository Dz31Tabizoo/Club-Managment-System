using CMS.DTOs;
using Core.Interfaces;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Security.AccessControl;

namespace Club_Managment_System.Services
{
    public class MemberServices : IMemberServices
    {
        private readonly IMemberRepository _memberRepo;
        private readonly ILogger<MemberServices> _logger;

        public MemberServices(IMemberRepository memberRepo, ILogger<MemberServices> logger)
        {
            _memberRepo = memberRepo;
            _logger = logger;
        }


        public async Task<IEnumerable<PersonDTO>> GetAllMembersAsync()
        {
            try
            {
                var members = await _memberRepo.GetAllMembersAsync();
                var memberList = new List<PersonDTO>();

                foreach (var m in members)
                {
                    // Use the ID from the SP to decide the type
                    if (m.PlayerID != null)
                    {
                        memberList.Add(new PlayerDTO
                        {
                            PersonID = (int)m.PersonID,
                            FirstName = m.FirstName ?? string.Empty,
                            LastName = m.LastName ?? string.Empty,
                            DateOfBirth = m.DateOfBirth,
                            Phone = m.Phone,
                            Email = m.Email,
                            Address = m.Address,
                            // Safe char conversion
                            Gender = m.Gender != null ? m.Gender[0] : 'M',
                            Photo = m.Photo,
                            LastUpdate = m.LastUpdate,

                            // Player specific
                            PlayerID = (int)m.PlayerID,
                            CategoryID = (int)(m.CategoryID ?? 0),
                            CategoryName = m.CategoryName ?? "Unknown",
                            isActive = m.isActive ?? true
                        });
                    }
                    else if (m.CoachID != null)
                    {
                        memberList.Add(new CoachDTO
                        {
                            PersonID = (int)m.PersonID,
                            FirstName = m.FirstName ?? string.Empty,
                            LastName = m.LastName ?? string.Empty,
                            DateOfBirth = m.DateOfBirth,
                            Phone = m.Phone,
                            Email = m.Email,
                            Address = m.Address,
                            Gender = m.Gender != null ? m.Gender[0] : 'M',
                            Photo = m.Photo,
                            LastUpdate = m.LastUpdate,

                            // Coach specific
                            CoachID = (int)m.CoachID,
                            Specialization = m.Specialization ?? "N/A",
                            
                            salary = m.Salary ?? 0m,
                            isActive = m.isActive ?? true
                        });
                    }
                }

                return memberList;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Mapping members in MemberService");
                return new List<PersonDTO>();
            }
        }


    }
}
