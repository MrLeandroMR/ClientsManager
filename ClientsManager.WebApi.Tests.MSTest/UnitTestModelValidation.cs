using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientsManager.WebApi.Models.ModelValidations;

namespace ClientsManager.WebApi.Tests.MSTest
{
    [TestClass]
    public class UnitTestModelValidation
    {
        [TestMethod]
        public void CpfValidationTest()
        {
            CpfValidationAttribute cpfValidation = new CpfValidationAttribute();

            Assert.IsTrue(cpfValidation.IsValid("529.982.247-25"));
        }

        [TestMethod]
        public void CpfValidationTest2()
        {
            CpfValidationAttribute cpfValidation = new CpfValidationAttribute();

            Assert.IsFalse(cpfValidation.IsValid("529.982.247-05")); //CPF não válido
        }
    }
}
