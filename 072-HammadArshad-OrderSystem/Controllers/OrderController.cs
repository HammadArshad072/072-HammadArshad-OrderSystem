using _072_HammadArshad_OrderSystem.Data;
using _072_HammadArshad_OrderSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _072_HammadArshad_OrderSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly StoreDB _storeDB;
        private readonly IWebHostEnvironment _env;
        public OrderController(StoreDB storeDB, IWebHostEnvironment env)
        {
            _env = env;
            _storeDB = storeDB;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _storeDB.OrderModels.ToListAsync());
        }

        public IActionResult CreateOrder()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(OrderModel order)
        {
            if (ModelState.IsValid)
            {
                string folder = "/images/";
                order.ImageSource=  folder += (Guid.NewGuid().ToString() +order.Image.FileName);
                string server_folder = Path.Combine(_env.WebRootPath, folder);
                using(var filestream=new FileStream(server_folder, FileMode.Create))
                {
                    await order.Image.CopyToAsync(filestream);
                }
                Decimal price = order.Price;
                int quantity = order.Quantity;
                order.Total = (price * quantity).ToString();
                await _storeDB.AddAsync(order);
                await _storeDB.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public async Task<IActionResult> ViewOrder(int ? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            OrderModel singleorder = await _storeDB.OrderModels.FirstOrDefaultAsync(x => x.Id == Id);
            if (singleorder == null)
            {
                return NotFound();
            }
            return View(singleorder);
        }

        public async Task<IActionResult> UpdateOrder(int ? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            OrderModel Updateorder = await _storeDB.OrderModels.FirstOrDefaultAsync(x => x.Id == Id);
            if (Updateorder == null)
            {
                return NotFound();
            }
            return View(Updateorder);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrder(OrderModel order)
        {
            if (ModelState.IsValid)
            {
                OrderModel update_order = await _storeDB.OrderModels.FirstOrDefaultAsync(x => x.Id == order.Id);
                if (update_order == null)
                {
                    return NotFound();
                }
                Decimal price = order.Price;
                int quantity = order.Quantity;
                update_order.Total = (price * quantity).ToString();
                update_order.Name = order.Name;
                update_order.Description = order.Description;
                update_order.Price = order.Price;
                update_order.Quantity = order.Quantity;
                _storeDB.OrderModels.Update(update_order);
                await _storeDB.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public async Task<IActionResult> DeleteOrder(int ? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            OrderModel deleteorder = await _storeDB.OrderModels.FirstOrDefaultAsync(x => x.Id == Id);
            if (deleteorder == null)
            {
                return NotFound();
            }
            return View(deleteorder);
        }

        public async Task<IActionResult> DeleteOrderConfirm(int ? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            OrderModel deleteorder = await _storeDB.OrderModels.FirstOrDefaultAsync(x => x.Id == Id);
            if (deleteorder == null)
            {
                return NotFound();
            }
            _storeDB.OrderModels.Remove(deleteorder);
            await _storeDB.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
