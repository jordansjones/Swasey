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
