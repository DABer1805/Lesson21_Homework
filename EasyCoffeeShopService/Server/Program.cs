using Server;
using System.Data.SQLite; // Добавляем пространство имен для работы с SQLite

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер.
builder.Services.AddControllers();

// Добавляем поддержку Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем ProductRepository
builder.Services.AddSingleton<IShopRepository>(provider =>
{
    // Создаем базу данных и передаем путь к ней
    string connectPath = "Data Source= DataBase.db"; 
    // Создаем экземпляр репозитория и передаем путь к базе данных SQLite
    IShopRepository shopRepository = new SQLiteShopRepository(connectPath);
    return shopRepository; // Путь к файлу базы данных SQLite
});

var app = builder.Build();

// Настраиваем конвейер обработки HTTP-запросов.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();