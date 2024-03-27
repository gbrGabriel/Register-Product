namespace Presentation.Models;

/// <summary>
/// Modelo de Produto para entrada na requisição de gravação de produto.
/// </summary>
public class ProdutoModel
{
    /// <summary>
    /// Nome do produto.
    /// </summary>
    /// <example>Produto Exemplo</example>
    public string nome { get; set; } = null!;

    /// <summary>
    /// Descrição do produto.
    /// </summary>
    /// <example>Descrição do produto exemplo</example>
    public string descricao { get; set; } = null!;

    /// <summary>
    /// Preço do produto.
    /// </summary>
    /// <example>15.99</example>
    public decimal preco { get; set; }

    /// <summary>
    /// Unidade do produto.
    /// </summary>
    /// <example>UN</example>
    public string unidade { get; set; } = null!;

    /// <summary>
    /// SKU do produto.
    /// </summary>
    /// <example>3453EEE</example>
    public string sku { get; set; } = null!;

}

