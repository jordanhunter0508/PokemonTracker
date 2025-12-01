using DataAccessFakes;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class CollectionManagerTest
{
    ICollectionManager _collectionManager;
    [TestInitialize]
    public void TestSetup() 
    {
        _collectionManager = new CollectionManager(new CollectionAccessorFakes());
    }

    [TestMethod]
    public void TestMethod1()
    {
    }
}
