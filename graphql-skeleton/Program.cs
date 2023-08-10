using HotChocolate.AspNetCore;
using Infrastructure;
using Application;
using Microsoft.Extensions.DependencyInjection;
using graphql_skeleton.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddCors(o =>
        o.AddDefaultPolicy(b =>
            b.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()));


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks();

// This adds the GraphQL server core service and declares a schema.
builder.Services
    .AddMemoryCache()

    .AddGraphQLServer()

    // Next we add the types to our schema.
    .AddQueryType(d => d.Name(OperationTypeNames.Query))
                .AddTypeExtension<RolesQueries>()
    .AddMutationType()
    .AddSubscriptionType();
    //.AddTypeExtension<RolesQueries>();

    // In this section we are adding extensions like relay helpers,
    // filtering and sorting.
    //.AddSorting()
    //.AddGlobalObjectIdentification();

   
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors();

app.MapHealthChecks("/healthz");

app.UseWebSockets();
app.UseRouting();

app.UseEndpoints(endpoints =>
{

    endpoints.MapGraphQL()
        .WithOptions(new GraphQLServerOptions
        {
            Tool =
            {
                GaTrackingId = "G-2Y04SFDV8F"
            }
        });

    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("/graphql", true);
        return Task.CompletedTask;
    });

});

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.Run();
