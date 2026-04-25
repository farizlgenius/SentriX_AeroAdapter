using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Entities;
using AeroAdapter.Domain.Enums;
using AeroAdapter.Domain.Helpers;
using AeroAdapter.Infrastructure.Helpers;
using HID.Aero.ScpdNet.Wrapper;
using Microsoft.Extensions.Logging;

namespace AeroAdapter.Infrastructure.Writer;

public sealed class ScpWriter(ILogger<ScpWriter> logger,IWriterRepository writer) : BaseWriter,IScpWriter
{
      public async Task<bool> AccessDatabaseSpecification(short ScpId,AccessDatabaseSpecification spec)
      {
            CC_SCP_ADBS c = new CC_SCP_ADBS();
            c.lastModified = 0;
            c.nScpID = spec.ScpId;
            c.nCards = spec.nCards;
            c.nAlvl = spec.nAlvl;
            c.nPinDigits = spec.nPinDigit;
            c.bIssueCode = spec.bIssueCode;
            c.bApbLocation = spec.bApbLocation;
            c.bActDate = spec.bActDate;
            c.bDeactDate = spec.bDeactDate;
            c.bVacationDate = spec.bVacationDate;
            c.bUpgradeDate = spec.bUpgradeDate;
            c.bUserLevel = spec.bUserLevel;
            c.bUseLimit = spec.bUseLimit;
            c.bSupportTimedApb = spec.bSupportTimeApb;
            c.nTz = spec.nTz;
            c.bAssetGroup = spec.bAssetGroup;
            c.nHostResponseTimeout = spec.nHostResponseTimeout;
            c.nMxmTypeIndex = 0;
            c.nAlvlUse4Arq = spec.nAlvlUse4Arg;
            c.nFreeformBlockSize = 0;
            c.nEscortTimeout = spec.nEscortTimeout;
            c.nMultiCardTimeout = spec.nMultiCardTimeout;
            c.nAssetTimeout = 0;
             var result = Send((short)enCfgCmnd.enCcScpAdbSpec,c);
            if (result)
            {
                  logger.LogInformation(MessageHelper.CommandSuccess(WriterType.AccessDatabaseSpecification,ScpId));
                  await writer.AddWriterAuditAsync(ScpId,WriterType.AccessDatabaseSpecification,SCPDLL.scpGetTagLastPosted(ScpId),MessageHelper.ToJsonString(c));
                  return true;
                  
            }
            else
            {
                  logger.LogError(MessageHelper.CommandUnsuccess(WriterType.AccessDatabaseSpecification,ScpId));
                  return false;
                 
            }


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
            logger.LogInformation("Create channel command sent successfully.");
        return result;
    }

      public async Task<bool> ScpDeviceSpecification(short ScpId,ScpDeviceSpecification spec)
      {
    
           CC_SCP_SCP c = new CC_SCP_SCP();
           c.lastModified = 0;
           c.number = ScpId;
           c.ser_num_low = 0;
           c.ser_num_high = 0;
           c.rev_major = 0;
           c.rev_minor = 0;
           c.nMsp1Port = spec.nMsp1Port;
           c.nTransactions = spec.nTransactions;
           c.nSio = spec.nSio;
           c.nMp = spec.nMp;
           c.nCp = spec.nCp;
           c.nAcr = spec.nAcr;
           c.nAlvl = spec.nAlvl;
           c.nTrgr = spec.nTrgr;
           c.nProc = spec.nProc;
           c.gmt_offset = spec.GmtOffset;
           c.nDstID = spec.nDstId;
           c.nTz = spec.nTz;
           c.nHol = spec.nHol;
           c.nMpg = spec.nMpg;
           c.nTranLimit = spec.nTranLimit;
           c.nAuthModType = 0;
           c.nOperModes = spec.nOperMode;
           c.oper_type = spec.OperType;
           c.nLanguages = spec.nLanguage;
           c.nSrvcType = 0;
           var result = Send((short)enCfgCmnd.enCcScpScp,c);
            if (result)
            {
                  logger.LogInformation(MessageHelper.CommandSuccess(WriterType.ScpDeviceSpecification,ScpId));
                  await writer.AddWriterAuditAsync(ScpId,WriterType.ScpDeviceSpecification,SCPDLL.scpGetTagLastPosted(ScpId),MessageHelper.ToJsonString(c));
                  return true;
                  
            }
            else
            {
                  logger.LogError(MessageHelper.CommandUnsuccess(WriterType.ScpDeviceSpecification,ScpId));
                  return false;
                 
            }
                  
      }

      public async Task<bool> TimeSet(short ScpId)
      {
            CC_TIME c = new CC_TIME();
            c.scp_number = ScpId;
            c.custom_time = 0;
            var result = Send((short)enCfgCmnd.enCcTime,c);
            if (result)
            {
                  logger.LogInformation(MessageHelper.CommandSuccess(WriterType.TimeSet,ScpId));
                  await writer.AddWriterAuditAsync(ScpId,WriterType.TimeSet,SCPDLL.scpGetTagLastPosted(ScpId),MessageHelper.ToJsonString(c));
                  return true;
                  
            }
            else
            {
                  logger.LogError(MessageHelper.CommandUnsuccess(WriterType.TimeSet,ScpId));
                  return false;
                 
            }
      }

      public async Task<bool> WebConfigRead(short ScpId, WebConfigReadType Type)
      {
            CC_WEB_CONFIG_READ c = new CC_WEB_CONFIG_READ();
            c.scp_number = ScpId;
            c.read_type = (short)Type;
            var result = Send((short)enCfgCmnd.enCcWebConfigRead,c);
            if (result)
            {
                  logger.LogInformation(MessageHelper.CommandSuccess(WriterType.ReadWebConfig,ScpId));
                  await writer.AddWriterAuditAsync(ScpId,WriterType.ReadWebConfig,SCPDLL.scpGetTagLastPosted(ScpId),MessageHelper.ToJsonString(c));
                  return true;
                  
            }
            else
            {
                  logger.LogError(MessageHelper.CommandUnsuccess(WriterType.ReadWebConfig,ScpId));
                  return false;
                 
            }

      }
}
