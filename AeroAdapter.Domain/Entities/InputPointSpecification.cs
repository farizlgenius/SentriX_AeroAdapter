using System;

namespace AeroAdapter.Domain.Entities;

public sealed class InputPointSpecification
{
    

      public short ScpId { get; private set; }
      public string Mac {get; private set;} = string.Empty;
      public short SioNumber {get; private set;}
      public short InputNumber { get; private set; }
      public short IcvtNum { get; private set; }
      public short Debounce { get; private set; }
      public short HoldTime { get; private set; }

      public InputPointSpecification(){}

        public InputPointSpecification(short scpId,string mac,short sioNumber, short inputNumber, short icvtNum, short debounce, short holdTime)
      {
            ScpId = scpId;
            Mac = mac;
            SioNumber = sioNumber;
            InputNumber = inputNumber;
            IcvtNum = icvtNum;
            Debounce = debounce;
            HoldTime = holdTime;
      }
}
