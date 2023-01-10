using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PixselCrew.Model
{
    [Serializable]
    public class FloatPersistenProperty : PrefsPersistenProperty<float>
    {
        public FloatPersistenProperty(float defaultValue, string key) : base(defaultValue, key)
        {
            Init();
        }
        protected override float Read(float defaultValue)
        {
            return PlayerPrefs.GetFloat(Key, defaultValue);
        }

        protected override void Write(float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

   
    }
}
