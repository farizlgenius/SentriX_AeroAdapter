using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Infrastructure.Persistences;
using HID.Aero.ScpdNet.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace AeroAdapter.Infrastructure.Writer;

public sealed class CommandWrite(AppDbContext context) : ICommandWriter
{
    public async Task<bool> SystemLevelSpecification()
    {
        var data = await context.SystemLevelSpecifications.OrderByDescending(x => x.id).FirstOrDefaultAsync();
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
        return Send((short)enCfgCmnd.enCcSystem,c);
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
