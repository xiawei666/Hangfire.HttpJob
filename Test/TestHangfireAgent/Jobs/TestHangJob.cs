﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.HttpJob.Agent;
using Hangfire.HttpJob.Agent.Attribute;
using Microsoft.Extensions.Logging;

namespace TestHangfireAgent.Jobs
{
    [HangJobUntilStop(true)]
    public class TestHangJob : JobAgent
    {
        private readonly ILogger<TestHangJob> _logger;

        public TestHangJob(ILogger<TestHangJob> logger)
        {
            _logger = logger;
            _logger.LogInformation($"Create {nameof(TestHangJob)} Instance Success");
        }
        protected override async Task OnStart(string param)
        {
            await Task.Delay(1000 * 10);
           
            _logger.LogWarning(nameof(OnStart) + (param ?? string.Empty));

            throw new Exception("ddddd");
        }

        protected override void OnStop()
        {
            _logger.LogInformation("OnStop");
        }

        protected override void OnException(Exception ex)
        {
            _logger.LogError(ex, nameof(OnException) + (ex.Data["Method"] ?? string.Empty));
        }
    }
}
