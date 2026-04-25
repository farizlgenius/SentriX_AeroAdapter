using System;
using AeroAdapter.Domain.Entities;
using AeroAdapter.Domain.Enums;


namespace AeroAdapter.Application.Interfaces;

public interface IScpWriter
{
      // Below Command need to reset controller if change
      Task<bool> ScpDeviceSpecification(short ScpId,ScpDeviceSpecification Spec);
      Task<bool> AccessDatabaseSpecification(short ScpId,AccessDatabaseSpecification Spec);

      // End

      Task<bool> TimeSet(short ScpId);
      
      bool CreateChannel();
      
      Task<bool> WebConfigRead(short ScpId,WebConfigReadType Type);  

}
