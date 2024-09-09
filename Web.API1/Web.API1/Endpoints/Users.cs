using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using vnpt.mplis.Web.Application.TodoLists.Queries.GetTodos;
using vnpt.mplis.Web.Application.Users.Queries;
using vnpt.mplis.Web.Domain.Configs;
using vnpt.mplis.Web.Infrastructure.Identity;

namespace vnpt.mplis.Web.API1.Endpoints
{
    public class Users : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GenerateJwtToken, "token");
            //.MapIdentityApi<ApplicationUser>();

        }

        public Task<string> GenerateJwtToken(ISender sender)
        {
            return sender.Send(new GetTokenQuery());
        }
    }
}
