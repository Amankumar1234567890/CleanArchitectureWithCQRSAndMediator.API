using CleanArchitectureWithCQRSAndMediator.Application.Blogs.Commands.CreateBlog;
using CleanArchitectureWithCQRSAndMediator.Application.Blogs.Commands.DeleteBlog;
using CleanArchitectureWithCQRSAndMediator.Application.Blogs.Commands.UpdateBlog;
using CleanArchitectureWithCQRSAndMediator.Application.Blogs.Queries.GetBlogById;
using CleanArchitectureWithCQRSAndMediator.Application.Blogs.Queries.GetBlogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWithCQRSAndMediator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ApiControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var data = await Mediator.Send(new GetBlogQuery());
                return Ok(new { GetData = data });

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateBlogCommand command)
        {
            try
            {
                var data = await Mediator.Send(command);
                return Ok(data);
                             
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var data = await Mediator.Send(new GetBlogByIdQuery() {BlogId=id});
                return Ok(new { Data = data});
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var data = await Mediator.Send(new DeleteBlogCommand() { Id = id });
                return Ok(new { Data = data});
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id,UpdateBlogCommand command)
        {
            try
            {   
                if(id!=command.Id)
                {
                    return BadRequest();
                }
                else
                {
                    var data = await Mediator.Send(command);
                    return NoContent();

                }
               
            }
            catch(Exception ex)
            { 
                return BadRequest(ex.Message);

            }
        }

    }
}
