using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase {
        private dbContext _context;
        private IMapper _mapper;

        public EnderecoController(dbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um endereco a base de dados.
        /// </summary>
        /// <param name="enderecoDto">Dados dos campos necessários para criação de um endereço.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "201">Caso a inserção do endereço seja feita com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto) {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaEnderecoPorId), new {
                Id = endereco.Id
            }, endereco);
        }

        /// <summary>
        /// Retorna uma lista com todos os endereços.
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code = "200">Confirmando a consulta a base de dados.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ReadEnderecoDto> RecuperaEnderecos() {
            return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos);
        }

        /// <summary>
        /// Retorna um objeto endereço de acordo com o id informado.
        /// </summary>
        /// <param name="id">Identificação do endereço necessária para retorna-lo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "200">Confirmando a consulta do endereço ao banco de dados.</response>
        /// <response code = "404">Caso o endereço não seja encontrado na base de dados.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RecuperaEnderecoPorId(int id) {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco != null) {
                ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
                return Ok(enderecoDto);
            }
            return NotFound();
        }

        /// <summary>
        /// Atualiza dados do objeto endereço por completo.
        /// </summary>
        /// <param name="id">Identificação do endereço necessária para atualiza-lo.</param>
        /// <param name="enderecoDto">Dados do endereço passados no body necessário para atualização do mesmo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "204">Caso a alteração do endereço tenha sucesso.</response>
        /// <response code = "404">Caso o endereço não seja encontrado na base de dados.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Atualizaendereco(int id, [FromBody] UpdateEnderecoDto enderecoDto) {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null) return NotFound();

            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta o objeto endereço da base de dados.
        /// </summary>
        /// <param name="id">Identificação do endereço necessária para atualiza-lo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "204">Caso a deleção do endereço tenha sucesso.</response>
        /// <response code = "404">Caso o endereço não seja encontrado na base de dados.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletaEndereco(int id) {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null) return NotFound();

            _context.Remove(endereco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
