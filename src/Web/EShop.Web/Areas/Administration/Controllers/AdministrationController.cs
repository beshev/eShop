﻿namespace EShop.Web.Areas.Administration.Controllers
{
    using EShop.Common;
    using EShop.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area(GlobalConstants.AdministrationArea)]
    public class AdministrationController : BaseController
    {
    }
}
