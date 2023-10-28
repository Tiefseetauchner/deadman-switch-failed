using System;
using Dapper;

namespace DeadmanSwitchFailed.Common.Domain;

public class GuidTypeHandler : SqlMapper.StringTypeHandler<Guid>
{
  protected override Guid Parse(string guid) =>
    Guid.Parse(guid);

  protected override string Format(Guid guid) =>
    guid.ToString("N");
}