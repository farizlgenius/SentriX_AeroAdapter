using System;

namespace AeroAdapter.Domain.Entities;

public sealed class SioPanelConfiguration
{

      public short ScpId { get; private set; }
      public string Mac {get; private set;} = string.Empty;
      public short SioNumber { get; private set; }
      public short nInputs { get; private set; }
      public short nOutputs { get; private set; }
      public short nReaders { get; private set; }
      public short Model { get; private set; }
      public short Enable { get; private set; }
      public short Port { get; private set; }
      public short Address { get; private set; }
      public short EMax { get; private set; }
      public short Flags { get; private set; }
      public short nSioNextIn { get; private set; }
      public short nSioNextOut { get; private set; }
      public short nSioNextRdr { get; private set; }

      public SioPanelConfiguration(){}

      
      public SioPanelConfiguration(short scpId, string mac, short sioNumber, short nInputs, short nOutputs, short nReaders, short model, short enable, short port, short address, short eMax, short flags, short nSioNextIn, short nSioNextOut, short nSioNextRdr)
      {
            ScpId = scpId;
            Mac = mac;
            SioNumber = sioNumber;
            this.nInputs = nInputs;
            this.nOutputs = nOutputs;
            this.nReaders = nReaders;
            Model = model;
            Enable = enable;
            Port = port;
            Address = address;
            EMax = eMax;
            Flags = flags;
            this.nSioNextIn = nSioNextIn;
            this.nSioNextOut = nSioNextOut;
            this.nSioNextRdr = nSioNextRdr;
      }
}
