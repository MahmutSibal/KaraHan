using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KaraHan.API.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected int UserId
    {
        get
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue("sub");

            if (!int.TryParse(userIdValue, out var userId))
            {
                throw new UnauthorizedAccessException("Gecerli kullanici kimligi bulunamadi.");
            }

            return userId;
        }
    }
}
