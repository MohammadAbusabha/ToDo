using Microsoft.AspNetCore.Mvc;
using ToDo.Infrastructure.ServiceTest;

namespace ToDo.Api.ControllerTest
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkControllerTest : ControllerBase
    {
        private readonly IPrivilegeRoleLink _test;
        public LinkControllerTest(IPrivilegeRoleLink roleLink)
        {
            _test = roleLink;
        }
        [HttpPost]
        public async Task Post(string r, List<string> p)
        {
            await _test.CreateLinkAsync(r, p);
        }
    }
}
