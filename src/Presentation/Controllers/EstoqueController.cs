using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

[Route("api/estoque")]
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

    /// <summary>
    /// Grava um estoque produto no sistema.
    /// </summary>
    /// <param name="model">Dados do estoque a ser gravado.</param>
    /// <returns>O novo estoque cadastrado.</returns>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /gravar-estoque
    ///     {
    ///        "produtoId": "40e7c47c-2ffa-4de5-a7e4-84ef8564894f",
    ///        "quantidade": 1
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Retorna o novo estoque cadastrado.</response>
    /// <response code="400">Se o estoque não pôde ser cadastrado, retorna uma mensagem de erro.</response>
    [HttpPost("gravar-estoque")]
    [ProducesResponseType(typeof(Estoque), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GravarProduto([FromBody] EstoqueModel model)
    {
        try
        {
            if (model == null)
                return BadRequest();

            if (model.quantidade <= 0 || model.produtoId == Guid.Empty)
                return BadRequest("Não foi possível gravar um estoque com os dados fornecidos.");

            var estoque = new Estoque
            {
                Quantidade = model.quantidade,
                ProdutoId = model.produtoId,
            };

            _repositoryEstoque.Add(estoque);
            await _repositoryEstoque.SaveChangesAsync();

            return Ok(estoque);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Atualiza um produto existente no sistema.
    /// </summary>
    /// <param name="id">Identificador único do produto.</param>
    /// <param name="quantidade">Quantidade do produto a serem atualizados.</param>
    /// <returns>O estoque atualizado.</returns>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     PUT /atualizar-dados-produto/{id}/{quantidade}
    /// </remarks>
    /// <response code="200">Retorna o estoque atualizado.</response>
    /// <response code="400">Se o estoque não pôde ser atualizado, retorna uma mensagem de erro.</response>
    /// <response code="404">Se o estoque não existe no sistema, retorna uma mensagem de erro.</response>
    [HttpPut("atualizar-estoque/{id:guid}/{quantidade:decimal}")]
    public async Task<IActionResult> AtualizarEstoque(Guid id, decimal quantidade)
    {
        try
        {
            var estoque = await _repositoryEstoque.GetByIdAsync(id);

            if (estoque == null)
                return BadRequest("Não foi possível encontrar o estoque com o id fornecido.");

            estoque.Quantidade = quantidade;

            await _repositoryEstoque.UpdateAsync(estoque);

            return Ok(estoque);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Inativa um estoque cadastrado no sistema.
    /// </summary>
    /// <param name="id">Identificador único do estoque.</param>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     PUT /inativar-estoque/{id}
    ///     Identificador único do estoque.
    ///     
    /// </remarks>
    /// <response code="204">Retorna apenas o StatusCodes de 204.</response>
    /// <response code="404">Se o estoque não existe no sistema, retorna uma mensagem de erro.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [HttpPut("inativar-estoque/{id:guid}")]
    public async Task<IActionResult> InativarEstoque(Guid id)
    {
        try
        {
            var estoque = await _repositoryEstoque.GetByIdAsync(id);

            if (estoque == null)
                return BadRequest("Não foi possível encontrar o estoque com o id fornecido.");

            estoque.Inativo = true;

            await _repositoryEstoque.UpdateAsync(estoque);

            return NoContent();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Deleta um estoque existente no sistema.
    /// </summary>
    /// <param name="id">Identificador único do estoque.</param>
    /// <returns></returns>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     DELETE /deletar-estoque/{id}
    ///     Identificador único do estoque.
    /// </remarks>
    /// <response code="204">Retorna apenas o StatusCodes de 204.</response>
    /// <response code="404">Se o estoque não existe no sistema, retorna uma mensagem de erro.</response>
    [HttpDelete("deletar-estoque/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletarEstoque(Guid id)
    {
        try
        {
            var estoque = await _repositoryEstoque.GetByIdAsync(id);

            if (estoque == null)
                return BadRequest("Não foi possível localizar um produto com o id fornecido");

            await _repositoryEstoque.DeleteAsync(estoque);

            return NoContent();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
