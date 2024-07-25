using DeviceSimulatorController.Model;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceSimulatorController.Services
{
    public class TableStorageService
    {
        private readonly CloudTable _table;
        private readonly ILogger<TableStorageService> _logger;

        public TableStorageService(IConfiguration configuration, ILogger<TableStorageService> logger)
        {
            _logger = logger;
            var azureStorageConfig = configuration.GetSection("AzureStorage");
            var connectionString = azureStorageConfig["ConnectionString"];
            var tableName = azureStorageConfig["TableName"];

            if (string.IsNullOrEmpty(connectionString))
            {
                _logger.LogError("Azure Storage ConnectionString is null or empty.");
                throw new ArgumentNullException(nameof(connectionString));
            }
            if (string.IsNullOrEmpty(tableName))
            {
                _logger.LogError("Azure Storage TableName is null or empty.");
                throw new ArgumentNullException(nameof(tableName));
            }

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            _table = tableClient.GetTableReference(tableName);
        }

        public async Task<List<IoTData>> GetDataBetweenTimesAsync(long startTime, long endTime)
        {
            var query = new TableQuery<IoTData>().Where(
            TableQuery.CombineFilters(
            TableQuery.GenerateFilterConditionForLong("Timestamp", QueryComparisons.GreaterThanOrEqual, startTime),
            TableOperators.And,
            TableQuery.GenerateFilterConditionForLong("Timestamp", QueryComparisons.LessThanOrEqual, endTime)));

            var result = new List<IoTData>();
            TableContinuationToken token = null;

            do
            {
                var queryResult = await _table.ExecuteQuerySegmentedAsync(query, token);
                result.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            return result;
        }
    }
}