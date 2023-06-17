// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DrugStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DrugStore.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppNetUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IHttpContextAccessor contxt;
        private readonly UserManager<AppNetUser> _userManager;

        public LogoutModel(SignInManager<AppNetUser> signInManager, UserManager<AppNetUser> userManager, ILogger<LogoutModel> logger, IHttpContextAccessor contxt)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.contxt = contxt;
            this._userManager = userManager;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            contxt.HttpContext.Session.Remove("dsSpMua" + _userManager.GetUserId(User));
            _logger.LogInformation("Người dùng đã đăng xuất.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
