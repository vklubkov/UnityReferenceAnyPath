#if !REFERENCE_ANY_PATH_NO_PARALLEL_CHECK_IN_INSPECTOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ReferenceAnyPath {
    public static class EditorWindowsCache {
        readonly static List<EditorWindow> _editorWindows = new();
        static double _nextUpdateTime;
        
        public static void Repaint() {
            if (EditorApplication.timeSinceStartup >= _nextUpdateTime) {
                _nextUpdateTime = EditorApplication.timeSinceStartup + 1.0;
                UpdateEditorWindows();
            }

            foreach (var inspector in _editorWindows) {
                if (inspector == null)
                    continue;

                inspector.Repaint();
            }
        }

        static void UpdateEditorWindows() {
            _editorWindows.Clear();
            var editorWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
            foreach (var editorWindow in editorWindows) {
                var windowName = editorWindow.GetType().Name;

                switch (windowName) {
                    // Exclude most common windows where custom properties can't be drawn
                    case "InspectorDebugWindow": // Doesn't use custom property drawers
                    case "SceneView":
                    case "GameView":
                    case "HierarchyWindow":
                    case "ProjectBrowser":
                    case "ConsoleWindow":
                    case "AnimationWindow":
                    case "ProfilerWindow":
                    case "AssetStoreWindow":
                    case "PackageManagerWindow":
                    case "LightingWindow":
                    case "SpritePackerWindow":
                    case "AudioMixerWindow":
                    case "PhysicsDebugWindow":
                    case "AnimatorControllerTool":
                    case "VersionControlWindow":
                    case "TimelineWindow":
                    case "ShaderGraphWindow":
                    case "TilePaletteWindow":
                    case "TestRunnerWindow":
                    case "ProfilerTimelineView":
                    case "PresetManagerWindow":
                    case "LogEntries":
                    case "MemoryProfilerWindow":
                    case "VersionControlSettingsInspector":
                    case "BuilderWindow":
                    case "CollabHistoryWindow":
                    case "ShaderInspector":
                    case "AssetBundleBrowserMain":
                    case "LookDevView":
                        continue;

                    // Explicitly don't exclude:
                    case "InspectorWindow":
                    case "PreferencesWindow":
                    case "ProjectSettingsWindow":
                        break;
                }

                _editorWindows.Add(editorWindow);
            }
        }
    }
}
#endif