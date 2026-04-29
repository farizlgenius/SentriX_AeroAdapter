using System;
using AeroAdapter.Domain.Enums;

namespace AeroAdapter.Domain.Entities;

public sealed class Scp
{
     

      public int Id {get; private set;}
      public short ScpId {get; private set;}
      public string Mac {get; private set;} = string.Empty;
      public string Ip {get; private set;} = string.Empty;
      public string SerialNumber {get; private set;} = string.Empty;
      public int Port {get; private set;}   
      public string Fw {get; private set;} = string.Empty;
      public DateTime SyncedAt {get; private set;}
      public ScpSyncStatus SyncStatus {get; private set;} = ScpSyncStatus.SYNC;

      public Scp(){}

       public Scp(int id, short scpId, string mac, string ip,string serialNumber, int port,string fw,DateTime syncedAt,ScpSyncStatus syncStatus)
      {
            Id = id;
            ScpId = scpId;
            Mac = mac;
            Ip = ip;
            SerialNumber = serialNumber;
            Port = port;
            Fw = fw;
            SyncedAt = syncedAt;
            SyncStatus = syncStatus;

      }
}
