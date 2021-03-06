﻿// -----------------------------------------------------------------------
//  <copyright file="ArrayExtensions.cs" company="SF开源团队">
//      Copyright (c) 2014-2017 SF. All rights reserved.
//  </copyright>
//  <site>http://www.SF.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-08-04 0:46</last-date>
// -----------------------------------------------------------------------

using System;


namespace SF.Utility.Extensions
{
    /// <summary>
    /// 数组扩展方法
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 复制一份新的二维灰度数组
        /// </summary>
        public static byte[,] Copy(this byte[,] bytes)
        {
            int width = bytes.GetLength(0), height = bytes.GetLength(1);
            byte[,] newBytes = new byte[width, height];
            Array.Copy(bytes, newBytes, bytes.Length);
            return newBytes;
        }
    }
}