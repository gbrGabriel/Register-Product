namespace Presentation.Models;

/// <summary>
/// Modelo de Estoque para entrada na requisição de gravação de produto.
/// </summary>
public class EstoqueModel
{

    /// <summary>
    /// Id do produto.
    /// </summary>
    /// <example>Id do produto</example>
    public Guid produtoId { get; set; }

    /// <summary>
    /// Quantidade do produto disponível.
    /// </summary>
    /// <example>Quantidade do produto</example>
    public decimal quantidade { get; set; }

}
