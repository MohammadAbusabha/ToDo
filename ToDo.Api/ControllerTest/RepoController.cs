//using Mapster;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Data;
//using ToDo.Core.Entities;
//using ToDo.Core.Interfaces;
//using ToDo.Core.RepoTest;
//using ToDo.Core.Resources;
//using ToDo.Core.SpecTest;

//namespace ToDo.Api.ControllerTest
//{
//    [Route("api/[controller]")]
//    [Authorize]
//    [ApiController]
//    public class DeveloperController : ControllerBase
//    {
//        public readonly IGenericRepository<Data> _repo;
//        public readonly ISpecification<Data> _spec;
//        public readonly ICurrentUserService _user;
//        public DeveloperController(IGenericRepository<Data> repository, ISpecification<Data> specification, ICurrentUserService userService)
//        {
//            _repo = repository;
//            _spec = specification;
//            _user = userService;
//        }

//        [HttpPost("{id}")] // should be httpget
//        //[Authorize(policy: "CanWrite")]
//        public IActionResult GetIdBySpec(int id)
//        {
//            var spec = new Specifications<Data>(x => x.Id == id);
//            var data = _repo.GetById(spec);
//            return Ok(data);
//        }
//        [HttpPost]
//        public void CreateData(CreateDataResource data)
//        {
//            var entity = data.Adapt<Data>();
//            entity.UserId = _user.UserId;
//            _repo.AddAsync(entity);
//        }
//    }
//}
