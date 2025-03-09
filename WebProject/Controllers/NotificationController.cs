using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class NotificationController : Controller
    {
        private readonly MyAppContext _context;
        private readonly UserManager<User> _userManager;

        private static int _testCounter = 1;
        public static int TestCounter
        {
            get => _testCounter;
            set => _testCounter = value;
        }


        public NotificationController(MyAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index (Guid id)
        {
            var noti = await _context.Notifications
                        .Where(n => n.Id == id)
                        .FirstOrDefaultAsync();
            if (noti == null)
            {
                return NotFound();
            }
            noti.Isread = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Post", new { id = noti.PostId });
        }

        [Authorize]
        public async Task<IActionResult> GetNoti()
        {
            //var notifications = (_testCounter % 2 == 0)
            //? new List<object>
            //{
            //        new { Title = "คุณได้เข้าร่วม", Message = "555555" },
            //        new { Title = "แจ้งเตือนใหม่", Message = "มีการอัปเดตระบบ" },
            //        new { Title = "คำเชิญ", Message = "คุณได้รับคำเชิญเข้าร่วมกลุ่ม" }
            //}
            //: new List<object>
            //{
            //        new { Title = "คุณได้เข้าร่วม", Message = "66666" },
            //        new { Title = "แจ้งเตือนใหม่", Message = "มีการอัปเดต" },
            //        new { Title = "คำเชิญ", Message = "คุณได้รับคำเ" }
            //};

            //_testCounter = (_testCounter + 1) % 2;
            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return RedirectToAction("Login", "Account");
            }
            var notifications = await _context.Notifications
                                .Where(n => n.UserId == user.Id && !n.Isread)
                                .Select(n => new {n.Type, n.PostName, n.Id})
                                .ToListAsync();
            return Json(notifications);
        }

    }
}
