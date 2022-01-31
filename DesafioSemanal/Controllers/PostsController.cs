using DesafioSemanal.Interfaces;
using DesafioSemanal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSemanal.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class PostsController : ControllerBase
	{
		private readonly IPostRepository _postRepository;
		public PostsController(IPostRepository post)
		{
			_postRepository = post;
		}

		[HttpGet]
		[Route("ObtenerPosts")]
		public IActionResult get()
		{
			return Ok(_postRepository.GetPosts()); 
		}

		[HttpPost]
		public IActionResult Post(Post post)
		{
			_postRepository.Add(post);   
			return Ok("Se agregó Post.");
		}

		[HttpPut]
		public IActionResult Put(Post post)
		{
			var modificar = _postRepository.GetEntity(post.Id);
			if (modificar == null)
				return NotFound($"El post con id {post.Id} no existe.");
			
			modificar.Content = post.Content;
			modificar.Date = post.Date;
			modificar.User = post.User;
			modificar.Title = post.Title;
			
			_postRepository.Update(modificar);     
			return Ok("Se modificó post.");
		}

		[HttpDelete]
		[Route("{id}")]
		public IActionResult Delete(int id)
		{
			var user = _postRepository.GetEntity(id);
			if (user == null)
				return NotFound($"El post con id {id} no existe.");

			_postRepository.Delete(id);
			return Ok("Se eliminó post.");
		}

	}
}
