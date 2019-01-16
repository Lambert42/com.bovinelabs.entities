﻿// <copyright file="NativeHashMapImposter.cs" company="Timothy Raines">
//     Copyright (c) Timothy Raines. All rights reserved.
// </copyright>

namespace BovineLabs.Entities.Helpers
{
    using System;
    using System.Runtime.InteropServices;
    using Unity.Collections;
    using Unity.Collections.LowLevel.Unsafe;

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct NativeHashMapImposter<TKey, TValue>
        where TKey : struct, IEquatable<TKey>
        where TValue : struct
    {
        [NativeDisableUnsafePtrRestriction]
        internal NativeHashMapDataImposter* Buffer;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        private AtomicSafetyHandle Safety;

        [NativeSetClassTypeToNullOnSchedule]
        private DisposeSentinel DisposeSentinel;
#endif

        private Allocator AllocatorLabel;

        public static implicit operator NativeHashMapImposter<TKey, TValue>(NativeHashMap<TKey, TValue> hashMap)
        {
            var ptr = UnsafeUtility.AddressOf(ref hashMap);
            UnsafeUtility.CopyPtrToStructure(ptr, out NativeHashMapImposter<TKey, TValue> imposter);
            return imposter;
        }
    }
}