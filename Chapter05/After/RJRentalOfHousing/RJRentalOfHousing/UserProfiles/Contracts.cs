﻿namespace RJRentalOfHousing.UserProfiles
{
    public static class Contracts
    {
        public static class V1
        {
            public class RegisterUser
            {
                public Guid UserId { get; set; }
                public string FullName { get; set; }
                public string DisplayName { get; set; }
            }

            public class UpdateUserFullName
            {
                public Guid UserId { get; set; }
                public string FullName { get; set; }
            }

            public class UpdateDisplayName
            {
                public Guid UserId { get; set; }
                public string DisplayName { get; set; }
            }

            public class UpdateUserProfilePhoto
            {
                public Guid UserId { get; set; }
                public string PhotoUrl { get; set; }
            }
        }
    }
}
