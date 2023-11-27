using ActionStateService;
using Core.Entities.Actions;
using Core.Interfaces;
using Core.Interfaces.IActions;
using Infrastructure.Data;
using Infrastructure.Data.Services;
using Infrastructure.Data.Services.ActionsRepo;
using Microsoft.EntityFrameworkCore;
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostcontext, services) =>
    {
        services.AddDbContext<HRISContext>(opt =>
        {
            opt.UseSqlServer(hostcontext.Configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IRequestService, RequestService>();
        services.AddTransient<IActionService<ActionHistory>, ActionService<ActionHistory>>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
