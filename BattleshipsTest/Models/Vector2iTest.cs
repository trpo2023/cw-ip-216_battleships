using Battleships.Models.Primitive;

namespace BattleshipsTest;

[TestClass]
public class Vector2iTest
{
    [TestMethod]
    public void ConstructorsTest()
    {
        Vector2i v1 = new();
        Vector2i v2 = new(3, 5);

        Assert.AreEqual(v1.x, 0);
        Assert.AreEqual(v1.y, 0);

        Assert.AreEqual(v2.x, 3);
        Assert.AreEqual(v2.y, 5);
    }

    [TestMethod]
    public void MakeOffsetTest()
    {
        Vector2i vector = new(1, 5);
        vector.MakeOffset(10);

        Assert.AreEqual(vector, new Vector2i(2, 5));

        vector = new(9, 5);
        vector.MakeOffset(10);

        Assert.AreEqual(vector, new Vector2i(0, 6));

        vector = new(9, 9);
        vector.MakeOffset(10);

        Assert.AreEqual(vector, new Vector2i(0, 0));
    }

    [TestMethod]
    public void GetRandomVectorTest()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector2i v = Vector2i.GetRandomVector(10, 10);
            Assert.IsTrue(v.x < 10 || v.x >= 0);
            Assert.IsTrue(v.y < 10 || v.y >= 0);
        }
    }
}