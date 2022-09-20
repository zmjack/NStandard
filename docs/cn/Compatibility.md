# NStandard

**.NET** extension library for system library.

<br/>

## Compatibility

**NStandard** ports parts of **.NET Core** for use within the **.NET Framework**.

<br/>

| Namespace          | Class or Strcut                                 | Compatibility        |
| ------------------ | ----------------------------------------------- | -------------------- |
| System             | HashCode                                        | = .NET Framework     |
| System             | Tuple                                           | < .NET Framework 4.0 |
| System             | FormattableString                               | < .NET Framework 4.6 |
| System.Collections | IStructuralComparable<br />IStructuralEquatable | < .NET Framework 4.0 |
| System.Reflection  | CustomAttributeExtensions                       | < .NET Framework 4.5 |

