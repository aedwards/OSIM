using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;
using NUnit.Framework;
using NBehave.Spec.NUnit;
using Moq;
using NHibernate;

namespace OSIM.UnitTests.OSIM.Core
{

    /// <summary>
    /// CLASS PURPOSE: MAIN BASE CLASS /  Child Class of the Specification class.
    /// Base class for all the unit tests that test the ItemType repository. This class inherits from 
    /// the Specification{} base class in order to identify itself to NUnit as a class that may contain unit tests, 
    /// and receive functionality provided by the NBehave SpecBase{} class. This class does not contain any unit tests. 
    /// It only contains code to set up the test environment, which inevitably, the test classes inherit from.
    /// </summary>
    public class when_working_with_the_item_type_repository : Specification
    {
    }

    /// <summary>
    /// This class contains the unit test for the saving a valid item type feature.
    /// </summary>
    public class and_saving_a_valid_item_type : when_working_with_the_item_type_repository
    {

        /// holds the return value of the current test.
        private int _result;

        ///respository instance
        private IItemTypeRepository _itemTypeRepository;

        /// used to store an item type that is under test 
        private ItemType _testItemType;

        /// variable used to store the entity item id
        private int _itemTypeId;


        /// <summary>
        /// A virtual method inherited from the NBehave class SpecBase, this method
        /// provides a place to create the context under which your tests will run.
        /// </summary>
        protected override void Establish_context()
        {
            base.Establish_context();

            var randomNumberGenerator = new Random();
            _itemTypeId = randomNumberGenerator.Next(32000);

            ///provides a stub value (mocked Session Factory) for the ItemTypeRepository constructor.
            var sessionFactory = new Mock<ISessionFactory>();

            ///create new mock object of the Session class.
            var session = new Mock<ISession>();

            ///supply the Session mock with a stubbed implementation of the Save method.
            session.Setup(s => s.Save(_testItemType)).Returns(_itemTypeId);

            ///stub method for SessionFactory.OpenSession that returns the mocked Session object.
            sessionFactory.Setup(sf => sf.OpenSession()).Returns(session.Object);

            _itemTypeRepository = new ItemTypeRepository(sessionFactory.Object);
        }


        /// <summary>
        /// EXECUTION portion of test. 'Because_of()' is overriden from the NBehave.SpecBase class.
        /// This method is responsible for executing the test. 
        /// It is required to be generic because it will appear in almost every test we do. 
        /// </summary>
        protected override void Because_of()
        {
            //Call code being tested and save the result in _result for later evaluation 
            //by the use of the 'then_a_valid_item_type_id_should_be_returned()' method.
            _result = _itemTypeRepository.Save(_testItemType);
        }

        /// <summary>
        /// Evaluation portion of the test.
        /// </summary>
        [Test]
        public void then_a_valid_item_type_id_should_be_returned()
        {
            //evaluate the result that is returned by the 'Because_of()' function
            _result.ShouldEqual(_itemTypeId);
        }
    }

}
