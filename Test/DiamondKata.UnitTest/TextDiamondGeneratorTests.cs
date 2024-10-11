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
            public void When_Valid_Input_Then_Diamond_Generated_Correctly_v1(char input, string[] expectedOutput)
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


            private static char[] _validTestInputs = [ 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
            'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

            [TestCaseSource(nameof(_validTestInputs))]
            public void When_Valid_Input_Then_Diamond_Should_Contain_Correct_Number_Of_Lines(char input)
            {
                // Arrange

                var generator = new TextDiamondGenerator();
                var expectedLineCount = (input - 'A') * 2 + 1;

                // Act

                var result = generator.GenerateDiamond(input);

                //Assert

                result.IsSuccess.Should().BeTrue();
                result.Value.Should().NotBeEmpty()
                    .And.HaveCount(expectedLineCount);
            }

            [TestCaseSource(nameof(_validTestInputs))]
            public void When_Valid_Input_Then_Diamond_Generated_Correctly_v2(char input)
            {
                // Arrange

                var generator = new TextDiamondGenerator();
                
                // Act

                var result = generator.GenerateDiamond(input);

                //Assert

                result.IsSuccess.Should().BeTrue();

                int lineIndex = 0;
                var lines = result.Value.ToList();
                for(char lineLetter = 'A'; lineLetter < input; lineLetter++)
                {
                    var expected = ExpectedLineForLetter(input, lineLetter);
                    lines[lineIndex++].Should().Be(expected);
                }
                for (char lineLetter = input; lineLetter >= 'A'; lineLetter--)
                {
                    var expected = ExpectedLineForLetter(input, lineLetter);
                    lines[lineIndex++].Should().Be(expected);
                }
            }


            /// <summary>
            /// For a given input letter, what is the expected text output text for the line with the specified letter on it?
            /// </summary>
            /// <remarks>The danger here is that we start coding logic in the tests which duplicates the logic of the subject under test.</remarks>
            /// <param name="inputLetter">Input letter</param>
            /// <param name="lineLetter">Line to return the expected text for</param>
            /// <returns>Expected text</returns>
            private string ExpectedLineForLetter(char inputLetter, char lineLetter)
            {
                if (inputLetter == 'A')
                    return "A";

                var outsideSpaceCount = inputLetter - lineLetter;

                if (lineLetter == 'A')
                {
                    return $"{new string(' ', outsideSpaceCount)}{lineLetter}{new string(' ', outsideSpaceCount)}";
                }
                else
                {
                    var lineAlphabeticPosition = (lineLetter - 'A') + 1;
                    var insideSpaceCount = ((lineAlphabeticPosition - 1) * 2) - 1;

                    return $"{new string(' ', outsideSpaceCount)}{lineLetter}{new string(' ', insideSpaceCount)}{lineLetter}{new string(' ', outsideSpaceCount)}";
                }
            }
        }
    }
}
