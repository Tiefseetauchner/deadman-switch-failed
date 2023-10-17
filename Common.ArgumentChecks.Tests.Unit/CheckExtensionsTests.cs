using DeadmanSwitchFailed.Common.ArgumentChecks;
using System;

namespace Common.ArgumentChecks.Tests.Unit
{
  [TestFixture]
  public class CheckExtensionsTests
  {
    [Test]
    public void CheckNotNull_Null()
    {
      string isNull = null;

      var exception = Assert.Throws<ArgumentNullException>(() => isNull.CheckNotNull());

      Assert.That(exception.Message, Does.Contain("Parameter 'isNull'"));
    }

    [Test]
    public void CheckNotNull_NotNull()
    {
      string maybeNull = "test";

      string notNull = maybeNull.CheckNotNull();

      Assert.That(notNull, Is.EqualTo(maybeNull));
    }
  }
}