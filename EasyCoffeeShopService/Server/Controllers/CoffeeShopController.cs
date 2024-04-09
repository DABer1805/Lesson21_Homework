namespace Server;

using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/")]
public class CoffeeShopController : ControllerBase
{
    private readonly IShopRepository _shopRepository;

    public CoffeeShopController(IShopRepository shopRepository)
    {
        _shopRepository = shopRepository;
    }

    [HttpGet("store/show")]
    public IActionResult Show()
    {
        return Ok(_shopRepository.GetAllOrders());
    }

    [HttpPost("store/add")]
    public IActionResult Add([FromBody] Order order)
    {
        _shopRepository.AddOrder(order);
        return Ok(_shopRepository.GetAllOrders());   
    }

    [HttpDelete("store/remove")]
    public IActionResult Delete(int id)
    {
        _shopRepository.DeleteOrder(id);
        return Ok(_shopRepository.GetAllOrders());
    }
}