using System;
using AeroAdapter.Domain.Entities;

namespace AeroAdapter.Application.Interfaces;

public interface IScpRepository
{
      Task<ScpDeviceSpecification> GetScpDeviceSpecificationByIdAndMacAsync(short ScpId,string Mac);
      Task<AccessDatabaseSpecification> GetAccessDatabaseSpecificationByIdAndMacAsync(short ScpId,string Mac);
      Task<bool> IsAnyScpWithMacAsync(string mac);
}
