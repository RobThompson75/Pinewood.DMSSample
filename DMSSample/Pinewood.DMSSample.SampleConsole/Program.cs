// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pinewood.DMSSample.Business;
using System.Data.SqlClient;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<Pinewood.DMSSample.Business.Interfaces.IPartInvoiceRepositoryDB, PartInvoiceRepositoryDB>();
builder.Services.AddTransient<Pinewood.DMSSample.Business.Interfaces.ICustomerRepositoryDB, CustomerRepositoryDB>();
builder.Services.AddTransient<Pinewood.DMSSample.Business.Interfaces.IPartAvailabilityClient, PartAvailabilityClient>();
builder.Services.AddTransient<Pinewood.DMSSample.Business.Interfaces.IPartInvoiceController, PartInvoiceController>();

using IHost host = builder.Build();

DMSClient dmsClient = new DMSClient(host.Services.GetService<Pinewood.DMSSample.Business.Interfaces.IPartInvoiceController>());

await dmsClient.CreatePartInvoiceAsync("1234", 10, "John Doe");
