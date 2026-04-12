using CMS.DTOs;
using Core.Interfaces;

/*this service is responsible for handling all the business logic related
to members(players and coaches).It interacts with the repository layer
to fetch data and then maps it to the appropriate DTOs before returning it
to the presentation layer.The service also includes error handling
and logging to ensure that any issues during data retrieval
or mapping are properly recorded for troubleshooting.*/

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

                var personList = new List<PersonDTO>();

                foreach (var m in members)
                {
                    if (m.PersonID <= 0)
                        continue;

                    bool isPlayer = m.PlayerID is > 0;
                    bool isCoach = m.CoachID is > 0;

                    if (isPlayer && isCoach)
                    {
                        _logger.LogWarning(
                            "Member row PersonID={PersonID} has both PlayerID and CoachID; skipping.",
                            m.PersonID);
                        continue;
                    }

                    char gender = !string.IsNullOrEmpty(m.Gender) ? m.Gender![0] : 'M';

                    if (isPlayer)
                    {
                        personList.Add(new PlayerDTO
                        {
                            PersonID = m.PersonID,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            DateOfBirth = m.DateOfBirth,
                            Phone = m.Phone,
                            Email = m.Email,
                            Address = m.Address,
                            Gender = gender,
                            Photo = m.Photo,
                            LastUpdate = m.LastUpdate,
                            CreatedDate = m.CreatedDate,
                            PlayerID = m.PlayerID!.Value,
                            CategoryID = m.CategoryID ?? 0,
                            CategoryName = m.CategoryName ?? "Unknown",
                            IsActive = m.IsActive
                        });
                    }
                    else if (isCoach)
                    {
                        personList.Add(new CoachDTO
                        {
                            PersonID = m.PersonID,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            DateOfBirth = m.DateOfBirth,
                            Phone = m.Phone,
                            Email = m.Email,
                            Address = m.Address,
                            Gender = gender,
                            Photo = m.Photo,
                            LastUpdate = m.LastUpdate,
                            CreatedDate = m.CreatedDate,
                            CoachID = m.CoachID!.Value,
                            Specialization = m.Specialization ?? "N/A",
                            Salary = m.Salary ?? 0m,
                            IsActive = m.IsActive
                        });
                    }
                }

                return personList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Mapping members in MemberService. Data: {Message}", ex.Message);
                return new List<PersonDTO>();
            }
        }

    }
}
