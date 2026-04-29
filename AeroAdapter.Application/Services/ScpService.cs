using System;
using AeroAdapter.Application.DTOs;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Entities;
using AeroAdapter.Domain.Enums;
using AeroAdapter.Domain.Helpers;
using Application.Contracts.GeneratedDtos;


namespace AeroAdapter.Application.Services;

public sealed class ScpService(IScpRepository repo, IMpRepository mpRepo, IScpWriter writer, ISioWriter sioWriter, IMpWriter mpWriter) : IScpService
{
      public async Task<bool> AssignIpAddressAsync(int ScpId, SCPReplyMessageDto.CC_WEB_CONFIG_NETWORKDto message)
      {
            var mac = await repo.GetMacFromScpIdAsync((short)ScpId);
            if (string.IsNullOrWhiteSpace(mac))
                  return false;
            return await repo.UpdateIpAddressAsync(mac, UtilitiesHelper.IntegerToIp(message.cIpAddr));
      }

      public async Task<bool> AssignPortAsync(int ScpId, SCPReplyMessageDto.CC_WEB_CONFIG_HOST_COMM_PRIMDto message)
      {

            var mac = await repo.GetMacFromScpIdAsync((short)ScpId);
            if (string.IsNullOrWhiteSpace(mac))
                  return false;
            return await repo.UpdatePortAsync(mac, message.ipclient.nPort);
      }

      public async Task HandleIdReport(SCPReplyMessageDto.SCPReplyIDReportDto id)
      {
            // Get Default Settings           
            var spec = await repo.GetScpDeviceSpecificationByIdAndMacAsync(0, string.Empty);
            if (spec.nMsp1Port == 0)
                  // Log here that no database detail
                  return;

            if (!await writer.ScpDeviceSpecification(id.scp_id, spec))
                  return;

            if (!await repo.IsAnyScpWithMacAsync(UtilitiesHelper.ByteToHexStr(id.mac_addr)))
            {

                  var domain = new Scp(0, id.scp_id, UtilitiesHelper.ByteToHexStr(id.mac_addr), string.Empty,id.serial_number.ToString(), 0, $"{id.sft_rev_major}.{id.sft_rev_minor}",DateTime.UtcNow,ScpSyncStatus.UPLOAD);
                  var status = await repo.AddAsync(domain);
                  // Log success
            }
            else
            {
                  // Update ScpId if already Exists
                  var domain = new Scp(0, id.scp_id, UtilitiesHelper.ByteToHexStr(id.mac_addr), string.Empty,id.serial_number.ToString(), 0, $"{id.sft_rev_major}.{id.sft_rev_minor}",DateTime.UtcNow,ScpSyncStatus.UPLOAD);
                  var status = await repo.UpdateAsync(domain);
            }

            var db = await repo.GetAccessDatabaseSpecificationByIdAndMacAsync(0, string.Empty);
            if (db.nCards == 0)
                  // Log here the no database detail
                  return;


            if (!await writer.AccessDatabaseSpecification(id.scp_id, db))
                  return;


            var elev = await repo.GetElevatorAccessLevelSpecificationByIdAndMacAsync(0, string.Empty);
            if (elev.MaxElalvl == 0)
                  // Log here
                  return;

            // if (!await writer.ElevatorAccessLevelSpecification(id.scp_id, elev))
            //       return;

            if (!await writer.TimeSet(id.scp_id))
                  return;


            // Read Structure 
            if (!await writer.SCPStructureStatusRead(id.scp_id,
                  [
                        (short)SCPStructure.SCPSID_TRAN,
                        (short)SCPStructure.SCPSID_TZ,
                        (short)SCPStructure.SCPSID_HOL,
                        (short)SCPStructure.SCPSID_MSP1,
                        (short)SCPStructure.SCPSID_SIO,
                        (short)SCPStructure.SCPSID_MP,
                        (short)SCPStructure.SCPSID_CP,
                        (short)SCPStructure.SCPSID_ACR,
                        (short)SCPStructure.SCPSID_ALVL,
                        (short)SCPStructure.SCPSID_TRIG,
                        (short)SCPStructure.SCPSID_PROC,
                        (short)SCPStructure.SCPSID_MPG,
                        (short)SCPStructure.SCPSID_AREA,
                        (short)SCPStructure.SCPSID_EAL,
                        (short)SCPStructure.SCPSID_CRDB
                  ]
            ))
                  return;


            // Send to get IP and Port 
            await writer.ReadsConfiguration(id.scp_id, WebConfigReadType.NetworkSettingss);
            await writer.ReadsConfiguration(id.scp_id, WebConfigReadType.HostCommunicationPrimarySettings);

      }

