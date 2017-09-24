using UnityEngine;
using System.Collections;
using System;

namespace GAMO.Hesman.Utilities {
    public static class MathExtension {
        private const float imprecision = 0.001f;
        private const float zero = 0f;

        #region FLOAT
        public static bool equal(this float value, float comparator) {
            return Mathf.Approximately(value, comparator);
        }
        public static bool greaterThan(this float value, float comparator) {
            return (value - comparator > imprecision);
        }
        public static bool lessThan(this float value, float comparator) {
            return (value - comparator < -imprecision);
        }
        public static bool greaterThanOrEqual(this float value, float comparator) {
            return value.greaterThan(comparator) || value.equal(comparator);
        }
        public static bool lessThanOrEqual(this float value, float comparator) {
            return value.lessThan(comparator) || value.equal(comparator);
        }
        public static float floorInRange(this float value) {
            return Mathf.Floor(value * 10 + imprecision) / 10;
        }
        /// <summary>
        /// Lazy function to normalize a float
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>value as normalized</returns>
        public static float normalize(this float value, float min, float max) {
            return Mathf.InverseLerp(min, max, value);
        }
        public static float floor(this float value) {
            return Mathf.Floor(value);
        }
        public static int floorToInt(this float value) {
            return Mathf.FloorToInt(value);
        }
        #endregion
    }
}
