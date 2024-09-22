using BrewUp.Sales.Shared;
using BrewUp.Shared.Commands;
using BrewUp.Shared.Models;

namespace BrewUp.Sales.Tests;

public class CommandSerializationTests
{
    private readonly Muflone.Persistence.Serializer _serializer = new ();
    
    [Fact]
    public async Task Can_Serialize_And_Deserialize_CreateSalesOrder_Command()
    {
        var rows = Enumerable.Empty<SalesRow>();
        rows = rows.Concat(new List<SalesRow>
        {
            new(Guid.NewGuid(), "Muflone IPA", 6)
        });
        
        SalesOrder salesOrder = new(
            Guid.NewGuid(),
            "SO-0001",
            Guid.NewGuid(),
            "John Doe",
            42.0m,
            "USD",
            rows
        );
		
        CreateSalesOrder salesOrderCommand = new (salesOrder.OrderId, salesOrder.OrderNumber,
            salesOrder.CustomerId, salesOrder.CustomerName,
            salesOrder.TotalAmount, salesOrder.Currency,
            salesOrder.Rows);
		
        var serializedCommand = await _serializer.SerializeAsync(salesOrderCommand);
        var deserializedCommand = await _serializer.DeserializeAsync<CreateSalesOrder>(serializedCommand);
        
        Assert.True(salesOrderCommand.SalesOrderId == deserializedCommand.SalesOrderId);
        Assert.True(salesOrderCommand.SalesOrderNumber == deserializedCommand.SalesOrderNumber);
        Assert.True(salesOrderCommand.CustomerId == deserializedCommand.CustomerId);
        Assert.True(salesOrderCommand.CustomerName == deserializedCommand.CustomerName);
        Assert.True(salesOrderCommand.TotalAmount == deserializedCommand.TotalAmount);
        Assert.True(salesOrderCommand.Currency == deserializedCommand.Currency);
    }
}