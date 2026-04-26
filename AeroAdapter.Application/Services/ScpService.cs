using System;
using AeroAdapter.Application.DTOs;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Enums;
using AeroAdapter.Domain.Helpers;
using Application.Contracts.GeneratedDtos;


namespace AeroAdapter.Application.Services;

public sealed class ScpService(IScpRepository repo,IMpRepository mpRepo,IScpWriter writer,ISioWriter sioWriter,IMpWriter mpWriter) : IScpService
{
      public async Task HandleIdReport(SCPReplyMessageDto.SCPReplyIDReportDto id)
      {
            // Get Default Settings           
            var spec = await repo.GetScpDeviceSpecificationByIdAndMacAsync(0,string.Empty);
            if(spec.nMsp1Port == 0)
                  // Log here that no database detail
                  return;

            var db = await repo.GetAccessDatabaseSpecificationByIdAndMacAsync(0,string.Empty);
            if(db.nCards == 0)
                  // Log here the no database detail
                  return;

            var config = await repo.GetDriverConfigurationByIdAndMacAndPortNumberAsync(0,string.Empty,3); // 3 is internal port in x1100
            if(config.Baudrate == 0)
                  // Log here the no database detail
                  return;

            var sio = await repo.GetSioPanelConfigurationByIdAndMacAndAddressAsync(0,string.Empty,0);
            if(sio.Model == 0)
                  // Log here the no database detail
                  return;

            var input = await mpRepo.GetInputPointSpecificationByIdAndMacAndSioNumber(0,string.Empty,0);
            if(input.HoldTime == 0)
                  // Log here the no database detail
                  return;

             if(!await repo.IsAnyScpWithMacAsync(UtilitiesHelper.ByteToHexStr(id.mac_addr)))
            {
                  // Update ScpId if already Exists
                  var status = await repo.AddAsync(id.scp_id,UtilitiesHelper.ByteToHexStr(id.mac_addr));
                        // Log success
            }
            else
            {
                  // Save to Db if new
                  var status = await repo.UpdateAsync(id.scp_id,UtilitiesHelper.ByteToHexStr(id.mac_addr));
            }


            if(!await writer.ScpDeviceSpecification(id.scp_id,spec))
                  return;

            if(!await writer.AccessDatabaseSpecification(id.scp_id,db))
                  return;

            if(!await writer.TimeSet(id.scp_id))
                  return;

            if(!await writer.DriverConfiguration(id.scp_id,config))
                  return;

            if(!await sioWriter.SioPanelConfiguration(id.scp_id,sio))
                  return;

            // Setting Input for Alarm 
          for (short i = 0; i < SioModelHelper.nInputByModel(SioModel.x1100); i++)
          {
              if (i + 1 >= SioModelHelper.nInputByModel(SioModel.x1100) - 3)
              {
                  if (!await mpWriter.InputPointSpecification(id.scp_id,input))
                        return;
              }

          }  
      
            
      }

      public async Task<bool> SendASCIICommandAsync(ASCIICommandDto Command)
      {
            return writer.SendASCIICommandAsync(Command);
      }

      public void VerifyAllocateScpMemory(SCPReplyMessageDto.SCPReplyStrStatusDto str)
      {
            throw new NotImplementedException();
      }
}
