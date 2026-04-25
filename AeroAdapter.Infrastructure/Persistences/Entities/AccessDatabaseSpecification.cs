using System;
using RabbitMQ.Client;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public sealed class AccessDatabaseSpecification : BaseEntity
{
      public short scp_id {get; set;}
      public string mac {get; set;} = string.Empty;
      public short n_card {get; set;}
      public short n_alvl {get; set;}
      public short n_pin_digits {get; set;}
      public short b_issue_code {get; set;}
      public short b_apb_location {get; set;}
      public short b_act_date {get; set;}
      public short b_deact_date {get; set;}
      public short b_vacation_date {get; set;}
      public short b_upgrade_date {get;set;}
      public short b_user_level {get;set;}
      public short b_use_limit {get; set;}
      public short b_support_time_apb {get; set;}
      public short n_tz {get; set;}
      public short b_asset_group {get; set;}
      public short n_host_response_timeout {get; set;}
      public short n_alvl_use4arg {get; set;}
      public short n_escort_timeout {get; set;}
      public short n_multi_card_timeout {get;set;}

      public AccessDatabaseSpecification(){}
}
