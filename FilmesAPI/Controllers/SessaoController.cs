using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase {
        private dbContext _context;
        private IMapper _mapper;

        public SessaoController(dbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma sessão a base de dados.
        /// </summary>
        /// <param name="sessaoDto">Dados dos campos necessários para criação de uma sessão.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "201">Caso a inserção da sessão seja feita com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaSessao(CreateSessaoDto sessaoDto) {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaSessoesPorId), new {
                Id = sessao.Id
            }, sessao);
        }

        /// <summary>
        /// Retorna uma lista com todas as sessões.
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code = "200">Confirmando a consulta das sessões a base de dados.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ReadSessaoDto> RecuperaSessoes() {
            return _mapper.Map<List<ReadSessaoDto>>(_context.Sessoes.ToList());
        }

        /// <summary>
        /// Retorna um objeto sessão de acordo com o id informado.
        /// </summary>
        /// <param name="id">Identificação da sessão necessária para retorna-lo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "200">Confirmando a consulta da sessão ao banco de dados.</response>
        /// <response code = "404">Caso a sessão não seja encontrado na base de dados.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RecuperaSessoesPorId(int id) {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao != null) { 
                ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
                return Ok(sessaoDto);
            }
            return NotFound();
        }

        /// <summary>
        /// Atualiza dados do objeto sessão por completo.
        /// </summary>
        /// <param name="id">Identificação da sessão necessária para atualiza-lo.</param>
        /// <param name="sessaoDto">Dados da sessão passados no body necessário para atualização do mesmo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "204">Caso a alteração da sessão tenha sucesso.</response>
        /// <response code = "404">Caso a sessão não seja encontrado na base de dados.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AtualizaSessao(int id, [FromBody] UpdateSessaoDto sessaoDto) {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao == null) return NotFound();

            _mapper.Map(sessaoDto, sessao);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta o objeto sessão da base de dados.
        /// </summary>
        /// <param name="id">Identificação da sessão necessária para atualiza-lo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "204">Caso a deleção da sessão tenha sucesso.</response>
        /// <response code = "404">Caso a sessão não seja encontrado na base de dados.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletaSessao(int id) {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao == null) return NotFound();

            _context.Remove(sessao);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
