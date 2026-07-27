using Microsoft.Extensions.Configuration;
using Serilog;
namespace CommonUtility
{
    public class InsuranceManagementSystemLogger
    {
        private const string Path = "C:\\Users\\vetri\\source\\repos\\InsuranceManagementSystemMVC\\InsuranceManagementSystemMVC\\appsettings.json";

        public void BuildConfigure()
        {
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile(Path).Build();       // .AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration().
                         ReadFrom.Configuration(configuration)
                         .CreateLogger();
        }
    }
}


