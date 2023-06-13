using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
public static class CPlayerPrefs
{
    public static void GenereateNewKey()
    {
        int _intKey = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        int _floatKey = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        long _longKey = (long)UnityEngine.Random.Range(long.MinValue, long.MaxValue);
        byte _byte = (byte)UnityEngine.Random.Range(byte.MinValue, byte.MaxValue);
        ObscuredInt.SetNewCryptoKey(_intKey);
        ObscuredLong.SetNewCryptoKey(_longKey);
        ObscuredFloat.SetNewCryptoKey(_floatKey);
        ObscuredBool.SetNewCryptoKey(_byte);
    }
    #region New PlayerPref Stuff
    /// <summary>
    /// Returns true if key exists in the preferences.
    /// </summary>
    public static bool HasKey(string key)
    {
        return ObscuredPrefs.HasKey(key);
    }

    /// <summary>
    /// Removes key and its corresponding value from the preferences.
    /// </summary>
    public static void DeleteKey(string key)
    {
        ObscuredPrefs.DeleteKey(key);
    }

    /// <summary>
    /// Removes all keys and values from the preferences. Use with caution.
    /// </summary>
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        ObscuredPrefs.DeleteAll();
    }

    /// <summary>
    /// Writes all modified preferences to disk.
    /// </summary>
    #endregion

    #region New PlayerPref Setters
    /// <summary>
    /// Sets the value of the preference identified by key.
    /// </summary>
    public static void SetInt(string key, int val)
    {
        ObscuredPrefs.SetInt(key, val);

    }

    /// <summary>
    /// Sets the value of the preference identified by key.
    /// </summary>
    public static void SetLong(string key, long val)
    {
        ObscuredPrefs.SetLong(key, val);
    }

    /// <summary>
    /// Sets the value of the preference identified by key.
    /// </summary>
    public static void SetString(string key, string val)
    {
        ObscuredPrefs.SetString(key, val);
    }

    /// <summary>
    /// Sets the value of the preference identified by key.
    /// </summary>
    public static void SetFloat(string key, float val)
    {
        ObscuredPrefs.SetFloat(key, val);
    }

    public static void SetBool(string key, bool value)
    {
        ObscuredPrefs.SetBool(key, value);
    }
    #endregion

    #region New PlayerPref Getters
    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// If it doesn't exist, it will return defaultValue.
    /// </summary>
    public static int GetInt(string key, int defaultValue)
    {
        return ObscuredPrefs.GetInt(key, defaultValue);
    }

    public static int GetInt(string key)
    {
        return ObscuredPrefs.GetInt(key, 0);
    }

    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// If it doesn't exist, it will return defaultValue.
    /// </summary>
    public static long GetLong(string key, long defaultValue)
    {
        return ObscuredPrefs.GetLong(key, defaultValue);
    }

    public static long GetLong(string key)
    {
        return GetLong(key, 0);
    }

    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// If it doesn't exist, it will return defaultValue.
    /// </summary>
    public static string GetString(string key, string defaultValue)
    {
        return ObscuredPrefs.GetString(key, defaultValue);
    }

    public static string GetString(string key)
    {
        return GetString(key, "");
    }

    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// If it doesn't exist, it will return defaultValue.
    /// </summary>
    public static float GetFloat(string key, float defaultValue)
    {
        return ObscuredPrefs.GetFloat(key, defaultValue);
    }

    public static float GetFloat(string key)
    {
        return GetFloat(key, 0);
    }

    public static bool GetBool(string key, bool defaultValue = false)
    {
        return ObscuredPrefs.GetBool(key, defaultValue);
    }
    #endregion

    #region Double
    public static void SetDouble(string key, double value)
    {
        ObscuredPrefs.SetDouble(key, value);
    }

    public static double GetDouble(string key, double defaultValue)
    {
        return ObscuredPrefs.GetDouble(key, defaultValue);
    }

    public static double GetDouble(string key)
    {
        return GetDouble(key, 0d);
    }

    private static string DoubleToString(double target)
    {
        return target.ToString("R");
    }

    private static double StringToDouble(string target)
    {
        if (string.IsNullOrEmpty(target))
            return 0d;

        return double.Parse(target);
    }
    #endregion

}
