using DesafioSemanal.Interfaces;
using DesafioSemanal.Model;
using Microsoft.AspNetCore.Mvc;

namespace DesafioSemanal.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
    {		
		private readonly IUserRepository _userRepository;		
		public UsersController(IUserRepository user)
		{
			_userRepository = user;
		}

		[HttpGet]
		[Route("ObtenerUsuarios")]       
		public IActionResult get()
		{			
			return Ok(_userRepository.GetUsers()); //obtengo entidades mediante el repositorio
		}
				
		[HttpPost]
		public IActionResult Post(User user)      
		{			
			_userRepository.Add(user);   //llamamos al metodo agregar del repositorio que ya agrega y guarda por lo que no necesito el saveCanges
			return Ok();
		}

		[HttpPut]
		public IActionResult Put(User user)       
		{			
			var modificarUser = _userRepository.GetEntity(user.Id);
			if (modificarUser == null) 
				return NotFound($"El usuario con id {user.Id} no existe.");
			
			//modificamos el obj auxiliar. .Net, reconoce el objeto levantado desde el context como si trabajaramos sobre el original directamente
			modificarUser.Name = user.Name;
			modificarUser.Email = user.Email;
			modificarUser.Password = user.Password;
			modificarUser.Comments = user.Comments;
			modificarUser.Posts = user.Posts;

			_userRepository.Update(modificarUser);     //guardamos los cambios
			return Ok("Se modificó usuario.");
		}

		[HttpDelete]
		[Route("{id}")] 
		public IActionResult Delete(int id)     
		{			
			var user = _userRepository.GetEntity(id);
			if (user == null) 
				return NotFound($"El usuario con id {id} no existe.");

			_userRepository.Delete(id);
			return Ok("Se eliminó usuario.");
		}


	}
}
