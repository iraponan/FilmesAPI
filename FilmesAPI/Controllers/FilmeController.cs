﻿using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase {

        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um filme ao banco de dados.
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "201">Caso a inserção seja feita com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto) {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme);
        }

        /// <summary>
        /// Retorna uma lista com todos os filmes de acordo com oa parâmetros de informados.
        /// </summary>
        /// <param name="skip">Parâmetro para pular uma sequencia de filmes na base de dados.</param>
        /// <param name="take">Parâmetro para retornar a quantidade de filmes na base de dados.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "200">Confirmando a consulta a base de dados.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50) {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
        }

        /// <summary>
        /// Retorna um objeto filme de acordo com o id informado.
        /// </summary>
        /// <param name="id">Identificação do filme necessária para retorna-lo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "200">Confirmando a consulta ao banco de dados.</response>
        /// <response code = "404">Caso o filme não seja encontrado na base de dados.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult RecuperaFilmePorId(int id) {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);

            return Ok(filmeDto);
        }

        /// <summary>
        /// Atualiza dados do objeto filme por completo.
        /// </summary>
        /// <param name="id">Identificação do filme necessária para atualiza-lo.</param>
        /// <param name="filmeDto">Dados do filme passados no body necessário para atualização do mesmo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "204">Caso a alteração tenha sucesso..</response>
        /// <response code = "404">Caso o filme não seja encontrado na base de dados.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto) {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Atualiza partes do dados do objeto filme.
        /// </summary>
        /// <param name="id">Identificação do filme necessária para atualiza-lo.</param>
        /// <param name="jsonPatch">Patch no body necessário para atualização do campo em especifico.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "204">Caso a alteração tenha sucesso..</response>
        /// <response code = "404">Caso o filme não seja encontrado na base de dados.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> jsonPatch) {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
            
            jsonPatch.ApplyTo(filmeParaAtualizar, ModelState);

            if (!TryValidateModel(filmeParaAtualizar)) return ValidationProblem(ModelState);

            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta o objeto filme da base de dados.
        /// </summary>
        /// <param name="id">Identificação do filme necessária para atualiza-lo.</param>
        /// <returns>IActionResult</returns>
        /// <response code = "404">Caso o filme não seja encontrado na base de dados.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeletaFilme(int id) {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
