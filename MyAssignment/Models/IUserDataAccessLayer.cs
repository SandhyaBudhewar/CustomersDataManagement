namespace MyAssignment.Models
{
    public interface IUserDataAccessLayer
    {
        bool AddUser(Users user);
        bool CheckLogin(Users user);
        Users CheckUserDetails(string Username);
        bool UpdatePassword(string Username, string NewPassword);
    }
}