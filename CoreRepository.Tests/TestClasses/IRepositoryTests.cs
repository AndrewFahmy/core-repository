using System.Collections.Generic;
using CoreRepository.Tests.Common;
using CoreRepository.Tests.Models;
using NUnit.Framework;

namespace CoreRepository.Tests
{
    [TestFixture]
    public class IRepositoryTests
    {
        private InMemContext _context = null!;

        [SetUp]
        public void SetUp()
        {
            _context = ContextFactory.CreateContext();
        }

        [Test]
        public void Should_AddANewItemInBasket_When_UsingInsert()
        {
            //arrange
            var usersRepo = new ReadOnlyRepository<User, InMemContext>(_context);
            var productsRepo = new ReadOnlyRepository<Product, InMemContext>(_context);
            var basketsRepo = new Repository<Basket, InMemContext>(_context);

            var user = usersRepo.GetFirstOrDefault(p => p.Id == 1);
            var product = productsRepo.GetFirstOrDefault(p => p.Price == 1500);

            //act
            basketsRepo.Insert(new Basket { ProductId = product.Id, UserId = user.Id });
            var basketItem = basketsRepo.GetFirstOrDefault(p => p.UserId == user.Id);

            //assert
            Assert.NotNull(basketItem);
            Assert.AreEqual(basketItem.UserId, user.Id);
            Assert.AreEqual(product.Id, basketItem.ProductId);
        }

        [Test]
        public void Should_AddANewItemInBasket_When_UpdatingUserObject()
        {
            //arrange
            var usersRepo = new Repository<User, InMemContext>(_context);
            var productsRepo = new ReadOnlyRepository<Product, InMemContext>(_context);
            var basketsRepo = new ReadOnlyRepository<Basket, InMemContext>(_context);

            var user = usersRepo.GetFirstOrDefault(p => p.Id == 2);
            var product = productsRepo.GetFirstOrDefault(p => p.Price == 1200);

            //act
            user.Baskets = new List<Basket> { new Basket { ProductId = product.Id } };

            usersRepo.Update(user);

            var basketItem = basketsRepo.GetFirstOrDefault(p => p.UserId == user.Id);

            //assert
            Assert.NotNull(basketItem);
            Assert.AreEqual(basketItem.UserId, user.Id);
            Assert.AreEqual(product.Id, basketItem.ProductId);
        }
    }
}
