using BrewUp.Sales.Shared;
using BrewUp.Shared.Commands;
using BrewUp.Shared.Models;

namespace BrewUp.Sales.Tests;

public class CommandSerializationTests
{
    private BrewUpSerializer _serializer = new();
    
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
		
        var serializedCommand = await BrewUpSerializer.SerializeAsync(salesOrderCommand);
        var deserializedCommand = await BrewUpSerializer.DeserializeAsync<CreateSalesOrder>(serializedCommand);
        
        Assert.True(salesOrderCommand.SalesOrderId == deserializedCommand.SalesOrderId);
        Assert.True(salesOrderCommand.SalesOrderNumber == deserializedCommand.SalesOrderNumber);
        Assert.True(salesOrderCommand.CustomerId == deserializedCommand.CustomerId);
        Assert.True(salesOrderCommand.CustomerName == deserializedCommand.CustomerName);
        Assert.True(salesOrderCommand.TotalAmount == deserializedCommand.TotalAmount);
        Assert.True(salesOrderCommand.Currency == deserializedCommand.Currency);
        
        var deserializedListWithoutCommandList = deserializedCommand.Rows.Except(salesOrder.Rows).ToList();
        var commandListWithoutDeserializedCommandList = salesOrder.Rows.Except(deserializedCommand.Rows).ToList();
        
        Assert.True(deserializedListWithoutCommandList.Count == 0 && commandListWithoutDeserializedCommandList.Count == 0);
    }
}