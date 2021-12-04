// -----------------------------------------------------------------------
// <copyright file="Coroutines.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace MelonLoader.Support
{
#pragma warning disable SA1202 // Elements should be ordered by access
#pragma warning disable SA1600 // Elements should be documented

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Scope.Client.API.Features;
    using UnhollowerBaseLib;
    using UnityEngine;

    internal static class Coroutines
    {
        private static readonly List<CoroTuple> OurCoroutinesStore = new();
        private static readonly List<IEnumerator> OurNextFrameCoroutines = new();
        private static readonly List<IEnumerator> OurWaitForFixedUpdateCoroutines = new();
        private static readonly List<IEnumerator> OurWaitForEndOfFrameCoroutines = new();
        private static readonly List<IEnumerator> TempList = new();
        private static readonly GetDeltaTimeDelegate GetDeltaTimeDelegateField;

        static Coroutines() => GetDeltaTimeDelegateField = IL2CPP.ResolveICall<GetDeltaTimeDelegate>("UnityEngine.Time::get_deltaTime");

        private delegate float GetDeltaTimeDelegate();

        internal static object Start(IEnumerator routine)
        {
            if (routine != null)
                ProcessNextOfCoroutine(routine);
            return routine;
        }

        internal static void Stop(IEnumerator enumerator)
        {
            try
            {
                if (OurNextFrameCoroutines.Contains(enumerator))
                    OurNextFrameCoroutines.Remove(enumerator);
                else
                {
                    int coroTupleIndex = OurCoroutinesStore.FindIndex(c => c.Coroutine == enumerator);
                    if (coroTupleIndex != -1)
                    {
                        object waitCondition = OurCoroutinesStore[coroTupleIndex].WaitCondition;
                        if (waitCondition is IEnumerator waitEnumerator)
                            Stop(waitEnumerator);

                        OurCoroutinesStore.RemoveAt(coroTupleIndex);
                    }
                }
            }
            catch (Exception)
            {
                Log.Info("Ignoring exception thrown while stopping a coroutine");
            }
        }

        private static void ProcessCoroList(List<IEnumerator> target)
        {
            if (target.Count == 0)
                return;

            TempList.AddRange(target);
            target.Clear();
            foreach (IEnumerator enumerator in TempList)
                ProcessNextOfCoroutine(enumerator);
            TempList.Clear();
        }

        internal static void Process()
        {
            for (int i = OurCoroutinesStore.Count - 1; i >= 0; i--)
            {
                CoroTuple tuple = OurCoroutinesStore[i];
                if (tuple.WaitCondition is WaitForSeconds waitForSeconds)
                {
                    if ((waitForSeconds.m_Seconds -= GetDeltaTimeDelegateField()) <= 0)
                    {
                        OurCoroutinesStore.RemoveAt(i);
                        ProcessNextOfCoroutine(tuple.Coroutine);
                    }
                }
            }

            ProcessCoroList(OurNextFrameCoroutines);
        }

        internal static void ProcessWaitForFixedUpdate() => ProcessCoroList(OurWaitForFixedUpdateCoroutines);

        internal static void ProcessWaitForEndOfFrame() => ProcessCoroList(OurWaitForEndOfFrameCoroutines);

        private static void ProcessNextOfCoroutine(IEnumerator enumerator)
        {
            try
            {
                if (!enumerator.MoveNext())
                {
                    List<int> indices = OurCoroutinesStore.Select((it, idx) => (idx, it)).Where(it => it.it.WaitCondition == enumerator).Select(it => it.idx).ToList();
                    for (int i = indices.Count - 1; i >= 0; i--)
                    {
                        int index = indices[i];
                        OurNextFrameCoroutines.Add(OurCoroutinesStore[index].Coroutine);
                        OurCoroutinesStore.RemoveAt(index);
                    }

                    return;
                }
            }
            catch (Exception e)
            {
                Log.Error($"Exception in coroutine of type {enumerator?.GetType().AssemblyQualifiedName}: {e}");
                Stop(FindOriginalCoro(enumerator));
                return;
            }

            object next = enumerator.Current;
            switch (next)
            {
                case null:
                    OurNextFrameCoroutines.Add(enumerator);
                    return;
                case WaitForFixedUpdate _:
                    OurWaitForFixedUpdateCoroutines.Add(enumerator);
                    return;
                case WaitForEndOfFrame _:
                    OurWaitForEndOfFrameCoroutines.Add(enumerator);
                    return;
                case WaitForSeconds _:
                    break;
                case Il2CppObjectBase il2CppObjectBase:
                    Il2CppSystem.Collections.IEnumerator nextAsEnumerator = il2CppObjectBase.TryCast<Il2CppSystem.Collections.IEnumerator>();
                    if (nextAsEnumerator != null)
                        next = new Il2CppEnumeratorWrapper(nextAsEnumerator);
                    else
                        Log.Error($"Unknown coroutine yield object of type {il2CppObjectBase} for coroutine {enumerator}");
                    break;
            }

            OurCoroutinesStore.Add(new CoroTuple { WaitCondition = next, Coroutine = enumerator });

            if (next is IEnumerator nextCoro)
                ProcessNextOfCoroutine(nextCoro);
        }

        private static IEnumerator FindOriginalCoro(IEnumerator enumerator)
        {
            int index = OurCoroutinesStore.FindIndex(ct => ct.WaitCondition == enumerator);
            if (index == -1)
                return enumerator;
            return FindOriginalCoro(OurCoroutinesStore[index].Coroutine);
        }

        private struct CoroTuple
        {
            public object WaitCondition;
            public IEnumerator Coroutine;
        }

        private class Il2CppEnumeratorWrapper : IEnumerator
        {
            private readonly Il2CppSystem.Collections.IEnumerator _il2cppEnumerator;

            public Il2CppEnumeratorWrapper(Il2CppSystem.Collections.IEnumerator il2CppEnumerator) => _il2cppEnumerator = il2CppEnumerator;

            public object Current => _il2cppEnumerator.Current;

            public bool MoveNext() => _il2cppEnumerator.MoveNext();

            public void Reset() => _il2cppEnumerator.Reset();
        }
    }
}