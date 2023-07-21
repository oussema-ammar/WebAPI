using InfluxDB.Client;

namespace WebAPI.Data
{
    public class InfluxDbDataContext
    {
        private readonly InfluxDBClient _client; 

        public InfluxDbDataContext(IConfiguration configuration)
        {
            var url = configuration.GetValue<string>("InfluxDb:Url");
            var token = configuration.GetValue<string>("InfluxDb:Token");

            _client = InfluxDBClientFactory.Create(url, token);
        }
    }
}
