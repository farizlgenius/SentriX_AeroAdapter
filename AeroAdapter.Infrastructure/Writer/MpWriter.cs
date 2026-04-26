using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Entities;
using AeroAdapter.Domain.Enums;
using AeroAdapter.Domain.Helpers;
using HID.Aero.ScpdNet.Wrapper;
using Microsoft.Extensions.Logging;

namespace AeroAdapter.Infrastructure.Writer;

public sealed class MpWriter(ILogger<MpWriter> logger,IWriterRepository writer) : BaseWriter,IMpWriter
{
      
      public async Task<bool> InputPointSpecification(short ScpId, InputPointSpecification spec)
      {
            CC_IP c = new CC_IP();
            c.lastModified = 0;
            c.scp_number = ScpId;
            c.sio_number = spec.SioNumber;
            c.input = spec.InputNumber;
            c.icvt_num = spec.IcvtNum;
            c.debounce = spec.Debounce;
            c.hold_time = spec.HoldTime;
            var result = Send((short)enCfgCmnd.enCcInput,c);
            if (result)
            {
                  logger.LogInformation(MessageHelper.CommandSuccess(WriterType.InputPointSpecification,ScpId));
                  await writer.AddWriterAuditAsync(ScpId,WriterType.InputPointSpecification,SCPDLL.scpGetTagLastPosted(ScpId),MessageHelper.ToString(c));
                  return true;
                  
            }
            else
            {
                  logger.LogError(MessageHelper.CommandUnsuccess(WriterType.InputPointSpecification,ScpId));
                  await writer.AddWriterAuditAsync(ScpId,WriterType.InputPointSpecification,0,MessageHelper.ToString(c),WriterStatus.FAILED.ToString());
                  return false;
                 
            }
      }
}
