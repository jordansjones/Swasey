namespace Swasey.Model
{
    public interface IResponseDefinition : IModelMetadata
    {

        IOperationDefinition Context { get; }

        DataType DataType { get; }
    }
}