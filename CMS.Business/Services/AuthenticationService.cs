
using CMS.DTOs;
using Core.Interfaces;


namespace CMS.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private UserDTO? _currentUser;
        public event Action? OnAuthenticationStateChanged;

        public UserDTO? CurrentUser => _currentUser;
        public bool IsLoggedIn => _currentUser != null;


        public void Login(UserDTO user)
        {
            _currentUser = user;

            OnAuthenticationStateChanged?.Invoke();
        }

        public void Logout()
        {
            _currentUser = null;

            OnAuthenticationStateChanged?.Invoke();
        }
    }
}
