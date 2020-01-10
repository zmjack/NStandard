# NStandard

DotNet Core 标准库扩展。



## Zipper (v0.1.1)

- **NET35**
  不带选择器方法最多支持 **7** 个参数，返回类型 **Tuple**（迁移实现）；
  带选择器方法最多支持 **4** 个参数（**Func** 最多只支持 4 个参数）。
- **NET40**
  不带选择器方法最多支持 **7** 个参数，返回类型 **Tuple**（标准库实现）；
  带选择器方法最多支持 **7** 个参数（**Tuple** 最多只支持 7 个参数）。
- **NETSTANDARD2_0**
  不带选择器方法最多支持 **16** 个参数，返回类型 **ValueTuple**（标准库实现）；
  带选择器方法最多支持 **16** 个参数。
