using Microsoft.AspNetCore.Mvc;
using ShoppingCartApplicationBackend.Models;

namespace ShoppingCartApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private static List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>
        {
            new ShoppingCartItem { Id = 1, Name = "Apple", Quantity = 5, Price = 1 },
            new ShoppingCartItem { Id = 2, Name = "Milk", Quantity = 2, Price = 2 },
            new ShoppingCartItem { Id = 3, Name = "Bread", Quantity = 1, Price = 3}
        };

        [HttpGet]
        public ActionResult<List<ShoppingCartItem>> GetShoppingCartItems()
        {
            return Ok(shoppingCartItems);
        }

        [HttpGet("{id}")]
        public ActionResult<ShoppingCartItem> GetShoppingCartItemById(int id)
        {
            var item = shoppingCartItems.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<ShoppingCartItem> AddNewShoppingCartItem([FromBody] ShoppingCartItem item)
        {
            if (item == null)
            {
                return BadRequest("Item cannot be null.");
            }

            int newId = shoppingCartItems.Count > 0 ? shoppingCartItems.Max(i => i.Id) + 1 : 1;
            item.Id = newId;

            shoppingCartItems.Add(item);
            return CreatedAtAction(nameof(GetShoppingCartItemById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShoppingCartItem(int id, [FromBody] ShoppingCartItem item)
        {
            if (item == null)
            {
                return BadRequest("Item cannot be null.");
            }

            var existingItem = shoppingCartItems.FirstOrDefault(t => t.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = item.Name;
            existingItem.Quantity = item.Quantity;
            existingItem.Price = item.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var item = shoppingCartItems.FirstOrDefault(t => t.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            shoppingCartItems.Remove(item);

            return NoContent();
        }

    }
}
