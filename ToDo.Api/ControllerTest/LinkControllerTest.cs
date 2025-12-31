using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Infrastructure.ServiceTest;
using ToDo.Infrastructure.SpecTest;

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
