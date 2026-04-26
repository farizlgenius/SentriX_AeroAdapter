using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Entities;
using AeroAdapter.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AeroAdapter.Infrastructure.Repositories;

public sealed class MpRepository(AppDbContext context) : IMpRepository
{
      public async Task<InputPointSpecification> GetInputPointSpecificationByIdAndMacAndSioNumber(short ScpId, string Mac, short SioNumber)
      {
            return await context.InputPointSpecifications.AsNoTracking()
            .OrderByDescending(x => x.id)
            .Where(x => x.scp_id == ScpId && x.mac.Equals(Mac) && x.sio_number == SioNumber)
            .Select(x => new InputPointSpecification(
                  x.scp_id,
                  x.mac,
                  x.sio_number,
                  x.input_number,
                  x.icvt_num,
                  x.debounce,
                  x.hold_time
                  ))
            .FirstOrDefaultAsync() ?? new Domain.Entities.InputPointSpecification();
      }
}
