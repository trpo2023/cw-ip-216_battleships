using Battleships.Models.Primitive;

namespace BattleshipsTest;

[TestClass]
public class RectangleTest
{

    [TestMethod]
    public void constructorsTest()
    {
        Rectangle r1 = new();
        Assert.AreEqual(r1, new Rectangle(new Vector2i(0, 0), new Vector2i(0, 0)));
        Rectangle r2 = new(new Vector2i(1, 2), new Vector2i(3, 4));
    }

    [TestMethod]
    public void GetCollisionTest()
    {
        var rect = new Rectangle(new Vector2i(5, 4), new Vector2i(9, 7));

        Assert.IsTrue(rect.GetCollision(new Vector2i(5, 4)));
        Assert.IsTrue(rect.GetCollision(new Vector2i(9, 7)));

        Assert.IsTrue(rect.GetCollision(new Vector2i(5, 5)));
        Assert.IsTrue(rect.GetCollision(new Vector2i(5, 6)));
        Assert.IsTrue(rect.GetCollision(new Vector2i(6, 5)));

        Assert.IsFalse(rect.GetCollision(new Vector2i(5, 3)));
        Assert.IsFalse(rect.GetCollision(new Vector2i(4, 4)));
        Assert.IsFalse(rect.GetCollision(new Vector2i(10, 10)));
    }

    [TestMethod]
    public void GetPositionsSetTest()
    {
        // #####
        // #####
        // #*###
        // #####
        // ###*#
        var rect = new Rectangle(new Vector2i(1, 2), new Vector2i(3, 4));
        var actualSet = rect.GetPositionsSet();
        HashSet<Vector2i> posSet = new() {
            new Vector2i(1,2),
            new Vector2i(1,3),
            new Vector2i(1,4),

            new Vector2i(2,2),
            new Vector2i(2,3),
            new Vector2i(2,4),

            new Vector2i(3,2),
            new Vector2i(3,3),
            new Vector2i(3,4),
        };

        Assert.IsTrue(posSet.SetEquals(actualSet));
    }

    [TestMethod]
    public void GetSizesTest()
    {
        var rect = new Rectangle(new Vector2i(1, 2), new Vector2i(3, 6));
        Assert.AreEqual(3, rect.GetWidth());
        Assert.AreEqual(5, rect.GetHeight());
    }

    [TestMethod]
    public void GetOutlinePositionsSetTest()
    {
        // #*###
        // #####
        // #####
        // ####*
        var rect = new Rectangle(new Vector2i(1, 0), new Vector2i(4, 3));
        var actualSet = rect.GetOutlinePositionsSet();
        HashSet<Vector2i> outlineset = new() {
            new Vector2i(1,0),
            new Vector2i(2,0),
            new Vector2i(3,0),
            new Vector2i(4,0),

            new Vector2i(1,1),
            new Vector2i(1,2),
            new Vector2i(4,1),
            new Vector2i(4,2),

            new Vector2i(1,3),
            new Vector2i(2,3),
            new Vector2i(3,3),
            new Vector2i(4,3),
        };

        Assert.IsTrue(actualSet.SetEquals(outlineset));
    }

}