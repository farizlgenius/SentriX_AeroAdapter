using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Entities;
using AeroAdapter.Domain.Enums;
using AeroAdapter.Domain.Helpers;
using HID.Aero.ScpdNet.Wrapper;
using Microsoft.Extensions.Logging;

namespace AeroAdapter.Infrastructure.Writer;

public sealed class SioWriter(ILogger<SioWriter> logger, IWriterRepository writer) : BaseWriter,ISioWriter
{
      public async Task<bool> SioPanelConfiguration(short ScpId, SioPanelConfiguration config)
      {
            CC_SIO c = new CC_SIO();
        c.lastModified = 0;
        c.scp_number = ScpId;
        c.sio_number = config.SioNumber;
        c.nInputs = config.nInputs;
        c.nOutputs = config.nOutputs;
        c.nReaders = config.nReaders;
        c.model = config.Model;
        c.revision = 0;
        c.ser_num_low = 0;
        c.ser_num_high = -1;
        c.enable = config.Enable;
        c.port = config.Port;
        c.channel_out = 0;
        c.channel_in = 0;
        c.address = config.Address;
        c.e_max = config.EMax;
        c.flags = config.Flags;
        c.nSioNextIn = config.nSioNextIn;
        c.nSioNextOut = config.nSioNextOut;
        c.nSioNextRdr = config.nSioNextRdr;
        c.nSioConnectTest = 0;
        c.nSioOemCode = 0;
        c.nSioOemMask = 0;
        var result = Send((short)enCfgCmnd.enCcSio,c);
        if (result)
            {
                  logger.LogInformation(MessageHelper.CommandSuccess(WriterType.SioPanelConfiguration,ScpId));
                  await writer.AddWriterAuditAsync(ScpId,WriterType.SioPanelConfiguration,SCPDLL.scpGetTagLastPosted(ScpId),MessageHelper.ToJsonString(c));
                  return true;
                  
            }
            else
            {
                  logger.LogError(MessageHelper.CommandUnsuccess(WriterType.SioPanelConfiguration,ScpId));
                  return false;
                 
            }
      }
}
