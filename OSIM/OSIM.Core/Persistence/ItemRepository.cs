using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSIM.Core.Entities;
using NHibernate;

namespace OSIM.Core.Persistence
{

    /// <summary>
    /// The ItemTypeRepository class houses the persistence (data access) logic for the OSIM application.
    /// It implements the IItemTypeRepository interface.
    /// </summary>

    public interface IItemTypeRepository
    {
        int Save(ItemType itemType);
    }

    public class ItemTypeRepository : IItemTypeRepository
    {

        ///This variable holds the session object necessary for calls back and forth to the repository.
        private ISessionFactory _sessionFactory;

        /// <summary>
        /// ItemTypeRepository constructor that allows a class that implements ISessionFactory
        /// to be supplied when ItemTypeRepository is created.
        /// </summary>
        /// <param name="sessionFactory"></param>
        public ItemTypeRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }


        /// <summary>
        /// Method that saves the curent entity
        /// </summary>
        /// <param name="itemType">Required item type</param>
        /// <returns></returns>
        public int Save(ItemType itemType)
        {
            int id;
            using (var session = _sessionFactory.OpenSession())
            {
                id = (int)session.Save(itemType);
                session.Flush();
            }

            return id;
        }
    }
}
