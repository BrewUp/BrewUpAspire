﻿namespace BrewUp.Warehouses.Shared.CustomTypes;

public record Availability(decimal Requested, decimal Available, string UnitOfMeasure);