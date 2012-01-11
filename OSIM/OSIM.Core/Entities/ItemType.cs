using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSIM.Core.Entities
{
    /// <summary>
    /// ItemType is a persistable class.
    /// </summary>

    public class ItemType
    {
        /// Used to store and retrieve the unique id given to the entity which is stored in the database.
        public virtual int Id
        {
            get;
            set;
        }
    }
}