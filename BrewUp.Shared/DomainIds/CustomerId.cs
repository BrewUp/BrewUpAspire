using Muflone.Core;

namespace BrewUp.Shared.DomainIds;

public sealed class CustomerId(Guid value) : DomainId(value.ToString());