﻿namespace EShop.Web.ViewModels.UserInfo
{
    using EShop.Data.Models;
    using EShop.Services.Mapping;

    public class UserInfoViewModel : IMapFrom<UserInfo>
    {
        public string FirsName { get; set; }

        public string LastName { get; set; }
    }
}
