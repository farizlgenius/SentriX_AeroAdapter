using System;
using AeroAdapter.Domain.Enums;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public sealed class Scp : BaseEntity
{
      public short scp_id {get; set;}
      public string mac {get; set;} = string.Empty;
      public string ip {get; set;} = string.Empty;
      public string serial_number {get; set;} = string.Empty;
      public int port {get; set;}
      public string fw {get; set;} = string.Empty;
      public DateTime synced_at {get; set;}
      public ScpSyncStatus sync_status {get; set;} = ScpSyncStatus.SYNC;
    
      public Scp(){}

      public Scp(Domain.Entities.Scp domain)
      {
            this.scp_id = domain.ScpId;
            this.mac = domain.Mac;
            this.ip = domain.Ip;
            this.port = domain.Port;
            this.fw = domain.Fw;
            this.created_at = DateTime.UtcNow;
            this.updated_at = DateTime.UtcNow;
      }

      public void UpdateScpId(short scp_number)
      {
            this.scp_id = scp_number;
            this.updated_at = DateTime.UtcNow;
      }

      public void UpdatePort(int Port)
      {
            this.port = Port;
            this.updated_at = DateTime.UtcNow;
      }

      public void UpdateIp(string ip)
      {
            this.ip = ip;
            this.updated_at = DateTime.UtcNow;
      }

      public void UpdateSyncStatus(ScpSyncStatus status,bool isUpload = false)
      {
            this.updated_at = DateTime.UtcNow;
            this.sync_status = status;
            if(isUpload)
            {
                  this.synced_at = DateTime.UtcNow;
            }
      }

      public void Update(Domain.Entities.Scp domain)
      {
            this.scp_id = domain.ScpId;
            this.mac = domain.Mac;
            this.ip = domain.Ip;
            this.serial_number = domain.SerialNumber;
            this.port = domain.Port;
            this.fw = domain.Fw;
            this.updated_at = DateTime.UtcNow;
      }
}
