using System.Threading.Channels;
using HID.Aero.ScpdNet.Wrapper;

namespace AeroAdapter.Worker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<Worker>();
        builder.Services.AddSingleton(
                Channel.CreateBounded<SCPReplyMessage>(
                 new BoundedChannelOptions(10_000)
                    {
                        FullMode = BoundedChannelFullMode.DropOldest,
                        SingleReader = true,
                        SingleWriter = false
                    }
                )
             );

        var host = builder.Build();
        host.Run();
    }
}
