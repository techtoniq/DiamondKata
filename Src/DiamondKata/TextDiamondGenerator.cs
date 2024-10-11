using FluentResults;

namespace DiamondKata
{
    public class TextDiamondGenerator
    {
        public Result<IEnumerable<string>> GenerateDiamond(char forLetter)
        {
            if(forLetter < 'A' ||  forLetter > 'Z')
            {
                return Result.Fail("Letter must be between A and Z (inclusive).");
            }

            var textLines = new List<string>();

            var alphabeticPosition = (forLetter - 'A') + 1;
            var outsideSpaceCount = alphabeticPosition - 1;

            for (int i = 0; i < alphabeticPosition; i++)
            {
                var outsideSpace = new string(' ', outsideSpaceCount);
                var diamondText = "A";
                if(i > 0)
                {
                    var insideSpaceCount = (i * 2) - 1;
                    var insideSpace = new string(' ', insideSpaceCount);
                    diamondText = $"{char.ConvertFromUtf32('A' + i)}{insideSpace}{char.ConvertFromUtf32('A' + i)}";
                }
                textLines.Add($"{outsideSpace}{diamondText}{outsideSpace}");
                outsideSpaceCount--;
            }
            var bottomLines = textLines[0..(alphabeticPosition-1)];
            if(bottomLines.Count > 0)
            {
                bottomLines.Reverse();
                textLines.AddRange(bottomLines);
            }
            
            return textLines;
        }
    }
}
