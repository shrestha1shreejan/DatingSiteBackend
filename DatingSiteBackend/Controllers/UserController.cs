using AutoMapper;
using Contracts;
using Entities.Dtos;
using FileLogger.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingSiteBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        #region Constructor

        public UserController(IUserRepository repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.GetAllUsersAsync();

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            _logger.LogInfo("returned all users");

            return Ok(usersToReturn);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(Guid id)
        {
            var user = await _repository.GetUserAsync(id);

            var userToReturn = _mapper.Map<UserForDetailDto>(user);

            _logger.LogInfo("returned all users");

            return Ok(userToReturn);
        }

    }
}
