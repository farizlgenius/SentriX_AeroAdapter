using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Entities;
using AeroAdapter.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AeroAdapter.Infrastructure.Repositories;

public sealed class ScpRepository(AppDbContext context) : IScpRepository
{
      public async Task<AccessDatabaseSpecification> GetAccessDatabaseSpecificationByIdAndMacAsync(short ScpId,string Mac)
      {
            return await context.AccessDatabaseSpecifications
            .AsNoTracking()
            .Where(x => x.scp_id == ScpId && x.mac.Equals(Mac))
            .Select(x => new AccessDatabaseSpecification(
                  x.scp_id,
                  x.n_card,
                  x.n_alvl,
                  x.n_pin_digits,
                  x.b_issue_code,
                  x.b_apb_location,
                  x.b_act_date,
                  x.b_deact_date,
                  x.b_vacation_date,
                  x.b_upgrade_date,
                  x.b_user_level,
                  x.b_use_limit,
                  x.b_support_time_apb,
                  x.n_tz,
                  x.b_asset_group,
                  x.n_host_response_timeout,
                  x.n_alvl_use4arg,
                  x.n_escort_timeout,
                  x.n_multi_card_timeout
            )).FirstOrDefaultAsync() ?? new AccessDatabaseSpecification();
            
      }

      public async Task<ScpDeviceSpecification> GetScpDeviceSpecificationByIdAndMacAsync(short ScpId,string Mac)
      {
            return await context.ScpDeviceSpecifications
            .AsNoTracking()
            .Where(x => x.scp_id == ScpId && x.mac.Equals(Mac))
            .Select(x => 
                  new ScpDeviceSpecification(
                        x.n_msp1_port,
                        x.n_transcations,
                        x.n_sio,
                        x.n_mp,
                        x.n_cp,
                        x.n_acr,
                        x.n_alvl,
                        x.n_trgr,
                        x.n_proc,
                        x.gmt_offset,
                        x.n_dst_id,
                        x.n_tz,
                        x.n_hol,
                        x.n_mpg,
                        x.n_tran_limit,
                        x.n_oper_mode,
                        x.oper_type,
                        x.n_language
                  )
            ).FirstOrDefaultAsync() ?? new ScpDeviceSpecification();
      }

      public async Task<bool> IsAnyScpWithMacAsync(string mac)
      {
            return await context.Scps.AsNoTracking().AnyAsync(x => x.mac.Equals(mac));
      }
}
