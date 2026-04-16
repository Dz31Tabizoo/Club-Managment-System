using ClubManagementSystem.Models;
using CMS.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ClubManagementSystem.Services
{
    public interface IMemberService
    {
        Task<List<PersonModel>> GetAllMembersasync();
        Task<PersonModel?> SaveMemberAsync(PersonModel member);

    }
}
