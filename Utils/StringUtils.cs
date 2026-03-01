namespace Utils
{
    public class StringUtils
    {
        public static readonly char[] DIGIT_CHARS = [ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' ];
        public static readonly char[] ALPHABET_CHARS = [
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z','A', 'B', 'C', 'D',
            'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T','U', 'V', 'W', 'X',
            'Y', 'Z'
            ];

        public static bool StartsWithOneOfChars(string input, char[] symbols)
        {
            if (input.Length == 0) return false;
            char first = input[0];
            foreach (var symbol in symbols)
            {
                if (symbol.Equals(first)) return true;
            }
            return false;
        }

        public static bool Contains(string input, char[] symbols)
        {
            char[] charOfInput = input.ToCharArray();
            foreach (char c in charOfInput)
            {
                foreach (var symbol in symbols)
                {
                    if (c == symbol) return true;
                }
            }
            return false;
        }

        public static bool ContainsOnly(string input, char[] symbols)
        {
            char[] charOfInput = input.ToCharArray();
            foreach (char c in charOfInput)
            {
                bool isCharAllowed = false;
                foreach (var symbol in symbols)
                {
                    if (c == symbol) 
                    {
                        isCharAllowed = true;
                        break;
                    }
                }
                if (!isCharAllowed) return false;
            }
            return true;
        }

        public static char GetRandomChar(char[] symbols)
        {
            Random random = new Random();
            return symbols[random.Next(symbols.Length)];
        }
    }
}
