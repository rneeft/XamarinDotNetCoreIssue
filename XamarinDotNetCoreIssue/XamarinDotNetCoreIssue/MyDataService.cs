﻿using Microsoft.Extensions.Logging;

namespace XamarinDotNetCoreIssue
{
    public interface IDataService
    {
        void DoStuff();
    }

    public class MyDataService : IDataService
    {
        // We need access to the ILogger from Microsoft.Extensions so pass it into the constructor
        ILogger<MyDataService> logger;
        public MyDataService(ILogger<MyDataService> logger)
        {
            this.logger = logger;
        }

        public void DoStuff()
        {
            logger.LogCritical("You just called DoStuff from MyDataService");
        }
    }

    public class MockDataService : IDataService
    {
        // We need access to the ILogger from Microsoft.Extensions so pass it into the constructor
        ILogger<MyDataService> logger;
        public MockDataService(ILogger<MyDataService> logger)
        {
            this.logger = logger;
        }

        public void DoStuff()
        {
            logger.LogCritical("You just called DoStuff from MockDataService");
        }
    }

    public class MyViewModel
    {
        IDataService dataService;
        public MyViewModel(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public void DoIt()
        {
            dataService.DoStuff();
        }
    }
}
