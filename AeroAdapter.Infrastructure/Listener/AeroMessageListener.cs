using System;
using System.Threading.Channels;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Helpers;
using AeroAdapter.Infrastructure.Helpers;
using Application.Contracts.GeneratedDtos;
using HID.Aero.ScpdNet.Wrapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AeroAdapter.Infrastructure.Listener;

public sealed class AeroMessageListener(ILogger<AeroMessageListener> logger,Channel<SCPReplyMessageDto> queue,IServiceScopeFactory factory)
{
      private bool _shutdownFlag;

      public void SetShutDownFlag()
      {
            SCPDLL.scpDebugSet((int)enSCPDebugLevel.enSCPDebugToFile);
            Thread.Sleep(100);
            _shutdownFlag = true;
      }

      public void TurnOffDebug()
      {
            bool flag = SCPDLL.scpDebugSet((int)enSCPDebugLevel.enSCPDebugOff);
            if (flag)
            {
                  Console.WriteLine("Debug to file off");
            }
            else
            {
                  Console.WriteLine("Debug to file on");
            }
      }

      //////
      // Method: Turn on debug to file
      //////
      public void TurnOnDebug()
      {
            bool flag = SCPDLL.scpDebugSet((int)enSCPDebugLevel.enSCPDebugToFile);
            if (flag)
            {
                  Console.WriteLine("Debug to file on");
            }
            else
            {
                  Console.WriteLine("Debug to file off");
            }

      }

      public void GetTransactionUntilShutDown()
      {
            while (_shutdownFlag == false)
            {
                  GetTransaction();
            }
      }

      private void GetTransaction()
      {
            var mapper = factory.CreateScope().ServiceProvider.GetRequiredService<IObjectMapper>();
            SCPReplyMessage message = new SCPReplyMessage();
            if (message.GetMessage())
            {
                  ProcessMessage(mapper.Map<SCPReplyMessageDto>(message));
            }

      }

      private void ProcessMessage(SCPReplyMessageDto message)
      {
            // using var scope = scopeFactory.CreateScope();
            switch (message.ReplyType)
            {
                  // Occur when command to SCP not success
                  case (int)enSCPReplyType.enSCPReplyNAK:
                        //queue.Writer.TryWrite(message);
                        logger.LogError(ScpReplyMessageBuilder.BuildNakMessage(message));
                        break;
                  case (int)enSCPReplyType.enSCPReplyTransaction:
                        TransactionHandlerHelper.SCPReplyTransactionHandler(message, queue, logger);
                        break;
                  case (int)enSCPReplyType.enSCPReplyCommStatus:
                  switch (message.comm.status)
                        {
                              case 2:
                                    logger.LogInformation(ScpReplyMessageBuilder.CommStatusMessage(message));
                                    break;
                              default:
                                    logger.LogError(ScpReplyMessageBuilder.CommStatusMessage(message));
                                    break;
                        }
                        queue.Writer.TryWrite(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplyIDReport:
                        logger.LogInformation(ScpReplyMessageBuilder.IdReportMessage(message));
                        queue.Writer.TryWrite(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplyTranStatus:
                        queue.Writer.TryWrite(message);
                        switch (message.tran_sts.disabled)
                        {
                              case 0:
                                    logger.LogInformation(ScpReplyMessageBuilder.TranStatusMessage(message));
                                    break;
                              default:
                                    logger.LogError(ScpReplyMessageBuilder.TranStatusMessage(message));
                                    break;
                        }
                        break;
                  case (int)enSCPReplyType.enSCPReplySrMsp1Drvr:
                  switch (message.sts_drvr.mode)
                        {
                              case 0:
                                    logger.LogError(ScpReplyMessageBuilder.Msp1DrvrMessage(message));
                                    break;
                              default:
                                    logger.LogInformation(ScpReplyMessageBuilder.Msp1DrvrMessage(message));
                                    break;
                        }
                        break;
                  case (int)enSCPReplyType.enSCPReplySrSio:
                        queue.Writer.TryWrite(message);
                        // MessageHandlerHelper.SCPReplySrSio(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplySrMp:
                        queue.Writer.TryWrite(message);
                        // MessageHandlerHelper.SCPReplySrMp(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplySrCp:
                        queue.Writer.TryWrite(message);
                        // MessageHandlerHelper.SCPReplySrCp(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplySrAcr:
                        queue.Writer.TryWrite(message);
                        // MessageHandlerHelper.SCPReplySrAcr(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplySrTz:
                        // MessageHandlerHelper.SCPReplySrTz(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplySrTv:
                        // MessageHandlerHelper.SCPReplySrTv(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplySrMpg:
                        // MessageHandlerHelper.SCPReplySrMpg(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplySrArea:
                        // MessageHandlerHelper.SCPReplySrArea(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplySioRelayCounts:
                        // MessageHandlerHelper.SCPReplySioRelayCounts(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplyStrStatus:
                        queue.Writer.TryWrite(message);
                        // MessageHandlerHelper.SCPReplyStrStatus(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplyCmndStatus:
                        // Save to DB if fail
                        // commandService.HandleSaveFailCommand(command, message);
                        // MessageHandlerHelper.SCPReplyCmndStatus(message);
                        //command.CompleteCommand($"{message.SCPId}/{message.cmnd_sts.sequence_number}",message.cmnd_sts.status == 1);
                        queue.Writer.TryWrite(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplyWebConfigNetwork:
                        // MessageHandlerHelper.SCPReplyWebConfigNetwork(message);
                        queue.Writer.TryWrite(message);
                        break;
                  case (int)enSCPReplyType.enSCPReplyWebConfigNotes:
                        break;
                  case (int)enSCPReplyType.enSCPReplyWebConfigSessionTmr:
                        break;
                  case (int)enSCPReplyType.enSCPReplyWebConfigWebConn:
                        break;
                  case (int)enSCPReplyType.enSCPReplyWebConfigAutoSave:
                        break;
                  case (int)enSCPReplyType.enSCPReplyWebConfigNetDiag:
                        break;
                  case (int)enSCPReplyType.enSCPReplyWebConfigTimeServer:
                        break;
                  case (int)enSCPReplyType.enSCPReplyWebConfigDiagnostics:
                        break;
                  case (int)enSCPReplyType.enSCPReplyWebConfigHostCommPrim:
                        queue.Writer.TryWrite(message);
                        break;

                  default:
                        break;
            }
      }

}
