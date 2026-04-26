using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Entities;
using AeroAdapter.Infrastructure.Persistences;
using AeroAdapter.Infrastructure.Persistences.Entities;
using Microsoft.EntityFrameworkCore;

namespace AeroAdapter.Infrastructure.Repositories;

public sealed class ScpRepository(AppDbContext context) : IScpRepository
{
      public async Task<bool> AddAsync(short ScpId, string Mac)
      {
            var data = await context.Scps.AddAsync(
                  new Scp
                  {
                        mac = Mac,
                        scp_number = ScpId,
                        created_at = DateTime.UtcNow,
                        updated_at = DateTime.UtcNow
                        
                  }
            );

            var save = await context.SaveChangesAsync();
            if(data.Entity != null || save > 0)
                  return true;

            return false;
      }

      public async Task<Domain.Entities.AccessDatabaseSpecification> GetAccessDatabaseSpecificationByIdAndMacAsync(short ScpId,string Mac)
      {
            return await context.AccessDatabaseSpecifications
            .AsNoTracking()
            .Where(x => x.scp_id == ScpId && x.mac.Equals(Mac))
            .Select(x => new Domain.Entities.AccessDatabaseSpecification(
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
            )).FirstOrDefaultAsync() ?? new Domain.Entities.AccessDatabaseSpecification();
            
      }

      public async Task<Domain.Entities.DriverConfiguration> GetDriverConfigurationByIdAndMacAndPortNumberAsync(short ScpId, string Mac, short Port)
      {
            return await context.DriverConfigurations
            .AsNoTracking()
            .Where(x => x.scp_id == ScpId && x.mac.Equals(Mac) && x.port_number == Port)
            .Select(x => new Domain.Entities.DriverConfiguration(
                  x.scp_id,
                  x.mac,
                  x.msp1_number,
                  x.port_number,
                  x.baudrate,
                  x.reply_time,
                  x.n_protocol,
                  x.n_dialect
                  )).FirstOrDefaultAsync() ?? new Domain.Entities.DriverConfiguration() ;
      }

      public async Task<Domain.Entities.ScpDeviceSpecification> GetScpDeviceSpecificationByIdAndMacAsync(short ScpId,string Mac)
      {
            return await context.ScpDeviceSpecifications
            .AsNoTracking()
            .Where(x => x.scp_id == ScpId && x.mac.Equals(Mac))
            .Select(x => 
                  new Domain.Entities.ScpDeviceSpecification(
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
            ).FirstOrDefaultAsync() ?? new Domain.Entities.ScpDeviceSpecification();
      }

      public async Task<Domain.Entities.SioPanelConfiguration> GetSioPanelConfigurationByIdAndMacAndAddressAsync(short ScpId, string Mac, short Address)
      {
            return await context.SioPanelConfigurations
            .AsNoTracking()
            .Where(x => x.scp_id == ScpId && x.mac.Equals(Mac))
            .Select(x => 
                  new Domain.Entities.SioPanelConfiguration(
                        x.scp_id,
                        x.mac,
                        x.sio_number,
                        x.n_inputs,
                        x.n_outputs,
                        x.n_readers,
                        x.model,
                        x.enable,
                        x.port,
                        x.address,
                        x.emax,
                        x.flags,
                        x.n_sio_next_in,
                        x.n_sio_next_out,
                        x.n_sio_next_rdr

                  )
            ).FirstOrDefaultAsync() ?? new Domain.Entities.SioPanelConfiguration();
      }

      public async Task<bool> IsAnyScpWithMacAsync(string mac)
      {
            return await context.Scps.AsNoTracking().AnyAsync(x => x.mac.Equals(mac));
      }

      public async Task<bool> UpdateAsync(short ScpId, string Mac)
      {
            var entity = await context.Scps.Where(x => x.mac.Equals(Mac)).FirstOrDefaultAsync();
            if(entity == null)
                  return false;

            entity.Update(ScpId);
            var save = await context.SaveChangesAsync();
            if(save <= 0)
                  return false;

            return true;
      }
}
