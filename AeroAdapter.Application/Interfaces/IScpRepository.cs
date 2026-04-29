using System;
using AeroAdapter.Domain.Entities;
using AeroAdapter.Domain.Enums;

namespace AeroAdapter.Application.Interfaces;

public interface IScpRepository
{
      Task<Scp> GetScpByMacAsync(string Mac);
      Task<bool> AddAsync(Domain.Entities.Scp domain);
      Task<bool> UpdateAsync(Domain.Entities.Scp domain);
      Task<bool> UpdateScpIdAsync(short ScpId, string Mac);
      Task<bool> UpdateScpAsyncStatusAsync(string Mac,ScpSyncStatus status,bool IsUploaded);
      Task<bool> UpdateIpAddressAsync(string Mac,string Ip);
      Task<bool> UpdatePortAsync(string Mac,int Port);
      Task<string> GetMacFromScpIdAsync(short ScpId);
      Task<ScpDeviceSpecification> GetScpDeviceSpecificationByIdAndMacAsync(short ScpId,string Mac);
      Task<AccessDatabaseSpecification> GetAccessDatabaseSpecificationByIdAndMacAsync(short ScpId,string Mac);
      Task<bool> IsAnyScpWithMacAsync(string mac);
      Task<DriverConfiguration> GetDriverConfigurationByIdAndMacAndPortNumberAsync(short ScpId,string Mac,short Port);
      Task<SioPanelConfiguration> GetSioPanelConfigurationByIdAndMacAndAddressAsync(short ScpId,string Mac,short Address);
      Task<ElevatorAccessLevelSpecification> GetElevatorAccessLevelSpecificationByIdAndMacAsync(short ScpId,string Mac);
}
