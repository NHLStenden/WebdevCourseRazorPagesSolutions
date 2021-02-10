using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Pages.Lesson1
{
    public static class StaticUserRepo
    {
        static List<User> _users = new List<User>();

        public enum AddUserResult
        {
            UserNameIsNotUnique,
            GuidIsNotUnique,
            Succes
            //zou nooit moeten voorkomen
        }

        public static AddUserResult AddUser(Guid guid, User user)
        {
            if (_users.Count(u => String.Equals(u.UserName, user.UserName, StringComparison.CurrentCultureIgnoreCase)) > 0)
            {
                return AddUserResult.UserNameIsNotUnique;
            }

            if (_users.Count(u => u.UniqueGuid == guid) > 0)
            {
                return AddUserResult.GuidIsNotUnique;
            }

            _users.Add(user);
            return AddUserResult.Succes;
        }

        public static User GetUser(Guid guid)
        {
            return _users.SingleOrDefault(x => x.UniqueGuid == guid);
        }
    }

    public enum Role
    {
        WeddingCouple,
        Guest
    }

    public class User
    {
        public Guid UniqueGuid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Location { get; set; }
        public DateTime Date { get; set; }

        public Role Role { get; set; }
    }
}
