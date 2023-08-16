using HotChocolate.AspNetCore;
using Infrastructure;
using Application;
using Microsoft.Extensions.DependencyInjection;
using graphql_skeleton.Queries;
using HotChocolate.AspNetCore.Voyager;
using graphql_skeleton.Mutations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors(o =>
        o.AddDefaultPolicy(b =>
            b.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()));

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks();

// This adds the GraphQL server core service and declares a schema.
builder.Services
    .AddMemoryCache()
    .AddGraphQLServer()
    .AddQueryType()

   // Next we add the types to our schema.
    .AddMutationType()
   //.AddSubscriptionType()
    .AddTypeExtension<RolesQueries>()
    .AddTypeExtension<RolesMutations>()
   // In this section we are adding extensions like relay helpers,
   // filtering and sorting.
    .AddFiltering()
    .AddSorting();
// .AddGlobalObjectIdentification();


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
    // We will be using the new routing API to host our GraphQL middleware.
    endpoints.MapGraphQL()
        .WithOptions(new GraphQLServerOptions
        {
            Tool =
            {
                GaTrackingId = "G-2Y04SFDV8F"
            }
        });

    app.UseVoyager("/graphql", "/graphql-voyager");

    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("/graphql", true);
        return Task.CompletedTask;
    });
});




app.Run();
