using System;
using AeroAdapter.Domain.Enums;

namespace AeroAdapter.Application.Interfaces;

public interface IWriterRepository
{
      Task AddWriterAuditAsync(int ScpId,WriterType Command,int Tag,string Body);
      Task UpdateWriterAuditAsync(int ScpId,int Tag);
}
