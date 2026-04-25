using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Enums;
using AeroAdapter.Infrastructure.Persistences;
using AeroAdapter.Infrastructure.Persistences.Entities;
using HID.Aero.ScpdNet.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace AeroAdapter.Infrastructure.Repositories;

public class WriterRepository(AppDbContext context) : IWriterRepository
{
     

      public async Task AddWriterAuditAsync(int ScpId, WriterType Command, int Tag, string Body)
      {
            await context.WriterAudits.AddAsync(
                        new WriterAudit
                        {
                              mac= await context.Scps.AsNoTracking().Where(x => x.scp_number == ScpId).Select(x => x.mac).FirstOrDefaultAsync() ?? "",
                              scp_id = ScpId,
                              command_code = (short)WriterType.ScpDeviceSpecification,
                              command = WriterType.ScpDeviceSpecification.ToString(),
                              tag = SCPDLL.scpGetTagLastPosted((short)ScpId),
                              send_at = DateTime.UtcNow,
                              received_at = DateTime.UtcNow,
                              body = Body,
                              status = WriterStatus.PENDING.ToString(),
                              is_nak = false,
                              reason = ""
                        }
                  );
      }

      public async Task UpdateWriterAuditAsync(int ScpId, int Tag)
      {

            throw new NotImplementedException();
      }
}
