namespace RJRentalOfHousing.Domain
{
    public class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException(object entity, string? message)
            : base($"实体{entity.GetType().Name}状态改变时发生错误,{message}")
        {
        }
    }
}
