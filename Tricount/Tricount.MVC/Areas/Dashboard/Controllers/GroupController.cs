﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tricount.BL.Abstract;
using Tricount.BL.Concrete;
using Tricount.DAL.Contexts;
using Tricount.Entities.Concrete;
using Tricount.MVC.Models;
using Tricount.MVC.Models.DTO_s.Expense;
using Tricount.MVC.Models.DTO_s.Group;
using Tricount.MVC.Models.DTO_s.Payment;

namespace Tricount.MVC.Areas.Dashboard.Controllers
{

    [Area("Dashboard")]
    public class GroupController : Controller
    {
        private readonly IGroupManager groupManager;
        private readonly IInviteManager inviteManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public GroupController(
            IGroupManager groupManager,
            IInviteManager inviteManager,
            UserManager<User> userManager,
            IMapper mapper
            )
        {
            this.groupManager = groupManager;
            this.inviteManager = inviteManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostCreateGroup(GroupDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Fill in the mandatory fields!");
                return View("_CreateModal", model);
            }
            try
            {
                var group = mapper.Map<Group>(model.GroupDTO);
                var user = await userManager.GetUserAsync(User);

                await groupManager.Create(group);
                await groupManager.Update(group);
                user.Groups.Add(group);
                await userManager.UpdateAsync(user);

                return RedirectToAction("Index", "Group");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, $"An error was encountered.\nError message: {ex.Message}");
                return View("_CreateModal", model);
            }
        }

        [HttpGet]
        [Route("Dashboard/Group/Update/{slug}")]
        public async Task<IActionResult> GetUpdateGroup(string slug)
        {
            GroupDetailViewModel model = new();
            try
            {
                var group = groupManager.GetAll(p => p.Slug == slug).Result.FirstOrDefault();
                model.Group = group;
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, $"An error was encountered.\nError message: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUpdateGroup(GroupDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("_UpdateGroupModal", model);
            }
            try
            {
                await groupManager.Update(model.Group);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, $"An error was encountered.\nError message: {ex.Message}");
                return View("_UpdateGroupModal", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDeleteGroup(string id)
        {
            try
            {
                var result = await groupManager.GetAll(g => g.Id == id);
                return View(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, $"An error was encountered.\nError message: {ex.Message}");
                return RedirectToAction("Index", "Group");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> PostDeleteGroup(Group group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }
            try
            {
                await groupManager.Delete(group);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, $"An error was encountered.\nError message: {ex.Message}");
                return View(group);
            }
        }

        [HttpGet]
        [Route("Dashboard/Group/Detail/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            GroupDetailViewModel viewModel = new();
            try
            {
                var groupDetail = await groupManager.GetAllInclude(g => g.Slug == slug, g => g.Users).Result.FirstOrDefaultAsync();
                var user = groupDetail.Users.Where(u => u.Id == GetUserId()).FirstOrDefault();
                viewModel.Group = groupDetail;

                if (user == null)
                {
                    return Redirect("/Identity/Account/AccessDenied");
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, $"An error was encountered.\nError message: {ex.Message}");
                return View(viewModel);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> PostCreateInvite(GroupDetailViewModel model)
        {
            try
            {
                var user = await userManager.FindByNameAsync(model.Invite.UserName);
                var group = groupManager.GetAllInclude(g => g.Slug == model.Invite.GroupSlug, g => g.Users).Result.FirstOrDefault();
                var groupUser = group.Users.FirstOrDefault(u => u.Id == user.Id);

                if (user.Id == GetUserId())
                {
                    ModelState.AddModelError("Try Again", "You can't add yourself!");
                    return View("_InviteModal", model);
                }
                else if (group == null && user == null && groupUser != null)
                {
                    ModelState.AddModelError("Try Again", "The invitation could not be sent!");
                    return View("_InviteModal", model);
                }

                model.Invite.UserId = user.Id;
                model.Invite.GroupId = group.Id;
                model.Invite.SenderId = GetUserId();
                await inviteManager.Create(model.Invite);
                return RedirectToAction("Index", "Group");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, $"An error was encountered.\nError message: {ex.Message}");
                return View("_InviteModal", model);
            }
        }

        public async Task<IActionResult> DeniedInvite(string inviteId)
        {
            try
            {
                var invite = inviteManager.GetAll(i => i.Id == inviteId).Result.FirstOrDefault();
                invite.IsFinished = true;
                await inviteManager.Update(invite);

                return RedirectToAction("Index", "Group");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, $"An error was encountered.\nError message: {ex.Message}");
                return RedirectToAction("Index", "Group");
            }
        }
        
        public async Task<IActionResult> AcceptInvite(string inviteId)
        {
            try
            {
                var invite = inviteManager.GetAll(i => i.Id == inviteId).Result.FirstOrDefault();
                var group = groupManager.GetAll(i => i.Id == invite.GroupId).Result.FirstOrDefault();
                var user = await userManager.GetUserAsync(User);

                user.Groups.Add(group);
                await userManager.UpdateAsync(user);

                invite.IsFinished = true;
                await inviteManager.Update(invite);

                return RedirectToAction("Index", "Group");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, $"An error was encountered.\nError message: {ex.Message}");
                return RedirectToAction("Index", "Group");
            }
        }

        public string GetUserId()
        {
            var userId = userManager.GetUserId(User).ToString();
            return userId;
        }
    }
}
