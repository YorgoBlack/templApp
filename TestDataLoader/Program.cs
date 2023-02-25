using Microsoft.EntityFrameworkCore;
using System.Collections;
using Templ.Domain.Customers;
using Templ.Domain.Customers.ValueObjects;
using Templ.Infrastucture.Repositories;



async void VoidMethod()
{
    await Task.Delay(1000);
    Console.WriteLine("Void");
}

async Task TaskMethod()
{
    await Task.Delay(1000);
    Console.WriteLine("Task");
} 

async Task<T> GenTaskMethod<T>(T data)
{
    await Task.Delay(1000);
    Console.WriteLine("GenTask");
    return await Task.FromResult(data);
}

async Task Some() 
{
    VoidMethod();
    TaskMethod().ConfigureAwait(false).GetAwaiter();
    GenTaskMethod(10).GetAwaiter().GetResult();
}

await Some();

Thread.Sleep(5000);

return;
DbContextOptionsBuilder<EfDbContext> b = new();
b.UseSqlServer("Data Source=DESKTOP-VDQL7RV\\ELEPHANT;TrustServerCertificate=True;Database=templ;Integrated Security=True");
CustomerRespository repo = new( new EfDbContext(b.Options));

Random rnd = new Random();


var radicals = Enumerable.Range(0, 1000).Select(_ =>  GenerateWord(20,rnd)).ToList();

var names = Enumerable.Range(0, 1_000_000)
    .Select(_ => $"{radicals[rnd.Next(radicals.Count)]}{GenerateWord(10, rnd)}");

var l = await repo.FindAll();

int cc = 0;
foreach(var name in names)
{
    await repo.Add(new Customer { 
        Name = name, 
        Company = new Company("Name","Address"), 
        Email = "Email@tyt.net", 
        Phone = ""
    });
    Console.Write($"{++cc}\r");
}

Console.Write("\nSaving ...");

await repo.SaveAsync();

Console.Write("Oki!");

string GenerateWord(int length, Random rnd) =>
    new string(Enumerable.Range(0, length).Select(_ => (char)rnd.Next('A', 'Z')).ToArray());
