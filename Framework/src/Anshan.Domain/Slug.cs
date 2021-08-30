using System;
using System.Linq;
using System.Text.RegularExpressions;
using Anshan.Domain.Exceptions;

#nullable enable

namespace Anshan.Domain
{
    public class Slug : ValueObject
    {
        private static readonly string[] InvalidWords =
        {
            "the", "at", "there", "some", "my", "of", "be", "use", "her", "than", "and", "this", "an", "would", "a",
            "have", "each", "to", "from", "been", "in", "or", "she", "him", "is", "do", "into", "you", "had", "how",
            "that", "by", "their", "has", "its", "it", "if", "he", "but", "will", "was", "up", "for", "what", "on",
            "about", "are", "were", "out", "did", "as", "we", "many", "get", "with", "when", "then", "his", "your",
            "them", "they", "can", "these", "could", "may", "I", "said", "so"
        };

        private readonly string _value;

        private Slug(string value)
        {
            if (!Regex.IsMatch(value, "^[a-zA-Z0-9-]*$")) throw new SlugIsNotInEnglishException();

            var slugWords = value.Split("-");

            foreach (var invalidWord in InvalidWords)
                if (slugWords.Any(w => w.Equals(invalidWord, StringComparison.OrdinalIgnoreCase)))
                    throw new SlugContainInvalidWordsException(invalidWord);

            _value = value;
        }

        public static implicit operator string(Slug slug)
        {
            return slug._value;
        }

        public static implicit operator Slug(string stringValue)
        {
            return new(stringValue);
        }

        public override bool Equals(object obj)
        {
            return Equals(_value, obj);
        }

        public override string ToString()
        {
            return _value;
        }
    }
}