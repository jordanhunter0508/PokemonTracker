# Update Accessor Classes

This plan addresses the cleanup of Data Accessor classes in the `DataAccess` project.

## User Review Required

> [!WARNING]
> Replacing `SqlParameterCollection.Add(name, sqlDbType, size)` with `AddWithValue(name, value)` will remove explicit types and sizes, meaning ADO.NET will infer them from the value's type. This is generally safe but can occasionally cause performance issues with query plan caching for `string` types (due to `NVarChar` lengths). Please let me know if you would prefer to retain specific length mappings.

Keep explicit types and sizes for strings. Then place them seperate from the AddWithValue(name,value) stack. This is to ensure that the string types are not inferred with the wrong size.

## Proposed Changes

### DataAccess Project

We will apply the following refactoring to all 12 Accessor classes:
1. Ensure `using System.Data;` is at the top of the file.
2. Remove any fully qualified calls like `System.Data.CommandType` and `System.Data.SqlDbType`, replacing them with `CommandType` and `SqlDbType`.
3. Locate all instances where parameters are added via `cmd.Parameters.Add()` and then later initialized via `cmd.Parameters["@Name"].Value = value;`. These will be condensed to the more concise `cmd.Parameters.AddWithValue("@Name", value);`.

#### [MODIFY] AbilityAccessor.cs
#### [MODIFY] AltArtAccessor.cs
#### [MODIFY] ArtistAccesor.cs
#### [MODIFY] BoosterAccsesor.cs
#### [MODIFY] CardAccessor.cs
#### [MODIFY] CardComponentAccessor.cs
#### [MODIFY] CollectionAccessor.cs
#### [MODIFY] ElementAccessor.cs
#### [MODIFY] MoveAccessor.cs
#### [MODIFY] RuleAccessor.cs
#### [MODIFY] SearchAccessor.cs
#### [MODIFY] UserAccessor.cs

## Open Questions

- Should we handle the `Add` calls that don't receive values (for example, output parameters), or keep them as is? (Assuming we ignore them since `AddWithValue` cannot be used without a value).
    Leave output parameters as is.

## Verification Plan

### Automated Tests
- Run `dotnet build` to verify there are no namespace conflicts or syntax errors.

### Manual Verification
- Review the modified files to ensure all usages of `System.Data.SqlDbType` and `System.Data.CommandType` have been correctly shortened and simplified.
