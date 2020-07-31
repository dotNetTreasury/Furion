﻿using Fur.AppBasic.Attributes;
using System;

namespace Fur.DatabaseAccessor.Attributes
{
    /// <summary>
    /// 禁用工作单元特性
    /// </summary>
    /// <remarks>
    /// <para>慎用！一旦贴了此特性，单次请求中有任何异常代码，对数据库的任何更改将不会回滚。</para>
    /// <para>支持方法或所在类中贴此特性</para>
    /// <para>注意：只对请求中的起始方法起作用</para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class), NonWrapper]
    public sealed class NonUnitOfWorkAttribute : Attribute { }
}