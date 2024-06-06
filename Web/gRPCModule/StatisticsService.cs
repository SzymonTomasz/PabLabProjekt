using Application.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Web.Middleware;

namespace Web.gRPCModule
{

    public class StatisticsService : Statistics.StatisticsBase
    {
        int currentValue;

        public StatisticsService(CounterClass counter)
        {
            currentValue = counter.Value;
        }
        public override Task<GetStatisticsResponse> GetStatistics(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new GetStatisticsResponse
            {
                Counter = currentValue
            });
        }
    }
}