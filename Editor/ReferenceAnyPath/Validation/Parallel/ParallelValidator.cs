#if !REFERENCE_ANY_PATH_NO_PARALLEL_CHECK_IN_INSPECTOR
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;

#if REFERENCE_ANY_PATH_FORCE_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if UNITY_2023_1_OR_NEWER
using UnityEngine;
#else
using System.Threading.Tasks;
#endif

namespace ReferenceAnyPath {
    public static class ParallelValidator {
        class Validation {
            public bool IsRunning = true;
            public bool? IsValid;
        }

        static readonly Dictionary<string, Validation> _validations = new();
        static readonly CancellationTokenSource _cancellationTokenSource = new();

        static ParallelValidator() => EditorApplication.quitting += () => _cancellationTokenSource.Cancel();

        public static bool? Validate(SerializedProperty property, string[] extensions,
            string absolutePath, Func<string[], string, bool> validate) {
            var propertyId = property.GetPropertyId();
            if (_validations.TryGetValue(propertyId, out var validation)) {
                if (validation.IsRunning)
                    return validation.IsValid;

                validation.IsRunning = true;
            }
            else {
                validation = new Validation();
                _validations[propertyId] = validation;
            }

            ValidateAsync(propertyId, extensions, absolutePath, validate, _cancellationTokenSource.Token);
            return validation.IsValid;
        }

        static async void ValidateAsync(string propertyId, string[] extensions,
            string absolutePath, Func<string[], string, bool> validate, CancellationToken cancellationToken) {
#if REFERENCE_ANY_PATH_FORCE_UNITASK
            try {
                cancellationToken.ThrowIfCancellationRequested();
                await UniTask.SwitchToThreadPool();
                cancellationToken.ThrowIfCancellationRequested();
                var isValid = validate.Invoke(extensions, absolutePath);
                cancellationToken.ThrowIfCancellationRequested();
                await UniTask.SwitchToMainThread();
                HandleSuccess(isValid, propertyId);
            }
            catch(OperationCanceledException) { }
            catch {
                await UniTask.SwitchToMainThread();
                HandleException(propertyId);
            }
#elif UNITY_2023_1_OR_NEWER
            try {
                cancellationToken.ThrowIfCancellationRequested();
                await Awaitable.BackgroundThreadAsync();
                cancellationToken.ThrowIfCancellationRequested();
                var isValid = validate.Invoke(extensions, absolutePath);
                cancellationToken.ThrowIfCancellationRequested();
                await Awaitable.MainThreadAsync();
                HandleSuccess(isValid, propertyId);
            }
            catch(OperationCanceledException) { }
            catch {
                await Awaitable.MainThreadAsync();
                HandleException(propertyId);
            }
#else
            var startingThreadContext = SynchronizationContext.Current;
            try {
                cancellationToken.ThrowIfCancellationRequested();
                var isValid = await Task.Run(() => {
                    cancellationToken.ThrowIfCancellationRequested();
                    return validate.Invoke(extensions, absolutePath);
                }, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
                HandleSuccess(isValid, propertyId);
            }
            catch(OperationCanceledException) { }
            catch {
                startingThreadContext.Post(_ => HandleException(propertyId), null);
            }
#endif
        }

        public static bool? Validate(SerializedProperty property, string[] extensions, string absolutePath,
            int width, int height, int bits, Func<string[], string, int, int, int, bool> validate) {
            var propertyId = property.GetPropertyId();
            if (_validations.TryGetValue(propertyId, out var validation)) {
                if (validation.IsRunning)
                    return validation.IsValid;

                validation.IsRunning = true;
            }
            else {
                validation = new Validation();
                _validations[propertyId] = validation;
            }

            ValidateAsync(propertyId, extensions, absolutePath, width, height, bits, validate, _cancellationTokenSource.Token);
            return validation.IsValid;
        }

        static async void ValidateAsync(string propertyId, string[] extensions, string absolutePath,
            int width, int height, int bits, Func<string[], string, int, int, int, bool> validate,
            CancellationToken cancellationToken) {
#if REFERENCE_ANY_PATH_FORCE_UNITASK
            try {
                cancellationToken.ThrowIfCancellationRequested();
                await UniTask.SwitchToThreadPool();
                cancellationToken.ThrowIfCancellationRequested();
                var isValid = validate.Invoke(extensions, absolutePath, width, height, bits);
                cancellationToken.ThrowIfCancellationRequested();
                await UniTask.SwitchToMainThread();
                HandleSuccess(isValid, propertyId);
            }
            catch(OperationCanceledException) { }
            catch {
                await UniTask.SwitchToMainThread();
                HandleException(propertyId);
            }
#elif UNITY_2023_1_OR_NEWER
            try {
                cancellationToken.ThrowIfCancellationRequested();
                await Awaitable.BackgroundThreadAsync();
                cancellationToken.ThrowIfCancellationRequested();
                var isValid = validate.Invoke(extensions, absolutePath, width, height, bits);
                cancellationToken.ThrowIfCancellationRequested();
                await Awaitable.MainThreadAsync();
                HandleSuccess(isValid, propertyId);
            }
            catch(OperationCanceledException) { }
            catch {
                await Awaitable.MainThreadAsync();
                HandleException(propertyId);
            }
#else
            var startingThreadContext = SynchronizationContext.Current;
            try {
                cancellationToken.ThrowIfCancellationRequested();
                var isValid = await Task.Run(() => {
                    cancellationToken.ThrowIfCancellationRequested();
                    return validate.Invoke(extensions, absolutePath, width, height, bits);
                }, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
                HandleSuccess(isValid, propertyId);
            }
            catch(OperationCanceledException) { }
            catch {
                startingThreadContext.Post(_ => HandleException(propertyId), null);
            }
#endif
        }

        static void HandleSuccess(bool isValid, string propertyId) {
            var foundValidation = _validations[propertyId];
            var wasValid = foundValidation.IsValid;
            foundValidation.IsValid = isValid;
            foundValidation.IsRunning = false;
            if (wasValid == isValid)
                return;

            EditorWindowsCache.Repaint();
        }

        static void HandleException(string propertyId) {
            if (_validations == null ||
                propertyId == null ||
                !_validations.TryGetValue(propertyId, out var foundValidation) ||
                foundValidation == null) {
                return;
            }

            foundValidation.IsRunning = false;
        }
    }
}
#endif