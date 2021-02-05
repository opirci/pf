using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pars.Core.Data;

namespace Pars.Util.Puma.Data.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<tb_Claim> claimList = new List<tb_Claim>();

            claimList.Add(new tb_Claim() { ClaimRef = 1, Name = "Test Claim 1"});
            claimList.Add(new tb_Claim() { ClaimRef = 2, Name = "Test Claim 2" });
            claimList.Add(new tb_Claim() { ClaimRef = 3, Name = "Test Claim 3" });
            claimList.Add(new tb_Claim() { ClaimRef = 4, Name = "Test Claim 4" });
            claimList.Add(new tb_Claim() { ClaimRef = 5, Name = "Test Claim 5" });



            Mock<IDataRepository<tb_Claim>> mockClaimRep = new Mock<IDataRepository<tb_Claim>>(MockBehavior.Strict);
            mockClaimRep.Setup(s => s.Table).Returns(claimList.AsQueryable());


            Mock<IDataRepository<tb_Claim_Locale>> mockClaimLocaleRep = new Mock<IDataRepository<tb_Claim_Locale>>(MockBehavior.Strict);

            Mock<IDataRepository<tb_EntityState>> mockEntityStateRep = new Mock<IDataRepository<tb_EntityState>>(MockBehavior.Strict);

            Mock<IDataRepository<tb_EntityState_Locale>> mockEntityStateLocaleRep = new Mock<IDataRepository<tb_EntityState_Locale>>(MockBehavior.Strict);

            Mock<IDataRepository<tb_UserClaim>> mockUserClaimRep = new Mock<IDataRepository<tb_UserClaim>>(MockBehavior.Strict);

            Mock<IPumaUnitOfWork> pMock = new Mock<IPumaUnitOfWork>(MockBehavior.Strict);
            pMock.Setup(s => s.Claim).Returns(mockClaimRep.Object);

        }
    }
}
