using System;

namespace AeroAdapter.Domain.Entities;

public sealed class DriverConfiguration
{
     

      public short ScpId { get; private set; }
      public string Mac {get; private set;} = string.Empty;
      public short Msp1Number { get; private set; }
      public short PortNumber { get; private set; }
      public short Baudrate { get; private set; }
      public short ReplyTime { get; private set; }
      public short nProtocol { get; private set; }
      public short nDialect { get; private set; }

      public DriverConfiguration(){}

       public DriverConfiguration(short scpId,string mac,short msp1Number, short portNumber, short baudrate, short replyTime, short nProtocol, short nDialect)
      {
            ScpId = scpId;
            Mac = mac;
            Msp1Number = msp1Number;
            PortNumber = portNumber;
            Baudrate = baudrate;
            ReplyTime = replyTime;
            this.nProtocol = nProtocol;
            this.nDialect = nDialect;
      }
}
