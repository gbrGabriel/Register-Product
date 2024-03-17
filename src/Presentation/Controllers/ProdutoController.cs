using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    [Route("api/produto")]
    public class ProdutoController(IRepositoryProduto repositoryProduto) : ControllerBase
    {
        private readonly IRepositoryProduto _repositoryProduto = repositoryProduto;

        /// <summary>
        /// Obtém todos os produtos cadastrados no sistema.
        /// </summary>
        /// <returns>Uma coleção de produtos.</returns>
        /// <response code="200">Retorna uma coleção de produtos</response>
        [HttpGet("obter-todos")]
        [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodos() => Ok(await _repositoryProduto.GetAllAsync());

        /// <summary>
        /// Obtém um produto cadastrado no sistema por id.
        /// </summary>
        /// <param name="id">Id do produto cadastrado no sistema.</param>
        /// <returns>Um produto cadastrado.</returns>
        /// <response code="200">Retorna o produto encontrado.</response>
        [HttpGet("obter-por-id/{id:guid}")]
        [ProducesResponseType(typeof(Produto), 200)]
        public async Task<IActionResult> ObterPorId(Guid id) => Ok(await _repositoryProduto.GetByIdAsync(id));

        /// <summary>
        /// Grava um novo produto no sistema.
        /// </summary>
        /// <param name="model">Dados do produto a ser gravado.</param>
        /// <returns>O novo produto cadastrado.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /gravar-produto
        ///     {
        ///        "nome": "Água mineral",
        ///        "descricao": "Água mineral da fonte de Palmeiras",
        ///        "preco": 1.50,
        ///        "unidade":"ML"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna o novo produto cadastrado.</response>
        /// <response code="400">Se o produto não pôde ser cadastrado, retorna uma mensagem de erro.</response>
        [HttpPost("gravar-produto")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GravarProduto([FromBody] ProdutoModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Não foi possível gravar o novo produto, verifique os parâmetros fornecido.");

                if (string.IsNullOrWhiteSpace(model.nome))
                    return BadRequest("Não é possível gravar um produto sem nome.");

                if (string.IsNullOrWhiteSpace(model.unidade))
                    return BadRequest("Não é possível gravar um produto sem unidade.");

                if (string.IsNullOrWhiteSpace(model.descricao))
                    return BadRequest("Não é possível gravar um produto sem uma descrição.");

                if (model.preco <= 0)
                    return BadRequest("Não é possível gravar um produto com preço menor ou igual a zero.");

                var novoProduto = new Produto
                {
                    Nome = model.nome,
                    Descricao = model.descricao,
                    Preco = model.preco,
                    Unidade = model.unidade,
                    Inativo = false
                };

                _repositoryProduto.Add(novoProduto);
                await _repositoryProduto.SaveChangesAsync();

                return Ok(novoProduto);
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
        /// <param name="model">Dados do produto a serem atualizados.</param>
        /// <returns>O produto atualizado.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /atualizar-dados-produto/{id}
        ///     {
        ///        "nome": "Água mineral",
        ///        "descricao": "Água mineral da fonte de Palmeiras",
        ///        "preco": 1.50,
        ///        "unidade":"ML"
        ///     }
        /// </remarks>
        /// <response code="200">Retorna o produto atualizado.</response>
        /// <response code="400">Se o produto não pôde ser atualizado, retorna uma mensagem de erro.</response>
        /// <response code="404">Se o produto não existe no sistema, retorna uma mensagem de erro.</response>
        [HttpPut("atualizar-dados-produto/{id:guid}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarDadosProduto(Guid id, [FromBody] ProdutoModel model)
        {
            try
            {
                var produto = await _repositoryProduto.GetByIdAsync(id);

                if (produto == null)
                    return NotFound("Não foi possível encontrar um produto com o ID fornecido.");

                return Ok(produto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inativa um produto cadastrado no sistema.
        /// </summary>
        /// <param name="id">Identificador único do produto.</param>
        /// <returns>O produto atualizado.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /inativar-produto/{id}
        ///     Identificador único do produto.
        ///     
        /// </remarks>
        /// <response code="200">Retorna o produto atualizado.</response>
        /// <response code="404">Se o produto não existe no sistema, retorna uma mensagem de erro.</response>
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpPut("inativar-produto/{id:guid}")]
        public async Task<IActionResult> InativarProduto(Guid id)
        {
            try
            {
                var produto = await _repositoryProduto.GetByIdAsync(id);

                if (produto == null)
                    return NotFound("Não foi possível localizar um produto com o id fornecido");

                produto.Inativo = true;

                if (produto.Estoque != null)
                    produto.Estoque.Inativo = true;

                await _repositoryProduto.UpdateAsync(produto);

                return Ok(produto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deleta um produto existente no sistema.
        /// </summary>
        /// <param name="id">Identificador único do produto.</param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /atualizar-dados-produto/{id}
        ///     Identificador único do produto.
        /// </remarks>
        /// <response code="204">Retorna apenas o StatusCodes de 204.</response>
        /// <response code="404">Se o produto não existe no sistema, retorna uma mensagem de erro.</response>
        [HttpDelete("deletar-produto/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletarProduto(Guid id)
        {
            try
            {
                var produto = await _repositoryProduto.GetByIdAsync(id);

                if (produto == null)
                    return BadRequest("Não foi possível localizar um produto com o id fornecido");

                await _repositoryProduto.DeleteAsync(produto);

                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
