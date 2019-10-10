using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SchoolTripPlanner.Controllers;
using SchoolTripPlanner.Data.Builders;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Tests.Controllers
{
    [TestFixture]
    class ClassesControllerTests
    {
        private IClassRepository _classRepositoryMock;
        private ClassesController _classesController;
        private ICollection<Class> _classes;
        private IUnitOfWork _unitOfWorkMock;
        private ClassBuilder _classBuilder;

        [SetUp]
        public void SetUp()
        {
            _classBuilder = new ClassBuilder();

            _classRepositoryMock = Substitute.For<IClassRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            _classesController = new ClassesController(_unitOfWorkMock);

            for (int i = 1; i <= 5; i++)
            {
                _classes = new List<Class> {_classBuilder.WithId(i).Build()};
            }
        }

        [TearDown]
        public void TearDown()
        {
            _classRepositoryMock = null;
            _unitOfWorkMock = null;
            _classesController = null;
            _classes = null;
        }

        [Test]
        public void Equals_WhenComperingId_ShouldBeTrue()
        {
            //Arrange

            //Act
            _classRepositoryMock.GetAll().Returns(Task.FromResult(_classes.ToList()).Result);

            //Assert
            Assert.AreNotEqual(Task.FromResult(_classes.ToList()), _classRepositoryMock.GetAll());
        }
    }
}