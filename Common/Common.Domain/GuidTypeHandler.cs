using System;
using System.Data;
using Dapper;
using DeadmanSwitchFailed.Common.ArgumentChecks;

namespace DeadmanSwitchFailed.Common.Domain;

public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
  public override void SetValue(IDbDataParameter parameter, Guid value) =>
    parameter.CheckNotNull().Value = value.ToString("N");

  public override Guid Parse(object value) =>
    Guid.Parse((string)value);
}