using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using Person.Data;
using Person.Models;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Person.Routes
{
    public static class PersonRoute
    {
        public static void PersonRoutes(this WebApplication app)
        {

            var route = app.MapGroup("person");

            // app.MapGet("", () => new PersonModel("leandro",true));

            route.MapPost("",
                async (PersonContext context, PersonRequest request) =>
                {
                    var person = new PersonModel(request.name, request.status);
                    await context.AddAsync(person);
                    await context.SaveChangesAsync();
                });


            route.MapGet("",
                async (PersonContext context) =>
                {
                    var people = await context.People.ToListAsync();

                    if (people == null)
                        return Results.NotFound("Não encontrado");

                    return Results.Ok(people);
                });

            route.MapPut("{Id:guid}",
                async (Guid Id, PersonRequest request, PersonContext context) =>
                {
                    var people = await context.People.FindAsync(Id);

                    if (people == null)
                        return Results.NotFound("Não encontrado");

                    people.ChangeName(request.name);
                    await context.SaveChangesAsync();

                    return Results.Ok(people);

                });


            route.MapDelete("{Id:guid}",
                async (Guid Id, PersonContext context ) =>
                {
                    var people = await context.People.FindAsync(Id);
                    if (people == null)
                        return Results.NotFound("Não encontrado");

                    people.ChangeStatus();
                    await context.SaveChangesAsync();


                    return Results.Ok();

                });






        }
    }
}
