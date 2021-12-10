using System.Collections.Generic;
using System.Linq;
using CoreRepository.Tests.Common;
using CoreRepository.Tests.Models;
using NUnit.Framework;

namespace CoreRepository.Tests
{
    [TestFixture]
    public class RepositoryTests
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

        [Test]
        public void Should_UpdateProductPrice_When_UsingUpdateOnProductObject()
        {
            //arrange
            var productsRepo = new Repository<Product, InMemContext>(_context);

            var product = productsRepo.GetFirstOrDefault(p => p.Id == 8);

            //act
            product.Price = 2000;

            productsRepo.Update(product);

            var updatedProduct = productsRepo.GetFirstOrDefault(p => p.Id == 8);

            //assert
            Assert.NotNull(updatedProduct);
            Assert.AreEqual(updatedProduct.Id, 8);
            Assert.AreEqual(updatedProduct.Price, 2000);
        }

        [Test]
        public void Should_UpdateMultipleProductPrices_When_UsingBulkUpdateOnProductObjects()
        {
            //arrange
            var productsRepo = new Repository<Product, InMemContext>(_context);

            var products = productsRepo.GetQueryable().Where(p => new[] { 6, 7, 8 }.Contains(p.Id)).ToList();

            //act
            products.ForEach(p => p.Price = 5000);

            productsRepo.BulkUpdate(products);

            var updatedProducts = productsRepo.GetQueryable().Where(p => new[] { 6, 7, 8 }.Contains(p.Id)).ToList();

            //assert
            Assert.NotNull(updatedProducts);

            CollectionAssert.AreEquivalent(new[] { 6, 7, 8 }, products.Select(p => p.Id));
            Assert.That(products.All(p => p.Price == 5000));
        }
        
        [Test]
        public void Should_DeleteProduct_When_UsingDeleteOnProductObject()
        {
            //arrange
            var productsRepo = new Repository<Product, InMemContext>(_context);

            var product = productsRepo.GetFirstOrDefault(p => p.Id == 5);

            //act
            productsRepo.Delete(product);

            var deletedProduct = productsRepo.GetFirstOrDefault(p => p.Id == 5);

            //assert
            Assert.IsNull(deletedProduct);
        }

        [Test]
        public void Should_DeleteMultipleProducts_When_UsingBulkDeleteOnProductObjects()
        {
            //arrange
            var productsRepo = new Repository<Product, InMemContext>(_context);

            var products = productsRepo.GetQueryable().Where(p => new[] { 6, 7, 8 }.Contains(p.Id)).ToList();

            //act
            productsRepo.BulkDelete(products);

            var deletedProducts = productsRepo.GetQueryable().Where(p => new[] { 6, 7, 8 }.Contains(p.Id)).ToList();

            //assert
            Assert.IsNotNull(deletedProducts);
            Assert.AreEqual(deletedProducts.Count, 0);
        }
    }
}
