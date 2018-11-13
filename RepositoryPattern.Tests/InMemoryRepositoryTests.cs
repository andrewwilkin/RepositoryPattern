using System;
using System.Collections;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RepositoryPattern.Interfaces;
using RepositoryPattern.Providers;
using RepositoryPattern.Tests;
using RepositoryPattern.Tests.Models;
using Assert = NUnit.Framework.Assert;


[TestFixture]
public class InMemoryRepositoryTests
{
    private IRepository<TestStoreable> _repository;
    private TestStoreable _testStoreable1;
    private TestStoreable _testStoreable1Different;
    private TestStoreable _testStoreable2;
    private TestStoreable _testStoreable3;


    [SetUp]
    public void CreateInMemoryRepository()
    {
        _repository = new InMemoryRepository<TestStoreable>();
        _testStoreable1 = new TestStoreable { Id = 1, Message = "Test Storeable 1" };
        _testStoreable2 = new TestStoreable { Id = 2, Message = "Test Storeable 2" };
        _testStoreable3 = new TestStoreable {Id = Guid.NewGuid(), Message = "Test Storeable 3"};
        _testStoreable1Different = new TestStoreable { Id = 1, Message = "Test Storeable 1 Different"};       
    }

    [Test]
    public void SaveAddItemsToEmptyRepository()
    {
        _repository.Save(_testStoreable1);
        _repository.Save(_testStoreable2);
        _repository.Save(_testStoreable3);

        Assert.That(_repository.All().Count(), Is.EqualTo(3));
        Assert.That(_repository.All().Contains(_testStoreable1));
        Assert.That(_repository.All().Contains(_testStoreable2));
        Assert.That(_repository.All().Contains(_testStoreable3));
    }

    [Test]
    public void SaveReplacesExitingItem()
    {
        _repository.Save(_testStoreable1);
        _repository.Save(_testStoreable1Different);
        Assert.That(_repository.All().All(x => x.Message != "Test Storeable 1"));
        Assert.That(_repository.All().All(x => x.Message == "Test Storeable 1 Different"));
    }

    [Test]
    public void DeleteNonExistentItem()
    {
        Assert.That(() => { _repository.Delete(_testStoreable1.Id); }, Throws.Nothing);
        Assert.That(_repository.All().Count(), Is.EqualTo(0));
    }

    [Test]
    public void DeleteItemRemovesCorrectItem()
    {
        _repository.Save(_testStoreable1);
        _repository.Save(_testStoreable2);
        _repository.Save(_testStoreable3);
        _repository.Delete(_testStoreable2.Id);
        Assert.That(_repository.All().Count(), Is.EqualTo(2));
        Assert.That(_repository.All().Contains(_testStoreable2), Is.False);
    }

    [Test]
    public void AllIsEnumerable()
    {
        Assert.That(_repository.All(), Is.InstanceOf<IEnumerable>());
    }

    [Test]
    public void FindReturnsCorrectItem()
    {
        _repository.Save(_testStoreable1);
        _repository.Save(_testStoreable2);
        _repository.Save(_testStoreable3);
        var item = _repository.FindById(_testStoreable2.Id);
        Assert.That(item.Message, Is.EqualTo(_testStoreable2.Message));       
    }

    [Test]
    public void FindReturnsNull()
    {
        var item = _repository.FindById(_testStoreable2.Id);
        Assert.That(item, Is.Null);
    }
    
    [Test]
    public void ThrowsNullReferenceException()
    {
        TestStoreable testNull = null;
        Assert.That(() => { _repository.Save(testNull); }, Throws.Exception.TypeOf<NullReferenceException>());
    }

    [Test]
    public void ThrowsArgumentNullReferenceException()
    {
        var testNull = new TestStoreable { Message = "Ooops" };
        Assert.That(() => { _repository.Save(testNull); }, Throws.Exception.TypeOf<ArgumentNullException>());
    }

}