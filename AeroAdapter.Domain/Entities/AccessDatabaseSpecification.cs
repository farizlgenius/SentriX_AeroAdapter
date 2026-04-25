using System;

namespace AeroAdapter.Domain.Entities;

public sealed class AccessDatabaseSpecification
{
      public short ScpId {get; private set;}
      public short nCards {get; private set;}
      public short nAlvl {get; private set;}
      public short nPinDigit {get; private set;}
      public short bIssueCode {get;private set;}
      public short bApbLocation {get; private set;}
      public short bActDate {get; private set;}
      public short bDeactDate {get; private set;}
      public short bVacationDate {get; private set;}
      public short bUpgradeDate {get;private set;}
      public short bUserLevel {get; private set;}
      public short bUseLimit {get; private set;}
      public short bSupportTimeApb { get; private set; }
      public short nTz { get; private set; }
      public short bAssetGroup { get; private set; }
      public short nHostResponseTimeout { get; private set; }
      public short nAlvlUse4Arg { get; private set; }
      public short nEscortTimeout {get; private set;}
      public short nMultiCardTimeout {get; private set;}
      public AccessDatabaseSpecification(){}

      public AccessDatabaseSpecification(short scpId, short nCards, short nAlvl, short nPinDigit, short bIssueCode, short bApbLocation, short bActDate, short bDeactDate, short bVacationDate, short bUpgradeDate, short bUserLevel, short bUseLimit, short nSupportTimeApb, short nTz, short bAssetGroup, short nHostResponseTimeout, short nAlvlUse4Arg, short nEscortTimeout, short nMultiCardTimeout)
      {
            ScpId = scpId;
            this.nCards = nCards;
            this.nAlvl = nAlvl;
            this.nPinDigit = nPinDigit;
            this.bIssueCode = bIssueCode;
            this.bApbLocation = bApbLocation;
            this.bActDate = bActDate;
            this.bDeactDate = bDeactDate;
            this.bVacationDate = bVacationDate;
            this.bUpgradeDate = bUpgradeDate;
            this.bUserLevel = bUserLevel;
            this.bUseLimit = bUseLimit;
            this.bSupportTimeApb = nSupportTimeApb;
            this.nTz = nTz;
            this.bAssetGroup = bAssetGroup;
            this.nHostResponseTimeout = nHostResponseTimeout;
            this.nAlvlUse4Arg = nAlvlUse4Arg;
            this.nEscortTimeout = nEscortTimeout;
            this.nMultiCardTimeout = nMultiCardTimeout;
      }
}
