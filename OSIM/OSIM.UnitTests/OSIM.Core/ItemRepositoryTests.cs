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
        ///Instance variable for the respository instance. Relies on the Mock of SessionFactory. 
        protected IItemTypeRepository _itemTypeRepository;

        ///Instance variable for the Mock of SessionFactory.  Relies on Mock of Session.
        protected Mock<ISessionFactory> _sessionFactory;

        ///Instance variable Session.
        protected Mock<ISession> _session;

        /// <summary>
        /// A virtual method inherited from the NBehave class SpecBase.
        /// This method is inherited by the tests to provide the CONTEXT under which they will
        /// run so as to reduce code duplication.
        /// </summary>
        protected override void Establish_context()
        {
            base.Establish_context();

            ///provides a stub value (mocked Session Factory) for the ItemTypeRepository constructor.
            _sessionFactory = new Mock<ISessionFactory>();

            ///create new mock object of the Session class.
            _session = new Mock<ISession>();
            
            ///stub method for SessionFactory.OpenSession that returns the mocked Session object.
            _sessionFactory.Setup(sf => sf.OpenSession()).Returns(_session.Object);

            ///new instace of the ItemTypeRepository with mock of the SessionFactory object as its parameter,
            _itemTypeRepository = new ItemTypeRepository(_sessionFactory.Object);
        }
    }


    /// <summary>
    /// This class contains the unit test for the saving a valid item type feature.
    /// </summary>
    public class and_saving_a_valid_item_type : when_working_with_the_item_type_repository
    {

        /// holds the return value of the current test.
        private int _result;

        /// used to store an item type that is under test 
        private ItemType _testItemType;

        /// variable used to store the entity item id
        private int _itemTypeId;


        /// <summary>
        /// A virtual method inherited from the NBehave class SpecBase, this method
        /// provides a place to create the CONTEXT under which your tests will run.
        /// </summary>
        protected override void Establish_context()
        {
            base.Establish_context();

            var randomNumberGenerator = new Random();
            _itemTypeId = randomNumberGenerator.Next(32000);

            ///supply the Session mock with a stubbed implementation of the Save method.
            _session.Setup(s => s.Save(_testItemType)).Returns(_itemTypeId);
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


    /// <summary>
    /// This class tests if a null object has been written to the database.  If so, the repository throws
    /// an exeption.
    /// </summary>
    public class and_saving_an_invalid_item_type : when_working_with_the_item_type_repository
    {

        /// <summary>
        /// _result is declared as type Exception because even though we the call to ItemTypeRepository
        /// to raise an exception, we want to verify the type of exception. If we were more specific with the
        /// type, such as declaring it as an ArgumentNullException type, if any other exception was thrown,
        /// a runtime error would occur.
        /// </summary>
        private Exception _result;


        /// <summary>
        /// Sets up the context that 'then_an_argument_null_exception_should_be_raised()' will run under.
        /// </summary>
        protected override void Establish_context()
        {
            base.Establish_context();

            ///Setup a stub for the Save method that reacts to the value null
            _session.Setup(s => s.Save(null)).Throws(new ArgumentNullException());
        }


        /// <summary>
        /// EXECUTION portion of test.
        /// This method executes the Save method of the current _itemRepository instance on a null object.
        /// Any subsequent exceptions are captured in the catch portion of the try/catch block.
        /// </summary>
        protected override void Because_of()
        {
            try
            {
                _itemTypeRepository.Save(null);
            }
            catch (Exception exception)
            {
                _result = exception;
            }
        }

        /// <summary>
        /// EVALUATION portion of test.
        /// This method validates that an exception of type ArgumentNullEception was thrown.
        /// </summary>
        [Test]
        public void then_an_argument_null_exception_should_be_raised()
        {
            _result.ShouldBeInstanceOfType(typeof(ArgumentNullException));
        }

    }


}
