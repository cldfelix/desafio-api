using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class ProductTest
{
    [Theory]
    [InlineData(0U, 1U, 0U)]
    [InlineData(4U, 15U, 4U)]
    [InlineData(4U, 2U, 2U)]
    [InlineData(50U, 50U, 0U)]
    public void Stock_Must_Be_Expected_Before_Remove(uint stock, uint updateStock, uint expected)
    {
        // arrange
        var produto = new Product("Produto 1", "Produto pratelera", 9.44m, stock);
        // act
        produto.UpdateStock(HandleItem.Remove, updateStock);

        // assert
        Assert.Equal(expected, produto.Stock);
    }

    [Theory]
    [InlineData(0U, 1U, 1U)]
    [InlineData(4U, 2U, 6U)]
    [InlineData(50U, 50U, 100U)]
    public void Stock_Must_Be_Expected_Before_Add(uint stock, uint updateStock, uint expected)
    {
        // arrange
        var produto = new Product("Produto 1", "Produto pratelera", 9.44m, stock);

        // act
        produto.UpdateStock(HandleItem.Add, updateStock);

        // assert
        Assert.Equal(expected, produto.Stock);
    }
}