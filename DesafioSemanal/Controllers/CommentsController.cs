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
	public class CommentsController : ControllerBase
	{
		private readonly ICommentRepository _commentRepository;
		public CommentsController(ICommentRepository user)
		{
			_commentRepository = user;
		}

		[HttpGet]
		[Route("ObtenerComentarios")]
		public IActionResult get()
		{
			return Ok(_commentRepository.GetComments()); 
		}

		[HttpPost]
		public IActionResult Post(Comment comment)
		{
			_commentRepository.Add(comment);   
			return Ok("Se agregó comentario.");
		}

		[HttpPut]
		public IActionResult Put(Comment comment)
		{
			var modificarComment = _commentRepository.GetEntity(comment.Id);
			if (modificarComment == null)
				return NotFound($"El comentario con id {comment.Id} no existe.");
			
			modificarComment.Date = comment.Date;
			modificarComment.Content = comment.Content;
			modificarComment.User = comment.User;
			modificarComment.Post = comment.Post;

			_commentRepository.Update(modificarComment);     
			return Ok("Se modificó comentario.");
		}

		[HttpDelete]
		[Route("{id}")]
		public IActionResult Delete(int id)
		{
			var comment = _commentRepository.GetEntity(id);
			if (comment == null)
				return NotFound($"El comentario con id {id} no existe.");

			_commentRepository.Delete(id);
			return Ok("Se eliminó comentario.");
		}


	}
}
