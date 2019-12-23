using System;
using System.Threading.Tasks;
using CreditRatingService;
using Grpc.Net.Client;

namespace CreditRatingClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Disable TLS
            AppContext.SetSwitch(
                            "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new CreditRatingCheck.CreditRatingCheckClient(channel);
            var creditRequest = new CreditRequest {CustomerId = "id0201", Credit = 7000 };
            var response = await client.CheckCreditRequestAsync(creditRequest);

            Console.WriteLine($"Credit for customer {creditRequest.CustomerId} is {(response.IsAccepted ? "Accepted" : "Rejected")}");
            Console.ReadKey();
        }
    }
}
