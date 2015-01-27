namespace Swasey.Model
{
    public interface IResponseDefinition : IServiceMetadata
    {

        IOperationDefinition Context { get; }

        DataType DataType { get; }
    }
}