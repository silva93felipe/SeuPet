using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.ModelView;    
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonoController : ControllerBase
    {
        private readonly IDonoService _donoService;

        public DonoController(IDonoService donoService)
        {
            this._donoService = donoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var donos = _donoService.GetAll();
            return Ok(donos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var donoDb = await _donoService.GetById(id);

            if(donoDb == null)
                return NoContent();

            var donoModelView = new DonoModelView();
            donoModelView.Ativo = donoDb.Ativo;
            donoModelView.Cpf = donoDb.Cpf;
            donoModelView.Nome = donoDb.Nome;
            donoModelView.Telefone = donoDb.Telefone;
            donoModelView.Id = donoDb.Id;

            return Ok(donoModelView);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DonoModelView donoInput)
        {            
            //_donoService.Create();
            Dono dono = new Dono();
            //Endereco endereco = new Endereco();
            dono.Ativo = true;
            dono.Cpf = donoInput.Cpf;
            dono.Nome = donoInput.Nome;
            dono.Telefone = donoInput.Telefone;
            dono.CreateAt = DateTime.UtcNow.AddHours(-3);
            dono.UpdateAt = DateTime.UtcNow.AddHours(-3);


            /* endereco.Bairro = donoInput.Endereco.Bairro;
            endereco.Cep = donoInput.Endereco.Cep;
            endereco.Cidade = donoInput.Endereco.Cidade;
            endereco.Logradouro = donoInput.Endereco.Logradouro;
            endereco.Numero = donoInput.Endereco.Numero;
            endereco.Estado = donoInput.Endereco.Estado; */

            // dono.Endereco = endereco;
            _donoService.Create(dono);
            
            return Ok();
        }

        [HttpGet("{cpf}&{nome}")]
        public async Task<IActionResult> GetByCpf(string cpf, string? nome)
        {
            var donoDb = await _donoService.GetByCpf(cpf);

            if(donoDb == null)
                return NotFound();

            var donoModelView = new DonoModelView();
            donoModelView.Ativo = donoDb.Ativo;
            donoModelView.Cpf = donoDb.Cpf;
            donoModelView.Nome = donoDb.Nome;
            donoModelView.Telefone = donoDb.Telefone;
            donoModelView.Id = donoDb.Id;

            return Ok(donoModelView);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DonoModelView dono)
        {
          /*   var donoModel = new Dono();
            
            var donoDb = await _context.Dono.FirstOrDefaultAsync( d => d.Id == id); 

            if(donoDb == null)
                return NotFound();

            donoDb.Ativo = dono.Ativo;
            donoDb.Cpf = dono.Cpf;
            donoDb.Nome = dono.Nome;
            donoDb.Telefone = dono.Telefone;
            donoDb.UpdateAt = DateTime.UtcNow.AddHours(-3);
            _context.Entry(donoDb).State = EntityState.Modified;

            //_context.Dono.Update(donoDb);
            
            await _context.SaveChangesAsync(); */

            return NoContent();

        }   

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donoService.Delete(id);
            if(result)
                return NoContent();
            
            return NotFound();
            
        }

    }
}