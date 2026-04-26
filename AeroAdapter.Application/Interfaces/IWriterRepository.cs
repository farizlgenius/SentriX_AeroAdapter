using System;
using AeroAdapter.Domain.Enums;
using Application.Contracts.GeneratedDtos;

namespace AeroAdapter.Application.Interfaces;

public interface IWriterRepository
{
      Task AddWriterAuditAsync(int ScpId,WriterType Command,int Tag,string Body,string? status="PENDING");
      Task UpdateWriterAuditAsync(int ScpId,int Tag,SCPReplyMessageDto.SCPReplyCmndStatusDto message);
      Task<bool> IsAnyByScpIdAndTagIdAsync(int ScpId,int Tag);
}
