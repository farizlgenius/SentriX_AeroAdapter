using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Infrastructure.Persistences;
using HID.Aero.ScpdNet.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace AeroAdapter.Infrastructure.Writer;

public sealed class CommandWrite(AppDbContext context) : ICommandWriter
{
    public bool SystemLevelSpecification()
    {
        var data = context.SystemLevelSpecifications.OrderByDescending(x => x.id).FirstOrDefault();
        if(data == null)
            return false;

        CC_SYS c = new CC_SYS();
        c.nPorts = data.n_ports;
        c.nScps = data.n_scps;
        c.nTimezones = data.n_timezones;
        c.nHolidays = data.n_holidays;
        c.bDirectMode = data.b_direct_mode;
        c.debug_rq = data.debug_rq;
        for(int i= 0;i < c.nDebugArg.Length;i++)
        {
            c.nDebugArg[i] = data.n_debug_arg;
        }
        var result = Send((short)enCfgCmnd.enCcSystem,c);
        if(result)
            Console.WriteLine("System level specification command sent successfully.");
        return result;
    }

    public bool CreateChannel()
    {

        CC_CHANNEL c = new CC_CHANNEL();
        c.nChannelId = 1;
        c.cType = 7;
        c.cPort = 3333;
        c.baud_rate = 0;
        c.timer1 = 3000;
        c.timer2 = 0;
        for(int i= 0;i < c.cModemId.Length;i++)
        {
            c.cModemId[i] = '\0';
        }
        c.cRTSMode = 0;
        var result = Send((short)enCfgCmnd.enCcCreateChannel,c);
        if(result)
            Console.WriteLine("Create channel command sent successfully.");
        return result;
    }

    //private readonly ConcurrentDictionary<Guid, int> _pending = new();
    //////
    // Method: SendCommand to Driver
    // PreCondition: SCP online
    // PostCondition: Pass/Fail response is sent from the driver
    //////
    private bool Send(short command, IConfigCommand cfg)
    {
        SCPConfig scp = new SCPConfig();
        bool success = scp.scpCfgCmndEx(command, cfg);
        return success;
    }
}
