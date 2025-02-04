namespace CandyShop.UnitTests
{
    public class ValiationTests
    {
        [Fact]
        public void WhenStringIsValidReturnTrue()
        {
            var stringInput = "Test Chocolate Bar";
            var result = Validation.IsStringValid(stringInput);

            Assert.True(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("string with more than 20 characters")]
        public void WhenStringIsNotValidReturnFalse(string testString)
        {
            var result = Validation.IsStringValid(testString);
            Assert.False(result);
        }

        [Theory]
        [InlineData("20")]
        [InlineData("20.5")]
        public void WhenPriceIsValidReturnTrue(string testPrice)
        {
            var result = Validation.IsPriceValid(testPrice);
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("-1")]
        [InlineData("10000")]
        [InlineData("123456789999999")]
        public void WhenPriceIsNotValidReturnFalse(string testPrice)
        {
            var result = Validation.IsPriceValid(testPrice);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("-1")]
        [InlineData("100")]
        [InlineData("123456789999999")]
        [InlineData("20.5")]

        public void WhenCocoaIsNotValidReturnFalse(string testCocoa)
        {
            var result = Validation.IsCocoaValid(testCocoa);
            Assert.False(result.IsValid);
        }

    }
}