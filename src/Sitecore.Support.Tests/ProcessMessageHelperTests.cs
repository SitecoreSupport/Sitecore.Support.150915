namespace Sitecore.Support.Tests
{
  using System;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Sitecore.Support.Form.Core.Pipelines.ProcessMessage;

  [TestClass]
  public class ProcessMessageHelperTests
  {
    [TestMethod]
    public void PlaintextTest()
    {
      var mail =
        @"[<label id=""{D751B390-8D81-47CE-9283-C81967DBA7E3}"">test</label>]" +
        @"<p><br></p>" +
        @"[<label id=""{9619AF1C-AD9E-4567-9AB2-FC859D3F22AD}"">ホスト更改 &rArr; CPUモデル</label>]";

      var id = "{D751B390-8D81-47CE-9283-C81967DBA7E3}";
      var expectedResult =
        @"FIELD_VALUE" +
        @"<p><br></p>" +
        @"[<label id=""{9619AF1C-AD9E-4567-9AB2-FC859D3F22AD}"">ホスト更改 &rArr; CPUモデル</label>]";

      var value = "FIELD_VALUE";
      var actualResult = ProcessMessageHelper.ReplaceFieldValue(mail, id, value);

      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void EmptyFieldTest()
    {
      var mail =
        @"[<label id=""{D751B390-8D81-47CE-9283-C81967DBA7E3}""></label>]" +
        @"<p><br></p>" +
        @"[<label id=""{9619AF1C-AD9E-4567-9AB2-FC859D3F22AD}"">ホスト更改 &rArr; CPUモデル</label>]";

      var id = "{D751B390-8D81-47CE-9283-C81967DBA7E3}";
      var expectedResult =
        @"FIELD_VALUE" +
        @"<p><br></p>" +
        @"[<label id=""{9619AF1C-AD9E-4567-9AB2-FC859D3F22AD}"">ホスト更改 &rArr; CPUモデル</label>]";

      var value = "FIELD_VALUE";
      var actualResult = ProcessMessageHelper.ReplaceFieldValue(mail, id, value);

      Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void JapaneseWithSpecialCharTest()
    {
      var mail =
        @"[<label id=""{D751B390-8D81-47CE-9283-C81967DBA7E3}"">test</label>]" +
        @"<p><br></p>" +
        @"[<label id=""{9619AF1C-AD9E-4567-9AB2-FC859D3F22AD}"">ホスト更改 &rArr; CPUモデル</label>]";

      var id = "{9619AF1C-AD9E-4567-9AB2-FC859D3F22AD}";
      var expectedResult =
        @"[<label id=""{D751B390-8D81-47CE-9283-C81967DBA7E3}"">test</label>]" +
        @"<p><br></p>" +
        @"FIELD_VALUE";

      var value = "FIELD_VALUE";
      var actualResult = ProcessMessageHelper.ReplaceFieldValue(mail, id, value);

      Assert.AreEqual(expectedResult, actualResult);
    }
  }
}
