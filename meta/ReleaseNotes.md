### New in 0.5.8 (Released 2015/02/26)
* Fixed a bug where nullables weren't being set in the code generation model

### New in 0.5.7 (Released 2015/02/26)
* Fixed a bug in the new DataType parsing

### New in 0.5.6 (Released 2015/02/26)
* Fixed DataType parsing

### New in 0.5.4 (Released 2015/02/23)
* Attempt to fix the stupid nuget package

### New in 0.5.1 (Released 2015/02/23)
* Fixed: Enum Model properties are now correctly indicated

### New in 0.5.0 (Released 2015/02/23)
* New: Better support for named enums
* Removed: Model/Enum name normalization

### New in 0.4.0 (Released 2015/02/14)
* New: `DataType.IsEnum`
* New: `DataType.IsModelType`
* New: `DataType.IsPrimitive`
* New: `DataType.IsString`
* New: `IModelDefinition.DefaultValueProperties`
* New: `IModelDefinition.HasDefaultValueProperties`
* New: `IModelDefinition.HasKeyProperty`
* New: `IModelDefinition.HasMaximumValueProperties`
* New: `IModelDefinition.HasMinimumValueProperties`
* New: `IModelDefinition.HasRequiredProperties`
* New: `IModelDefinition.KeyProperty`
* New: `IModelDefinition.MaximumValueProperties`
* New: `IModelDefinition.MinimumValueProperties`
* New: `IModelDefinition.RequiredProperties`
* New: `IModelPropertyDefinition.CanBeObserved`
* Fixed: Operation parameters missing `ParameterType`, `IsRequired`

### New in 0.3.0 (Released 2015/02/14)
* New: `GeneratorOptions.DataTypeMapping` - A dictionary used when processing data types
* New: `GeneratorOptions.OperationFilter` - Called during processing of operations. When `false` is returned for an operation, code will not be generated for that operation.
* New: `GeneratorOptions.OperationParameterFilter` - Called during processing of operation parameters. When `false` is returned, that parameter will be skipped.
* New: `IOperationDefinition.HasBodyParameters` - returns `true` when any parameter is of type `body`
* New: `IOperationDefinition.BodyParameters` - returns all parameters that are of type `body`
* New: `IOperationDefinition.HasFormParameters` - returns `true` when any parameter is of type `form`
* New: `IOperationDefinition.FormParameters` - returns all parameters that are of type `form`
* New: `IOperationDefinition.HasHeaderParameters` - returns `true` when any parameter is of type `header`
* New: `IOperationDefinition.HeaderParameters` - returns all parameters that are of type `header`
* New: `IOperationDefinition.HasPathParameters` - returns `true` when any parameter is of type `path`
* New: `IOperationDefinition.PathParameters` - returns all parameters that are of type `path`
* New: `IOperationDefinition.HasQueryParameters` - returns `true` when any parameter is of type `query`
* New: `IOperationDefinition.QueryParameters` - returns all parameters that are of type `query`
* New: `IOperationDefinition.HasRequiredParameters` - returns `true` when any parameter is required
* New: `IOperationDefinition.RequiredParameters` - returns all parameters that are required

### New in 0.2.1 (Released 2015/02/13)
* Fixed: Calling `SwaggerJsonLoader` with an resource relative path treats it as an absolute path

### New in 0.2.0 (Released 2015/02/13)
* New: `SwaseyEnumWriter(string name, string content, IEnumDefinition definition)`
* New: `SwaseyModelWriter(string name, string content, IModelDefinition definition)`
* New: `SwaseyOperationWriter(string name, string content, IOperationDefinition definition)`
* New: `IEnumDefinition` and `IModelDefinition` now have a `ResourceName` property
* New: `LCFirst` inline Handlebars helper - `{{LCFirst ResourceName}}`
* Fixed: `TargetFramework` has been changed from `4.5.1` to `4.5`

**Breaking Changes:**
* `SwaseyWriter` is now `SwaseyOperationWriter`

### New in 0.1.0 (Released 2015/02/07)
* Initial release
