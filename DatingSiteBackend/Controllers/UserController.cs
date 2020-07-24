using AutoMapper;
using Contracts;
using Entities.Dtos;
using FileLogger.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser (Guid id, UserForUpdateDto userForUpdate)
        {
            if (!ChcekUserIsAuthorized(id))
            {
                _logger.LogError($"Unauthorzied user with userid: {id}");
                return Unauthorized();
            }

            var userFromRepo = await _repository.GetUserAsync(id);

            _mapper.Map(userForUpdate, userFromRepo);

            _repository.UpdateUser(userFromRepo);

            if (await _repository.SaveAsync())
            {
                _logger.LogInfo($"Update for user with id: {id} successful");
                return NoContent();
            }

            _logger.LogError($"Error to update the user with id {id}");
            throw new Exception($"Updating user with {id} failed to save");
        }



        #region Private Methods

        private bool ChcekUserIsAuthorized(Guid id)
        {
            if (id.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return false;
            }

            return true;
        }
        #endregion

    }
}
