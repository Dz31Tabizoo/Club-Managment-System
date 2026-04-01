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
                    // 1. Validate that we at least have a PersonID before trying to map
                    if (m.PersonID == null) continue;

                    // Use 'as int?' to safely check for the IDs
                    int? playerId = m.PlayerID as int?;
                    int? coachId = m.CoachID as int?;

                    if (playerId != null)
                    {
                        memberList.Add(new PlayerDTO
                        {
                            PersonID = Convert.ToInt32(m.PersonID),
                            FirstName = m.FirstName?.ToString() ?? string.Empty,
                            LastName = m.LastName?.ToString() ?? string.Empty,
                            DateOfBirth = m.DateOfBirth,
                            Phone = m.Phone?.ToString(),
                            Email = m.Email?.ToString(),
                            Address = m.Address?.ToString(),
                            // Safe char conversion
                            Gender = !string.IsNullOrEmpty(m.Gender?.ToString()) ? m.Gender.ToString()[0] : 'M',
                            Photo = m.Photo as byte[],
                            LastUpdate = m.LastUpdate as DateTime?,

                            // Player specific - safe conversion
                            PlayerID = Convert.ToInt32(playerId),
                            CategoryID = m.CategoryID != null ? Convert.ToInt32(m.CategoryID) : 0,
                            CategoryName = m.CategoryName?.ToString() ?? "Unknown",
                            isActive = m.isActive ?? true
                        });
                    }
                    else if (coachId != null)
                    {
                        memberList.Add(new CoachDTO
                        {
                            PersonID = Convert.ToInt32(m.PersonID),
                            FirstName = m.FirstName?.ToString() ?? string.Empty,
                            LastName = m.LastName?.ToString() ?? string.Empty,
                            DateOfBirth = m.DateOfBirth,
                            Phone = m.Phone?.ToString(),
                            Email = m.Email?.ToString(),
                            Address = m.Address?.ToString(),
                            Gender = !string.IsNullOrEmpty(m.Gender?.ToString()) ? m.Gender.ToString()[0] : 'M',
                            Photo = m.Photo as byte[],
                            LastUpdate = m.LastUpdate as DateTime?,

                            // Coach specific - safe conversion
                            CoachID = Convert.ToInt32(coachId),
                            Specialization = m.Specialization?.ToString() ?? "N/A",
                            salary = m.Salary != null ? Convert.ToDecimal(m.Salary) : 0m,
                            isActive = m.isActive ?? true
                        });
                    }
                }

                return memberList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Mapping members in MemberService. Data: {Message}", ex.Message);
                return new List<PersonDTO>();
            }
        }

    }
}
