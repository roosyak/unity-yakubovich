using UnityEditor;
using UnityEngine;

namespace PixselCrew.Model
{
    /*
     промежуточный класс 
    схраняем идентификатор 
     */
    public abstract class PrefsPersistenProperty<TPropertyType> : PersistenProperty<TPropertyType>
    {
        protected string Key;

        protected PrefsPersistenProperty(TPropertyType defaultvalue, string key) : base(defaultvalue)
        {
            Key = key;
        }
    }
}