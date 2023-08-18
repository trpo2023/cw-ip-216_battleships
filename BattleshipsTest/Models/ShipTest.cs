using Battleships.Models.Primitive;

using Battleships.Models;

namespace BattleshipsTest;

[TestClass]
public class ShoTest
{
    Ship ship1;
    Ship ship2;
    Ship ship3;

    [TestInitialize]
    public void Initialize()
    {
        ship1 = new Ship(new Vector2i(3, 4), 3, Ship.Orientation.Horizontal);
        ship2 = new Ship(new Vector2i(1, 5), 4, Ship.Orientation.Vertical);
        ship3 = new Ship(new Vector2i(6, 9), 2, Ship.Orientation.Horizontal);
        // ##########
        // ##########
        // ##########
        // ##########
        // ###000####
        // #0########
        // #0########
        // #0########
        // #0########
        // ######00##
    }

    [TestMethod]
    public void EndPositionTest()
    {
        Assert.AreEqual(new Vector2i(5, 4), ship1.EndPosition);
        Assert.AreEqual(new Vector2i(1, 8), ship2.EndPosition);
    }

    [TestMethod]
    public void GetAreaRectangleTest()
    {
        var result = ship1.GetAreaRectangle();
        var expected = new Rectangle(new Vector2i(2, 3), new Vector2i(6, 5));
        Assert.AreEqual(expected, result);

        result = ship2.GetAreaRectangle();
        expected = new Rectangle(new Vector2i(0, 4), new Vector2i(2, 9));
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetBodyRectangleTest()
    {
        var expected = new Rectangle(new Vector2i(3, 4), new Vector2i(5, 4));
        var result = ship1.GetBodyRectangle();
        Assert.AreEqual(expected, result);

        expected = new Rectangle(new Vector2i(1, 5), new Vector2i(1, 8));
        result = ship2.GetBodyRectangle();
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetBodyPositionsSet()
    {
        var expected = new HashSet<Vector2i>
        {
            new Vector2i(3, 4),
            new Vector2i(4, 4),
            new Vector2i(5, 4),
        };
        var result = ship1.GetBodyPositionsSet();
        Assert.IsTrue(expected.SetEquals(result));

        expected = new HashSet<Vector2i>
        {
            new Vector2i(1, 5),
            new Vector2i(1, 6),
            new Vector2i(1, 7),
            new Vector2i(1, 8),
        };
        result = ship2.GetBodyPositionsSet();
        Assert.IsTrue(expected.SetEquals(result));
    }

    [TestMethod]
    public void GetAreaOutlinePositionsSet()
    {
        var expected = new HashSet<Vector2i>
        {
            new Vector2i(2, 3),
            new Vector2i(2, 4),
            new Vector2i(2, 5),
            new Vector2i(3, 3),
            new Vector2i(3, 5),
            new Vector2i(4, 3),
            new Vector2i(4, 5),
            new Vector2i(5, 3),
            new Vector2i(5, 5),
            new Vector2i(6, 3),
            new Vector2i(6, 4),
            new Vector2i(6, 5),
        };
        var result = ship1.GetAreaOutlinePositionsSet();
        Assert.IsTrue(expected.SetEquals(result));

        expected = new HashSet<Vector2i>
        {
            new Vector2i(0, 4),
            new Vector2i(0, 5),
            new Vector2i(0, 6),
            new Vector2i(0, 7),
            new Vector2i(0, 8),
            new Vector2i(0, 9),
            new Vector2i(1, 4),
            new Vector2i(1, 9),
            new Vector2i(2, 4),
            new Vector2i(2, 5),
            new Vector2i(2, 6),
            new Vector2i(2, 7),
            new Vector2i(2, 8),
            new Vector2i(2, 9),
        };
        result = ship2.GetAreaOutlinePositionsSet();
        Assert.IsTrue(expected.SetEquals(result));

        expected = new HashSet<Vector2i>
        {
            new Vector2i(5, 8),
            new Vector2i(5, 9),
            new Vector2i(6, 8),
            new Vector2i(7, 8),
            new Vector2i(8, 8),
            new Vector2i(8, 9),
        };
        result = ship3.GetAreaOutlinePositionsSet();
        Assert.IsTrue(expected.SetEquals(result));
    }
}
