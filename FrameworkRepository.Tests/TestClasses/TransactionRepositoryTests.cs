using System;
using FrameworkRepository.Tests.Common;
using FrameworkRepository.Tests.Models;
using NUnit.Framework;

namespace FrameworkRepository.Tests.TestClasses
{
    [TestFixture]
    public class TransactionRepositoryTests
    {
        private InMemContext _context = null!;

        [SetUp]
        public void SetUp()
        {
            _context = ContextFactory.CreateContext();
        }


        [Test]
        public void Should_CommitTransactionAndSaveData_When_CallingCreateTransactionAndSaveChanges()
        {
            //arrange
            var historyRepo = new TransactionRepository<History, InMemContext>(_context);

            _ = historyRepo.CreateTransaction();

            //act
            historyRepo.Insert(new History { UserId = 1, ProductId = 1, Price = 2.50, CreationDate = DateTime.Now });

            historyRepo.SaveChanges();

            var historyItem = historyRepo.GetFirstOrDefault(p => p.UserId == 1);
            
            //assert
            Assert.NotNull(historyItem);
            Assert.AreEqual(historyItem.UserId, 1);
            Assert.AreEqual(historyItem.ProductId, 1);
            Assert.AreEqual(historyItem.Price, 2.50);
        }


        [Test]
        public void Should_NotSaveData_When_CallingCreateTransactionOnly()
        {
            //arrange
            var historyRepo = new TransactionRepository<History, InMemContext>(_context);

            _ = historyRepo.CreateTransaction();

            //act
            historyRepo.Insert(new History { UserId = 1, ProductId = 1, Price = 2.50, CreationDate = DateTime.Now });

            var historyItem = historyRepo.GetFirstOrDefault(p => p.UserId == 1);            
            
            //assert
            Assert.Null(historyItem);
        }

    }
}
