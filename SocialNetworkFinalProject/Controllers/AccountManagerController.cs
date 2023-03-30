using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialNetworkFinalProject.Data;
using SocialNetworkFinalProject.Data.Repository;
using SocialNetworkFinalProject.Data.UoW;
using SocialNetworkFinalProject.Extentions;
using SocialNetworkFinalProject.Models;
using SocialNetworkFinalProject.ViewModels;
using SocialNetworkFinalProject.ViewModels.Account;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkFinalProject.Controllers
{
    public class AccountManagerController : Controller
    {
        private IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IUnitOfWork _unitOfWork;

        public AccountManagerController(IMapper mapper,UserManager<User> userManager, SignInManager<User> signInManager,IUnitOfWork unitOfWork )
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("Generate")]
        public async Task<IActionResult> Generate()
        {
            var usergen = new GenerateUsers();
            var userlist = usergen.Populate(20);

            foreach (var user in userlist)
            {
                var result = await _userManager.CreateAsync(user,"123123123");
                if (result.Succeeded)
                    continue;
            }
            return RedirectToAction("Index", "Home");
        }
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Home/Login");
        }
        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.IsPersistant, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
                }
                
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        [Route("MyPage")]
        public async Task<IActionResult> MyPage()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);
            var model = new UserViewModel(result);
            model.Friends = await GetAllFriend();
            return View("User",model);
        }
        [HttpGet]
        [Authorize]
        [Route("Edit")]
        public async Task<IActionResult> Edit()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);
            var model = _mapper.Map<UserEditViewModel>(result);
            return View("Edit",model);
        }
        [HttpPost]
        [Authorize]
        [Route("Update")]
        public async Task<IActionResult> Update(UserEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                user.Convert(model);

                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
                }
                else
                {
                    return RedirectToAction("Edit", "AccountManager");
                }
            }
            ModelState.AddModelError("", "Некорректные данные");
            return View("Edit", model);
        }
        [Route("UserList")]
        [HttpGet]
        public async Task<IActionResult> UserList(string search)
        {
            var model = await CreateSearch(search);
            return View("UserList", model);
        }

        public async Task<SearchViewModel> CreateSearch(string search)
        {
            var currentuser = User;
            var result = await _userManager.GetUserAsync(currentuser);

            var list = _userManager.Users.AsEnumerable().ToList();

            if(!string.IsNullOrEmpty(search))
                list = _userManager.Users.AsEnumerable().Where(x => x.GetFullName().ToLower().Contains(search.ToLower())).ToList();
            var withfirend = await GetAllFriend();

            var data = new List<UserWithFriendExt>();
            foreach(var l in list)
            {
                var t = _mapper.Map<UserWithFriendExt>(l);
                t.IsFriendWithCurrent = withfirend.Where(w => w.Id == l.Id || l.Id == result.Id).Count()!=0;
                data.Add(t);
            }
            var model = new SearchViewModel()
            {
                UserList = data
            };
            return model;
        }

        public async Task<List<User>> GetAllFriend()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);
            var repo = _unitOfWork.GetRepository<Friend>() as FriendsRepository;
            return await repo.GetFriendByUser(result);
        }
        [HttpPost]
        [Route("AddFriend")]
        public async Task<IActionResult> AddFriend(string id)
        {
            var user = User;
            var target = await _userManager.GetUserAsync(user);
            var friend = await _userManager.FindByIdAsync(id);
            var repo = _unitOfWork.GetRepository<Friend>() as FriendsRepository;
            await repo.AddFriend(target, friend);
            return RedirectToAction("MyPage", "AccountManager");
        }
        [HttpPost]
        [Route("DelFriend")]
        public async Task<IActionResult> DelFriend(string id)
        {
            var user = User;
            var target = await _userManager.GetUserAsync(user);
            var friend = await _userManager.FindByIdAsync(id);
            var repo = _unitOfWork.GetRepository<Friend>() as FriendsRepository;
            await repo.DeleteFriend(target, friend);
            return RedirectToAction("MyPage", "AccountManager");
        }
        
        [HttpPost]
        [Route("Chat")]
        public async Task<IActionResult> Chat(string id)
        {
            var currentuser = User;
            var result = await _userManager.GetUserAsync(currentuser);
            var friend = await _userManager.FindByIdAsync(id);
            var repo = _unitOfWork.GetRepository<Message>() as MessageRepository;
            var messages = await repo.GetMessages(result, friend);
            var model = new ChatViewModel()
            {
                You = result,
                ToWhom = friend,
                History = messages //Список должен вернуться отсортированным из GetMessages()
            };
            return View("Chat", model);
        }


        public async Task<IActionResult> NewMessage(string id,ChatViewModel chat)
        {
            var user = await _userManager.GetUserAsync(User);
            var friend = await _userManager.FindByIdAsync(id);
            var repo = _unitOfWork.GetRepository<Message>() as MessageRepository;
               
            var message = new Message()
            {
                Sender=user,
                Recipient=friend,
                Text=chat.NewMessage.Text
            };
            await repo.Create(message);

            var history = await repo.GetMessages(user, friend);
            var model = new ChatViewModel()
            {
                You = user,
                ToWhom = friend,
                History = history
            };
            return View("Chat", model);
        }

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
