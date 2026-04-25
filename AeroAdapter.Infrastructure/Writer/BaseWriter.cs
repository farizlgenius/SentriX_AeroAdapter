using System;
using HID.Aero.ScpdNet.Wrapper;

namespace AeroAdapter.Infrastructure.Writer;

public class BaseWriter
{
      protected bool Send(short command, IConfigCommand cfg)
    {
        SCPConfig scp = new SCPConfig();
        bool success = scp.scpCfgCmndEx(command, cfg);
        return success;
    }
}
