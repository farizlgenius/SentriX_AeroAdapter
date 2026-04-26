using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Enums;
using AeroAdapter.Infrastructure.Helpers;
using AeroAdapter.Infrastructure.Persistences;
using AeroAdapter.Infrastructure.Persistences.Entities;
using Application.Contracts.GeneratedDtos;
using HID.Aero.ScpdNet.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace AeroAdapter.Infrastructure.Repositories;

public class WriterRepository(AppDbContext context) : IWriterRepository
{
     

      public async Task AddWriterAuditAsync(int ScpId, WriterType Command, int Tag, string Body,string? status = "PENDING")
      {
            await context.WriterAudits.AddAsync(
                        new WriterAudit
                        {
                              mac= await context.Scps.AsNoTracking().Where(x => x.scp_number == ScpId).Select(x => x.mac).FirstOrDefaultAsync() ?? "",
                              scp_id = ScpId,
                              command_code = (short)Command,
                              command = Command.ToString(),
                              tag = Tag,
                              send_at = DateTime.UtcNow,
                              received_at = DateTime.UtcNow,
                              body = Body,
                              status = status ?? "PENDING",
                              is_nak = false,
                              reason = "",
                              updated_at = DateTime.UtcNow,
                              created_at = DateTime.UtcNow
                        }
                  );

            await context.SaveChangesAsync();
      }

      public async Task<bool> IsAnyByScpIdAndTagIdAsync(int ScpId, int Tag)
      {
            var mac = await context.Scps.Where(x => x.scp_number == ScpId).Select(x => x.mac).FirstOrDefaultAsync();
            if(mac == null)
                  return false;

            return await context.WriterAudits.AsNoTracking()
            .AnyAsync(x => x.mac.Equals(mac) && x.tag == Tag);
      }

      public async Task UpdateWriterAuditAsync(int ScpId, int Tag,SCPReplyMessageDto.SCPReplyCmndStatusDto message)
      {
            var mac = await context.Scps.Where(x => x.scp_number == ScpId).Select(x => x.mac).FirstOrDefaultAsync();
            var entity = await context.WriterAudits.Where(x => x.scp_id == ScpId && x.mac == mac && x.tag == Tag && x.status.Equals(WriterStatus.PENDING.ToString())).FirstOrDefaultAsync();
            if(entity == null)
                  return;

            entity.Update(message.status == (short)ScpCommandStatus.OK ? WriterStatus.SUCESS.ToString() : WriterStatus.FAILED.ToString(),message.status != (short)ScpCommandStatus.NAK ? false : true,message.status == (short)ScpCommandStatus.NAK ? DescriptionHelper.GetNakReasonDescription(message.nak.reason) :string.Empty );
            context.WriterAudits.Update(entity);
            await context.SaveChangesAsync();
      }
}