      public async Task InitialScpConfiguration(short ScpId)
      {
            

            var config = await repo.GetDriverConfigurationByIdAndMacAndPortNumberAsync(0, string.Empty, 3); // 3 is internal port in x1100
            if (config.Baudrate == 0)
                  // Log here the no database detail
                  return;

            if (!await writer.DriverConfiguration(ScpId, config))
                  return;


            var sio = await repo.GetSioPanelConfigurationByIdAndMacAndAddressAsync(0, string.Empty, 0);
            if (sio.Model == 0)
                  // Log here the no database detail
                  return;

            if (!await sioWriter.SioPanelConfiguration(ScpId, sio))
                  return;


            List<int> inputs = Enumerable.Range(SioModelHelper.nInputByModel(SioModel.x1100) - 3, 3).ToList();

            var input = await mpRepo.GetInputPointSpecificationByIdAndMacAndSioNumber(0, string.Empty, 0);
            if (input.HoldTime == 0)
                  // Log here the no database detail
                  return;


            // Setting Input for Alarm 
            foreach (var i in inputs)
            {
                  input.UpdateInputNumber((short)i);
                  if (!await mpWriter.InputPointSpecification(ScpId, input))
                        return;
            }
      }

      public async Task<bool> SendASCIICommandAsync(ASCIICommandDto Command)
      {
            return writer.SendASCIICommandAsync(Command);
      }

      public async  Task<bool> UploadScpComponentAsync(int ScpId)
      {
            // Query Each Component and Send Command here.
            throw new NotImplementedException();
      }

      public async Task<bool> VerifyScpComponentAsync(int ScpId)
      {
            // Query Each Component and Send Command here.
            // throw new NotImplementedException();
            return true;
      }

      public async Task<bool> VerifySCPStructureMemoryAllocate(int ScpId, SCPReplyMessageDto.SCPReplyStrStatusDto message)
      {
            bool isVerify = true;

            var spec = await repo.GetScpDeviceSpecificationByIdAndMacAsync(0, string.Empty);
            if (spec.nMsp1Port == 0)
                  // Log here that no database detail
                  return false;

            var db = await repo.GetAccessDatabaseSpecificationByIdAndMacAsync(0, string.Empty);
            if (db.nCards == 0)
                  // Log here the no database detail
                  return false;

            var elev = await repo.GetElevatorAccessLevelSpecificationByIdAndMacAsync(0, string.Empty);
            if (elev.MaxElalvl == 0)
                  // Log here
                  return false;

            // Switch
            foreach (var str in message.sStrSpec)
            {
                  switch (str.nStrType)
                  {
                        case (short)SCPStructure.SCPSID_TRAN: // 1 Transactions
                              isVerify = spec.nTransactions > str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_TZ: // 2 Time zones
                              isVerify = spec.nTz + 1 == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_HOL: // 3 Holidays
                              isVerify = spec.nHol == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_MSP1: // 4 Msp1 ports (SIO drivers)
                              // isVerify = spec.nMsp1Port == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_SIO: // 5 SIOs
                              isVerify = spec.nSio == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_MP: // 6 Monitor points
                              isVerify = spec.nMp == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_CP: // 7 Control points
                              isVerify = spec.nCp == str.nRecords;

                              break;

                        case (short)SCPStructure.SCPSID_ACR: // 8 Access control readers
                              isVerify = spec.nAcr == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_ALVL: // 9 Access levels
                              isVerify = spec.nAlvl == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_TRIG: // 10 Triggers
                              isVerify = spec.nTrgr == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_PROC: // 11 Procedures
                              isVerify = spec.nProc == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_MPG: // 12 Monitor point groups
                              isVerify = spec.nMpg == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_AREA: // 13 Access areas
                              // isVerify = spec.nArea == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_EAL: // 14 Elevator access levels
                              // isVerify = elev.MaxElalvl == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_CRDB: // 15 Cardholder database
                              isVerify = db.nCards == str.nRecords;
                              break;

                        case (short)SCPStructure.SCPSID_FLASH: // 20 FLASH specs
                              break;

                        case (short)SCPStructure.SCPSID_BSQN: // 21 Build sequence number
                              break;

                        case (short)SCPStructure.SCPSID_SAVE_STAT: // 22 Flash save status
                              break;

                        case (short)SCPStructure.SCPSID_MAB1_FREE: // 23 Memory alloc block 1 free
                              break;

                        case (short)SCPStructure.SCPSID_MAB2_FREE: // 24 Memory alloc block 2 free
                              break;

                        case (short)SCPStructure.SCPSID_ARQ_BUFFER: // 26 Access request buffers
                              break;

                        case (short)SCPStructure.SCPSID_PART_FREE_CNT: // 27 Partition memory free info
                              break;

                        default:
                              break;
                  }

                  if (!isVerify)
                        break;
            }

            var mac = await repo.GetMacFromScpIdAsync((short)ScpId);
            await repo.UpdateScpAsyncStatusAsync(mac, isVerify ? ScpSyncStatus.SYNC : ScpSyncStatus.RESET, false);
            return isVerify;

      }


}
