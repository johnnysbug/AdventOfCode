namespace tests;

public class UnitTest1
{
    [Theory]
    [InlineData(79, 19, 23)]
    [InlineData(79, 1, 23)]
    [InlineData(1, 19, 23)]
    [InlineData(79, 79, 13)]
    [InlineData(79, 1, 13)]
    public void Test1(decimal left, decimal right, int test)
    {
        var result = left * right % test == 0;
        var reduced = left / 3 * right / 3 % (test / 3) == 0;

        Assert.Equal(reduced, result);
    }

}
