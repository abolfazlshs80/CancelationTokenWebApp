using System.Diagnostics;
using CancelationTokenWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CancelationTokenWebApp.Controllers
{
    public class HomeController(ILogger<HomeController> _logger) : Controller
    {

        public async Task<IActionResult> Index(CancellationToken token)
        {
         //   await RunWithCancellationTokenAndUseTaskMethod(token);
            await RunWithCancellationTokenAndUseWithOutTaskMethod(token);

            _logger.LogInformation("View index Showed");
            return View("Privacy");
        }



        //use mthod token option
        public async Task RunWithCancellationTokenAndUseTaskMethod(CancellationToken token)
        {
            _logger.LogInformation("Action index Start");
            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(1000, cancellationToken: token);
                _logger.LogCritical("Action index Running " + i);
            }
            _logger.LogInformation("Action index End");
        }
        //use exception
        public async Task RunWithCancellationTokenAndUseWithOutTaskMethod(CancellationToken token)
        {
            _logger.LogInformation("Action index Start");
            for (int i = 0; i < 5; i++)
            {
                token.ThrowIfCancellationRequested();
                Thread.Sleep(1000);
             
                _logger.LogCritical("Action index Running " + i);
            }
            _logger.LogInformation("Action index End");
        }

        //dont use exception and check status token
        public async Task RunWithCancellationTokenAndUsePropertyWithOutTaskMethod(CancellationToken token)
        {
            _logger.LogInformation("Action index Start");
            for (int i = 0; i < 5; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                Thread.Sleep(1000);

                _logger.LogCritical("Action index Running " + i);
            }
            _logger.LogInformation("Action index End");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
