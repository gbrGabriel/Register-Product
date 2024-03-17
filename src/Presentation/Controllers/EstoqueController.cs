using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class EstoqueController(IRepositoryEstoque repositoryEstoque) : AbstractControllerBase
{
    private readonly IRepositoryEstoque _repositoryEstoque = repositoryEstoque;

    /// <summary>
    /// Retorna uma coleção de todos estoques cadastrados no sistema. 
    /// </summary>
    /// <returns>Uma coleção de estoques cadastrado.</returns>
    /// <response code="200">Uma coleção de estoques cadastrado.</response>
    [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
    [HttpGet("obter-todos")]
    public async Task<IActionResult> ObterTodos() => Ok(await _repositoryEstoque.GetAllAsync());

    /// <summary>
    /// Obtém um estoque cadastrado no sistema por id.
    /// </summary>
    /// <param name="id">Id do estoque cadastrado no sistema.</param>
    /// <returns>Um estoque cadastrado.</returns>
    /// <response code="200">Retorna o estoque encontrado.</response>
    [HttpGet("obter-por-id/{id:guid}")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterPorId(Guid id) => Ok(await _repositoryEstoque.GetByIdAsync(id));


}
