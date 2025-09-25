## Compatibility

**NStandard** 移植了 **.NET Core** 的部分功能，以便在 **.NET Framework** 中使用。

<br/>

| 命名空间           | Class / Strcut                                          | 兼容性                                                       |
| ------------------ | ------------------------------------------------------- | ------------------------------------------------------------ |
| System             | **HashCode**                                            | = .NET Framework<br />< .NET Standard 2.1<br />< .NET Core App 2.1 |
| System             | **ITuple**                                              | < .NET Framework 4.7<br />< .NET Standard 2.1<br />< .NET Core App 2.0 |
| System             | **Tuple**                                               | < .NET Framework 4.5.1                                       |
| System             | **FormattableString**                                   | < .NET Framework 4.6<br />< .NET Standard 1.3                |
| System.Collections | **IStructuralComparable**<br />**IStructuralEquatable** | < .NET Framework 4.5.1                                       |
| System.Reflection  | **CustomAttributeExtensions**                           | < .NET Framework 4.5.1                                       |

