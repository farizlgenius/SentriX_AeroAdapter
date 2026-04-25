using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Helpers;
using Application.Contracts.GeneratedDtos;


namespace AeroAdapter.Application.Services;

public sealed class ScpService(IScpRepository repo,IScpWriter writer) : IScpService
{
      public async void HandleIdReport(SCPReplyMessageDto.SCPReplyIDReportDto id)
      {
           
            if(await repo.IsAnyScpWithMacAsync(UtilitiesHelper.ByteToHexStr(id.mac_addr)))
            {
                  // Already Exists
                  var spec = await repo.GetScpDeviceSpecificationByIdAndMacAsync(id.scp_id,UtilitiesHelper.ByteToHexStr(id.mac_addr));
                  if(spec.nMsp1Port == 0)
                        // Log here that no database detail
                        return;

                  var db = await repo.GetAccessDatabaseSpecificationByIdAndMacAsync(id.scp_id,UtilitiesHelper.ByteToHexStr(id.mac_addr));
                  if(db.nCards == 0)
                        // Log here the no database detail
                        return;

                  if(!await writer.ScpDeviceSpecification(id.scp_id,spec))
                        return;

                  if(!await writer.AccessDatabaseSpecification(id.scp_id,db))
                        return;

                  if(!await writer.TimeSet(id.scp_id))
                        return;

                  

            }
            else
            {
                  // New Controller
                  var spec = await repo.GetScpDeviceSpecificationByIdAndMacAsync(0,string.Empty);
                  if(spec.nMsp1Port == 0)
                        // Log here that no database detail
                        return;

                  var db = await repo.GetAccessDatabaseSpecificationByIdAndMacAsync(0,string.Empty);
                  if(db.nCards == 0)
                        // Log here the no database detail
                        return;

                  if(!await writer.ScpDeviceSpecification(id.scp_id,spec))
                        return;

                  if(!await writer.AccessDatabaseSpecification(id.scp_id,db))
                        return;

                   if(!await writer.TimeSet(id.scp_id))
                        return;

            }

            
      }

      public void VerifyAllocateScpMemory(SCPReplyMessageDto.SCPReplyStrStatusDto str)
      {
            
      }
}
