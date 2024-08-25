﻿using BrewUp.Warehouses.Facade.Validators;
using BrewUp.Warehouses.ReadModel.Dtos;
using BrewUp.Warehouses.ReadModel.Queries;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.Shared.ReadModel;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouses.Facade;

public static class WarehousesHelper
{
	public static IServiceCollection AddWarehouses(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<SetAvailabilityValidator>();
		services.AddSingleton<ValidationHandler>();

		services.AddScoped<IMessagesService, MessagesService>();

		services.AddScoped<IWarehousesFacade, WarehousesFacade>();
		services.AddScoped<IAvailabilityService, AvailabilityService>();
		services.AddScoped<IQueries<Availability>, AvailabilityQueries>();

		return services;
	}
}