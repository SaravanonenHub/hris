

using Core.Entities.Actions;
using Core.Entities.Entries;
using Core.Interfaces;
using Core.Interfaces.IActions;
using Core.Specifications;
using Core.Specifications.EntriesSpec;
using Infrastructure.Data;

namespace ActionStateService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using(var scope = _serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var actionService = scope.ServiceProvider.GetRequiredService<IActionService<ActionHistory>>();
                    var context = scope.ServiceProvider.GetRequiredService<HRISContext>();
                    RequestSpecParams specParams = new RequestSpecParams();
                    specParams.Status = "Submitted";
                    RequestSpec spec = new RequestSpec(specParams);
                    var requests = await unitOfWork.Repository<Request>().ListAsync(spec);
                    if (requests != null)
                    {
                        foreach (var request in requests)
                        {
                            var actionHistory = new ActionHistory
                            {
                                Action = ActionTaken.Closed,
                                ActionBy = "1001",
                                Comment = "Succed",
                                Request = request
                            };
                            var actionResult = await actionService.CreateAction(actionHistory); ;
                        }

                    }
                }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}