using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase {

        private dbContext _context;
        private IMapper _mapper;

        public CinemaController(dbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um cinema a base de dados.
        /// </summary>
        /// <param name="cinemaDto">Dados dos campos necessários para criação de um filme.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "201">Caso a inserção do cinema seja feita com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AcionarCinema([FromBody] CreateCinemaDto cinemaDto) {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new {
                Id = cinema.Id
            }, cinemaDto);
        }

        /// <summary>
        /// Retorna uma lista com o cinema de um determinado endereço, caso o mesmo seja passado ou todos caso não seja passado nada.
        /// </summary>
        /// <param name="enderecoId">Identificação do endereço para retornar o cinema.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "200">Confirmando a consulta do cinema a base de dados.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ReadCinemaDto> RecuperaCinemas([FromQuery] int? enderecoId = null) {
            if (enderecoId == null) {
                return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
            }
            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM cinemas AS c WHERE c.EnderecoId = {enderecoId}").ToList());
        }

        /// <summary>
        /// Retorna um objeto cinema de acordo com o id informado.
        /// </summary>
        /// <param name="id">Identificação do cinema necessária para retorna-lo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "200">Confirmando a consulta do cinema ao banco de dados.</response>
        /// <response code = "404">Caso o cinema não seja encontrado na base de dados.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RecuperaCinemasPorId(int id) {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null) {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return Ok(cinemaDto);
            }
            return NotFound();
        }

        /// <summary>
        /// Atualiza dados do objeto cinema por completo.
        /// </summary>
        /// <param name="id">Identificação do cinema necessária para atualiza-lo.</param>
        /// <param name="cinemaDto">Dados do cinema passados no body necessário para atualização do mesmo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "204">Caso a alteração do cinema tenha sucesso.</response>
        /// <response code = "404">Caso o cinema não seja encontrado na base de dados.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Atualizacinema(int id, [FromBody] UpdateCinemaDto cinemaDto) {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) return NotFound();

            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta o objeto cinema da base de dados.
        /// </summary>
        /// <param name="id">Identificação do cinema necessária para atualiza-lo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "204">Caso a deleção do cinema tenha sucesso.</response>
        /// <response code = "404">Caso o cinema não seja encontrado na base de dados.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletaCinema(int id) {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) return NotFound();

            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
