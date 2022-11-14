using EntityFrameworkNullPK.EntityFramework.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    internal class TestCases
    {
        private TestDataContext _testContext;

        [SetUp]
        public void Setup()
        {
            _testContext = new TestDataContext();
            _testContext.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _testContext.Reset();
        }

        [Test]
        public async Task SeedDataAsync()
        {
            CompanyEntity newCompany = new CompanyEntity();
            _testContext.Add(newCompany);
            await _testContext.SaveChangesAsync();

            UserEntity newUser = new UserEntity();
            _testContext.Add(newUser);
            await _testContext.SaveChangesAsync();
        }
    }
}
