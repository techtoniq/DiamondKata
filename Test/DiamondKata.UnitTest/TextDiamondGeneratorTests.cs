using FluentAssertions;

namespace DiamondKata.UnitTest
{
    internal class TextDiamondGeneratorTests
    {
        public class InputValidation
        {
            [TestCase((char)('A' - 1), false)]
            [TestCase('A', true)]
            [TestCase((char)('A' + 1), true)]
            [TestCase('M', true)]
            [TestCase((char)('Z' - 1), true)]
            [TestCase('Z', true)]
            [TestCase((char)('Z' + 1), false)]
            [TestCase('a', false)]
            [TestCase('m', false)]
            [TestCase('z', false)]
            [TestCase(' ', false)]
            [TestCase('5', false)]
            [TestCase('?', false)]
            public void When_Input_Supplied_Then_Input_Must_Be_Valid(char input, bool isValidInputResult)
            {
                // Arrange

                var generator = new TextDiamondGenerator();

                // Act

                var result = generator.GenerateDiamond(input);

                //Assert

                result.IsSuccess.Should().Be(isValidInputResult);
            }
        }

        public class Generate
        {
            [TestCase('A', new string[] { "A" })]
            [TestCase('B', new string[] { " A ", 
                                          "B B", 
                                          " A " })]
            [TestCase('C', new string[] { "  A  ", 
                                          " B B ", 
                                          "C   C", 
                                          " B B ", 
                                          "  A  " })]
            public void When_Valid_Input_Then_Diamond_Generated_Correctly(char input, string[] expectedOutput)
            {
                // Arrange

                var generator = new TextDiamondGenerator();

                // Act

                var result = generator.GenerateDiamond(input);

                //Assert

                result.IsSuccess.Should().BeTrue();
                result.Value.Should().NotBeEmpty()
                    .And.HaveCount(expectedOutput.Length)
                    .And.ContainInOrder(expectedOutput);
            }
        }
    }
}
