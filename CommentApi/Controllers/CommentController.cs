using System.Net.WebSockets;
using System.Security.Claims;
using System.Net;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using Application.Services;
using CommentApi.ViewModels;
using CommentApi.ViewModels.CommentModels;

namespace CommentApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;

        public CommentController(
            ILogger<CommentController> logger,
            ICommentService commentService
            )
        {
            _logger = logger;
            _commentService = commentService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Comments")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userId = HttpContext.User.FindFirst("UserId")?.Value;

                var comments = await _commentService.GetAllCommentsAsync(userId);
                var commentViewModels = CommentViewModel.ToViewModels(comments);
                return Ok(commentViewModels);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorModel(ex.Message));
            }
        }

        [HttpPost]
        [Route("Comment")]
        public async Task<IActionResult> Post([FromBody] CommentModel commentModel)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("UserId")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                await _commentService.AddCommentAsync(commentModel.Comment, userId);

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorModel(ex.Message));
            }
        }
    }
}
