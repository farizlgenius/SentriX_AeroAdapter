using System;

namespace AeroAdapter.Domain.Entities;

public sealed class ScpDeviceSpecification
{
      public short nMsp1Port {get; private set;}
      public int nTransactions {get; private set;}
      public short nSio {get; private set;}
      public short nMp {get; private set;}
       public short nCp {get; private set;}
      public short nAcr {get; private set;}
      public short nAlvl {get; private set;}
      public short nTrgr {get; private set;}
      public short nProc {get; private set;}
      public short GmtOffset {get; private set;}
      public short nDstId {get; private set;}
      public short nTz {get; private set;}
      public short nHol {get; private set;}
      public short nMpg {get; private set;}
      public int nTranLimit {get; private set;}
      public short nOperMode {get; private set;}
      public short OperType {get; private set;} 
      public short nLanguage {get; private set;} 

      public ScpDeviceSpecification(){}

      public ScpDeviceSpecification(short nMsp1Port,int nTransactions,short nSio,short nMp,short nCp,short nAcr,short nAlvl,short nTrgr,short nProc,short GmtOffset,short nDstId,short nTz,short nHol,short nMpg,int nTranLimit,short nOperMode,short OperType,short nLanguage)
      {
            this.nMsp1Port = nMsp1Port;
            this.nTransactions = nTransactions;
            this.nSio = nSio;
            this.nMp = nMp;
            this.nCp = nCp;
            this.nAcr = nAcr;
            this.nAlvl = nAlvl;
            this.nTrgr = nTrgr;
            this.nProc = nProc;
            this.GmtOffset = GmtOffset;
            this.nDstId = nDstId;
            this.nTz = nTz;
            this.nHol = nHol;
            this.nMp = nMpg;
            this.nTranLimit = nTranLimit;
            this.nOperMode = nOperMode;
            this.OperType = OperType;
            this.nLanguage = nLanguage;

      }
}
