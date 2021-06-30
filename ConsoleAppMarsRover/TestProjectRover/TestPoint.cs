using System;
using Xunit;
using ConsoleAppMarsRover.Models;
using ConsoleAppMarsRover.BussinesCore;
namespace TestProjectRover
{
    public class TestPoint
    {
        [Fact]
        public void TestCompareEqualPoint()
        {
            int y = 0;
            int x = 0;
            Point first = new Point(y, x);
            Point second = new Point(y, x);          
            Assert.True(first.comparePoint(second));
        }

        [Fact]
        public void TestCompareDifferentPoint()
        {
            int y = 0;
            int x = 0;
            int y2 = 1;
            int x2 = 1;
            Point first = new Point(y, x);
            Point second = new Point(y2, x2);           
            Assert.False(first.comparePoint(second));
        }

        [Fact]
        public void TestCreatePoint()
        {
            int x = 0;
            int y = 5;
            Point first = new Point(y, x);
            Assert.NotNull(first);
            Assert.Equal(first.N, y);
            Assert.Equal(first.E, x);
        }

    }
}
