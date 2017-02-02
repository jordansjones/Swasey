namespace Swasey.Model
{
    public interface IResponseDefinition : IServiceMetadata
    {

        //IOperationDefinition Context { get; }
        IOperationDefinitionParent Context { get; }

        DataType DataType { get; }
    }
}