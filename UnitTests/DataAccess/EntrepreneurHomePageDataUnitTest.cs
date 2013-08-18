using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Screen.Vc.DataAccess;
using Ninject;
using Ninject.MockingKernel.Moq;

using Screen.Vc.Interfaces;
using Screen.Vc.Utilities;
using System.Data;
using System.Data.SqlClient;

namespace Screen.Vc.UnitTests.DataAccess
{
    [TestClass]
    public class EntrepreneurHomePageDataUnitTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            m_kernel = new MoqMockingKernel();
            KernelBinding.Initialize(m_kernel);

        }

        [TestMethod]
        public void HappyPathUnitTest()
        {
            var configProvider = m_kernel.Get<IConfigurationProvider>();
            
            var dbCommand = m_kernel.Get<SqlConnection>();
            
            var data = new EntrepreneurHomePageData(configProvider, dbCommand);
            
            data.EntrepreneurId = 1;
            data.Execute();
            Assert.AreEqual(data.EntrepreneurId, 1);
        }

        private MoqMockingKernel        m_kernel;
    }
}
