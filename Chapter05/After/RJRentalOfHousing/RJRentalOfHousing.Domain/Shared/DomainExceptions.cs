namespace RJRentalOfHousing.Domain.Shared
{
    public static class DomainExceptions
    {
        public class InvalidEntityStateException : Exception
        {
            public InvalidEntityStateException(object entity, string? message) : base($"实体{entity.GetType().Name}状态改变时发生错误,{message}") { }
        }
        public class ProfanityFound : Exception
        {
            public ProfanityFound(string text) : base($"文本中含有敏感词汇:{text}") { }
        }
    }
}
