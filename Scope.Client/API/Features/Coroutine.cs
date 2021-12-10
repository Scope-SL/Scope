// -----------------------------------------------------------------------
// <copyright file="Coroutine.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features
{
    using System;
    using System.Collections;
    using MelonLoader.Support;
    using UnityEngine;

    /// <summary>
    /// A set of tools to easily interact with coroutines.
    /// </summary>
    public class Coroutine
    {
        private readonly object _handle;

        private Coroutine(object coroutine) => _handle = coroutine;

        /// <summary>
        /// Starts the <see cref="Coroutine"/> execution.
        /// </summary>
        /// <param name="enumerator">The <see cref="IEnumerator"/> to execute.</param>
        /// <returns>The executed <see cref="Coroutine"/>.</returns>
        public static Coroutine Start(IEnumerator enumerator) => new(Coroutines.Start(enumerator));

        /// <summary>
        /// Delays the <see cref="Action"/> for the given amount of seconds.
        /// </summary>
        /// <param name="delay">The specified delay.</param>
        /// <param name="action">The specified <see cref="Action"/>.</param>
        /// <returns>The delayed <see cref="Coroutine"/> execution.</returns>
        public static Coroutine CallDelayed(float delay, Action action) => new(Start(Delay(action, delay)));

        /// <summary>
        /// Delays the <see cref="Action"/> for the specified <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="timeSpan">The specified delay.</param>
        /// <param name="action">The specified <see cref="Action"/>.</param>
        /// <returns>The delayed <see cref="Coroutine"/> execution.</returns>
        public static Coroutine CallDelayed(TimeSpan timeSpan, Action action) => Start(Delay(action, timeSpan));

        /// <summary>
        /// Kills the <see cref="Coroutine"/> execution.
        /// </summary>
        /// <param name="coroutine">The <see cref="Coroutine"/> to kill.</param>
        public static void Stop(Coroutine coroutine) => Coroutines.Stop((IEnumerator)coroutine._handle);

        /// <summary>
        /// Suspends the <see cref="Coroutine"/> execution for the given amount of seconds using scaled time.
        /// <para>The real time suspended is equal to the given time divided by <see cref="Time.timeScale"/>.</para>
        /// <para>See <see cref="WaitForSecondsRealtime(float)(float)"/> to use unscaled time.</para>
        /// </summary>
        /// <param name="seconds">The scaled time amount.</param>
        /// <returns>A <see cref="UnityEngine.WaitForSeconds"/> object.</returns>
        public static WaitForSeconds WaitForSeconds(float seconds) => new(seconds);

        /// <summary>
        /// Suspends the <see cref="Coroutine"/> execution for the given amount of seconds using unscaled time.
        /// <para>See <see cref="WaitForSeconds(float)"/> to use scaled time.</para>
        /// </summary>
        /// <param name="seconds">The unscaled time amount.</param>
        /// <returns>A <see cref="UnityEngine.WaitForSecondsRealtime"/> object.</returns>
        public static WaitForSecondsRealtime WaitForSecondsRealtime(float seconds) => new(seconds);

        private static IEnumerator Delay(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke();
        }

        private static IEnumerator Delay(Action action, TimeSpan timeSpan)
        {
            yield return new WaitForSeconds(Convert.ToSingle(timeSpan.TotalSeconds));
            action.Invoke();
        }
    }
}
