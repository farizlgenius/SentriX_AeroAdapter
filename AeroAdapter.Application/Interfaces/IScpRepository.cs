using System;
using AeroAdapter.Domain.Entities;

namespace AeroAdapter.Application.Interfaces;

public interface IScpRepository
{
      Task<bool> AddAsync(short ScpId,string Mac);
      Task<bool> UpdateAsync(short ScpId, string Mac);
      Task<ScpDeviceSpecification> GetScpDeviceSpecificationByIdAndMacAsync(short ScpId,string Mac);
      Task<AccessDatabaseSpecification> GetAccessDatabaseSpecificationByIdAndMacAsync(short ScpId,string Mac);
      Task<bool> IsAnyScpWithMacAsync(string mac);
      Task<DriverConfiguration> GetDriverConfigurationByIdAndMacAndPortNumberAsync(short ScpId,string Mac,short Port);
      Task<SioPanelConfiguration> GetSioPanelConfigurationByIdAndMacAndAddressAsync(short ScpId,string Mac,short Address);
}
