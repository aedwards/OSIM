using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace OSIM.UnitTests
{

    /// <summary>
    /// This provides a base class for unit tests written in a behavior-driven-development (BDD) style. 
    /// It ensures that unit test classes derive from the NBehave class Specbase{} and that all derived 
    /// classes are marked as unit tests for NUnit.
    /// </summary>
    [TestFixture]
    public class Specification : SpecBase
    {
    }
}