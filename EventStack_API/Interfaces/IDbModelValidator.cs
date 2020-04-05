namespace EventStack_API.Interfaces
{
    public interface IDbModelValidator
    {
        bool IsValid(IDbModel dbModel);
    }
}
