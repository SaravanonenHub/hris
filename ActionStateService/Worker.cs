

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
                ApprovalState();
                ProgressState();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }
        private async void ApprovalState()
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var reqService = scope.ServiceProvider.GetRequiredService<IRequestService>();
                var actionService = scope.ServiceProvider.GetRequiredService<IActionService<ActionHistory>>();
                var context = scope.ServiceProvider.GetRequiredService<HRISContext>();
                RequestSpecParams specParams = new RequestSpecParams();
                specParams.Status = ActionTaken.Created;
                RequestSpec spec = new RequestSpec(specParams);
                var requests = await unitOfWork.Repository<Request>().ListAsync(spec);
                if (requests != null)
                {
                    foreach (var request in requests)
                    {
                        var actionHistory = new ActionHistory
                        {
                            Action = ActionTaken.Inapproval,
                            ActionBy = "1001",
                            Comment = "Succeed",
                            Request = request
                        };
                        request.CurrentState = ActionTaken.Inapproval;
                        var req = await reqService.UpdateRequest(request);
                        var actionResult = await actionService.CreateAction(actionHistory);

                    }

                }
            }
            
        }
        private async void ProgressState()
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var reqService = scope.ServiceProvider.GetRequiredService<IRequestService>();
                var actionService = scope.ServiceProvider.GetRequiredService<IActionService<ActionHistory>>();
                var context = scope.ServiceProvider.GetRequiredService<HRISContext>();
                RequestSpecParams specParams = new RequestSpecParams();
                specParams.Status = ActionTaken.Inapproval;
                RequestSpec spec = new RequestSpec(specParams);
                var requests = await unitOfWork.Repository<Request>().ListAsync(spec);
                if (requests != null)
                {
                    foreach (var request in requests)
                    {
                        var actionHistory = new ActionHistory
                        {
                            Action = ActionTaken.Inprogress,
                            ActionBy = "1001",
                            Comment = "Succed",
                            Request = request
                        };
                        request.CurrentState = ActionTaken.Inprogress;
                        var req = await reqService.UpdateRequest(request);
                        var actionResult = await actionService.CreateAction(actionHistory);

                    }

                }
            }

        }

    }
}