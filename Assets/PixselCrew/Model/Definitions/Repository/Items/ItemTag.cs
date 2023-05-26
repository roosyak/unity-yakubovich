
namespace PixselCrew.Model
{
    /*
     теги объектов инвентаря 
     */
    public enum ItemTag
    {
        Stackable,  // добвление объектов к имеющимся, либо по одиночке  
        Usable,     // может попдать в «быстрый инвенталь» 
        Throwable,  // можем выкинуть предмет 
        Potion      // зелье здоровья 
    }
}
