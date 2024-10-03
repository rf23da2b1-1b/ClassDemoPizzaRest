using ClassDemoPizzaLib.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPizzaRepository>(new PizzaRepository(true));

builder.Services.AddCors(opt =>

        opt.AddPolicy("AllowGetPut",
              builder => builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "PUT")
              )
);

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.UseCors("AllowGetPut");

app.MapControllers();

app.Run();
