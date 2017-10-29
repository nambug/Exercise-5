﻿using UnityEngine;

namespace Code
{
    /// <summary>
    /// Represents a what platform (e.g. OS) we're running on
    /// </summary>
    public enum PlatformType
    {
        Windows,
        Mac,
        Linux,
    }

    /// <summary>
    /// Utilities for determining what platform (e.g. Mac vs Windows) we're running on.
    /// Determining the controller "axis" bindings for the particular platform we're on.
    /// This lets the rest of the game ignore whether we're running on Max or Windows.
    /// </summary>
    public static class Platform
    {
        /// <summary>
        /// Determine what platform we're presently running on.
        /// </summary>
        /// <returns>What platform we're running on</returns>
        public static PlatformType GetPlatform () {
            if ((Application.platform == RuntimePlatform.WindowsPlayer) || 
                (Application.platform == RuntimePlatform.WindowsEditor))
            {
                return PlatformType.Windows;
            }
            if ((Application.platform == RuntimePlatform.OSXEditor) || 
                (Application.platform == RuntimePlatform.OSXPlayer))
            {
                return PlatformType.Mac;
            }
            if ((Application.platform == RuntimePlatform.LinuxEditor) || 
                (Application.platform == RuntimePlatform.LinuxPlayer))
            {
                return PlatformType.Linux;
            }
            return PlatformType.Windows;
        }

        /// <summary>
        /// Returns the name of the platform appropriate input axis for firing.
        /// Windows has a different binding for the right trigger than OSX/Linux.
        /// </summary>
        /// <returns>Name of the "fire" axis</returns>
        public static string GetFireAxis() {
            
            return GetPlatform() == PlatformType.Windows ? "FireAxis1(Win)" : "FireAxis1(Mac)"; // OSX/Linux bind right trigger the same way
        }
    }

}