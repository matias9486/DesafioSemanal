using DesafioSemanal.Context;
using DesafioSemanal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSemanal.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
    {
		
		private readonly BlogContext _context;
		public UsersController(BlogContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("ObtenerUsuarios")]       
		public IActionResult get()
		{			
			return Ok(_context.Users.ToList()); 
		}
		
		
		[HttpPost]
		public IActionResult Post(User u)      
		{
			_context.Users.Add(u);   //lo agregamos al contexto(a la tabla)
			_context.SaveChanges();     //guardamos los cambios
			return Ok();
		}

		[HttpPut]
		public IActionResult Put(User user)       
		{
			if (_context.Users.FirstOrDefault(x => x.UserId == user.UserId) == null) return BadRequest("El usuario enviado no existe"); //sino encuentro ese objeto devuelvo un error 400 y msj

			//creamos un objeto auxiliar  y guardamos el original para modificarlo. Hacemos esto porque no nospermite aplicar _context.Continent.Update(continent); directamente																															//creamos un objeto Continent auxiliar  y guardamos el original para modificarlo. Hacemos esto porque no nospermite aplicar _context.users.Update(user); directamente
			var modificarUser = _context.Users.Find(user.UserId);
			//modificamos el obj auxiliar. .Net, reconoce el objeto levantado desde el context como si trabajaramos sobre el original directamente
			modificarUser.Name = user.Name;
			modificarUser.Email = user.Email;
			modificarUser.Password = user.Password;

			_context.SaveChanges();     //guardamos los cambios
			return Ok(_context.Users.ToList());
		}


		[HttpDelete]
		[Route("{id}")] 
		public IActionResult Delete(int id)     
		{
			if (_context.Users.FirstOrDefault(x => x.UserId == id) == null) 
				return BadRequest("El usuario enviado no existe"); //sino encuentro ese objeto devuelvo un error 400 y msj																																   

			//creamos un objeto Continent auxiliar  y guardamos el original para eliminarlo.
			var auxContinent = _context.Users.Find(id);
			//removemos el objeto de la lista
			_context.Users.Remove(auxContinent);

			_context.SaveChanges();     //guardamos los cambios
			return Ok(_context.Users.ToList());
		}


	}
}
